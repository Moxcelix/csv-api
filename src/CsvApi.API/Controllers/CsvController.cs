using Microsoft.AspNetCore.Mvc;
using CsvApi.Application;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace CsvApi.API;

public class CsvController
{
    private readonly AddProcessUsecase _usecase;
    private readonly ILogger<CsvController> _logger;

    public CsvController(AddProcessUsecase usecase, ILogger<CsvController> logger)
    {
        _usecase = usecase;
        _logger = logger;
    }

    public async Task<IResult> UploadCsv(IFormFile file)
    {
        var fileName = file.FileName.ToLowerInvariant();

        if (!fileName.EndsWith(".csv"))
        {
            return Results.BadRequest("Unsorted file type.");
        }

        try
        {
            if (file?.Length == 0) return Results.BadRequest("File is required");

            var records = await ParseCsv(file);
            _usecase.Execute(new CsvDTO
            {
                Name = Path.GetFileNameWithoutExtension(file.FileName),
                Records = records.ToArray()
            });

            return Results.Ok(new { message = "Success", count = records.Count });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing CSV upload");

            return Results.Problem(ex.Message);
        }
    }

    private async Task<List<CsvRecordDTO>> ParseCsv(IFormFile file)
    {
        var records = new List<CsvRecordDTO>();

        using var reader = new StreamReader(file.OpenReadStream());

        while (await reader.ReadLineAsync() is string line)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var parts = line.Split(';');
            if (parts.Length != 3)
            {
                throw new FormatException($"Invalid CSV format: expected 3 columns, got {parts.Length} on line");
            }

            var dateStr = parts[0].Trim();

            var startDate = DateTime.ParseExact(
                dateStr,
                "yyyy-MM-dd'T'HH-mm-ss.ffff'Z'",
                CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind);

            var execTime = double.Parse(parts[1].Trim(), CultureInfo.InvariantCulture);
            var value = double.Parse(parts[2].Trim(), CultureInfo.InvariantCulture);

            records.Add(new CsvRecordDTO
            {
                StartDate = startDate,
                ExecutionTimeSeconds = execTime,
                Value = value
            });
        }

        return records;
    }
}
