using Microsoft.AspNetCore.Mvc;
using CsvApi.Application;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace CsvApi.API;

public class FindController
{
    private readonly FindResultUsecase _usecase;
    private readonly ILogger<FindController> _logger;

    public FindController(FindResultUsecase usecase, ILogger<FindController> logger)
    {
        _usecase = usecase;
        _logger = logger;
    }

    public async Task<IResult> FindResults(
        [FromQuery] string? name,
        [FromQuery] DateTime? startTime,
        [FromQuery] DateTime? endTime,
        [FromQuery] double? minValue,
        [FromQuery] double? maxValue,
        [FromQuery] double? minExecutionTime,
        [FromQuery] double? maxExecutionTime,
        [FromQuery] int? page,
        [FromQuery] int? pageSize)
    {
        try
        {
            var requestDTO = new FindResultRequestDTO
            {
                Name = name,
                StartTime = startTime,
                EndTime = endTime,
                MinValue = minValue,
                MaxValue = maxValue,
                MinExecutionTime = minExecutionTime,
                MaxExecutionTime = maxExecutionTime,
                Page = page,
                PageSize = pageSize
            };

            var response = _usecase.Execute(requestDTO);

            return Results.Json(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error finding results");
            return Results.Problem(ex.Message);
        }
    }
}