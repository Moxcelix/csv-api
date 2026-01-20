using Microsoft.AspNetCore.Mvc;

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

        app.MapGet("/csv/health", () =>
            Results.Ok(new { status = "OK", service = "CSV Processor" }))
        .WithName("HealthCheck")
        .WithOpenApi();
    }
}