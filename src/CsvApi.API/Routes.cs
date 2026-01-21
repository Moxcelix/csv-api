using Microsoft.AspNetCore.Mvc;
using CsvApi.Application;

namespace CsvApi.API;

public class Routes
{
    public void Setup(WebApplication app)
    {
        app.MapPost("/csv/upload",
            async ([FromServices] CsvController controller, IFormFile file) => await controller.UploadCsv(file))
           .WithName("UploadCsv")
           .DisableAntiforgery()
           .Accepts<IFormFile>("multipart/form-data");

        app.MapGet("/results/find",
            ([FromServices] FindController controller,
             [FromQuery] string? name,
             [FromQuery] DateTime? startTime,
             [FromQuery] DateTime? endTime,
             [FromQuery] double? minValue,
             [FromQuery] double? maxValue,
             [FromQuery] double? minExecutionTime,
             [FromQuery] double? maxExecutionTime,
             [FromQuery] int? page,
             [FromQuery] int? pageSize) =>
                controller.FindResults(
                    name, startTime, endTime,
                    minValue, maxValue,
                    minExecutionTime, maxExecutionTime,
                    page, pageSize))
           .WithName("FindResults")
           .WithOpenApi()
           .Produces<FindResultResponseDTO>(200)
           .Produces(400);

        app.MapGet("/csv/health", () =>
            Results.Ok(new { status = "OK", service = "CSV Api" }))
        .WithName("HealthCheck")
        .WithOpenApi();
    }
}