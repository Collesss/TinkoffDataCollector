using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using SaveService.Data;
using SaveService.Exceptions;
using SaveService.Interfaces;
using SaveService.SaveInExcel.Options;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SaveService.SaveInExcel
{
    public class SaveInExcelService : ISaveService
    {
        private readonly ILogger<SaveInExcelService> _logger;
        private readonly IOptions<SaveInExelOptions> _options;

        public SaveInExcelService(ILogger<SaveInExcelService> logger, IOptions<SaveInExelOptions> options)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task Save(SaveData saveData, CancellationToken cancellationToken)
        {
            try
            {
                string fileName = $@"{_options.Value.SaveDirectory}\{Regex.Replace(saveData.StockName, @"[\\\/:*?""<>|]", "_")}.xlsx";

                _logger.LogTrace($"Data saving in file: {fileName}.");

                if (File.Exists(fileName))
                    File.Delete(fileName);

                using ExcelPackage excelPackage = new(fileName);

                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Data");

                worksheet.Cells[1, 1].Value = "CloseTime";
                worksheet.Column(1).Style.Numberformat.Format = "dd.MM.yyyy HH:mm";
                worksheet.Cells[1, 2].Value = "Open";
                worksheet.Cells[1, 3].Value = "Close";
                worksheet.Cells[1, 4].Value = "Low";
                worksheet.Cells[1, 5].Value = "High";

                int row = 2;

                foreach (CandlePayload candle in saveData.CandlePayloads)
                {
                    worksheet.Cells[row, 1].Value = candle.CloseTime;
                    worksheet.Cells[row, 2].Value = candle.Open;
                    worksheet.Cells[row, 3].Value = candle.Close;
                    worksheet.Cells[row, 4].Value = candle.Low;
                    worksheet.Cells[row, 5].Value = candle.High;

                    row++;
                }

                _logger.LogDebug($"Data added in ExcelPackage.");

                worksheet.Cells.AutoFitColumns();

                await excelPackage.SaveAsync(cancellationToken);

                _logger.LogInformation($"Data saved in: {fileName}.");
            }
            catch(Exception e)
            {
                _logger.LogError($"Error saving data: {e.Message}.");

                throw new SaveServiceException(e.Message, e);
            }
        }
    }
}
