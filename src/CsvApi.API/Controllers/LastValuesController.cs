using Microsoft.AspNetCore.Mvc;
using CsvApi.Application;
using Microsoft.Extensions.Logging;

namespace CsvApi.API;

public class LastValuesController
{
    private readonly GetLastValuesUsecase _usecase;
    private readonly ILogger<LastValuesController> _logger;

    public LastValuesController(GetLastValuesUsecase usecase, ILogger<LastValuesController> logger)
    {
        _usecase = usecase;
        _logger = logger;
    }

    public async Task<IResult> GetLastValues(string processName)
    {
        try
        {
            var response = _usecase.Execute(processName);

            return Results.Json(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error finding values");
            return Results.Problem(ex.Message);
        }
    }
}