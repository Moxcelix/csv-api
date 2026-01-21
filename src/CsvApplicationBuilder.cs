using CsvApi.Infrastructure;
using CsvApi.Domain;
using CsvApi.Application;
using CsvApi.API;
using Microsoft.AspNetCore.Http.Features;

namespace CsvApi;

public class CsvApplicationBuilder
{
    private readonly WebApplicationBuilder _builder;
    private WebApplication? _app;

    public CsvApplicationBuilder(string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);
    }

    public CsvApplicationBuilder ConfigureServices()
    {
        // Infrastructure
        _builder.Services.AddSingleton<Env>();
        _builder.Services.AddScoped<AppDbContext>();
        _builder.Services.AddScoped<IProcessRepository, ProcessRepository>();
        _builder.Services.AddScoped<IOperationRepository, OperationRepository>();
        _builder.Services.AddScoped<IResultRepository, ResultRepository>();
        _builder.Services.AddScoped<IFindResultQuery, FindResultQuery>();
        _builder.Services.AddScoped<ILastValuesQuery, LastValuesQuery>();
        
        // Domain
        _builder.Services.AddScoped<OperationFactory>();
        _builder.Services.AddScoped<ProcessFactory>();
        _builder.Services.AddScoped<ResultFactory>();
        _builder.Services.AddScoped<ResultCalculateService>();
        _builder.Services.AddScoped<ProcessBindService>();
        
        // Application
        _builder.Services.AddScoped<AddProcessUsecase>();
        _builder.Services.AddScoped<FindResultUsecase>();
        _builder.Services.AddScoped<GetLastValuesUsecase>();
        
        // API
        _builder.Services.AddScoped<CsvController>();
        _builder.Services.AddScoped<FindController>();
        _builder.Services.AddScoped<LastValuesController>();
        _builder.Services.AddSingleton<Routes>();
        
        // Framework services
        _builder.Services.AddEndpointsApiExplorer();
        _builder.Services.AddSwaggerGen();
        
        // Configuration
        _builder.Services.Configure<FormOptions>(options =>
        {
            options.ValueLengthLimit = int.MaxValue;
            options.MultipartBodyLengthLimit = long.MaxValue;
            options.MemoryBufferThreshold = int.MaxValue;
        });

        return this;
    }

    public CsvApplicationBuilder ConfigureApplication()
    {
        _app = _builder.Build();
        
        _app.UseSwagger();
        _app.UseSwaggerUI();
        _app.UseHttpsRedirection();
        
        return this;
    }

    public CsvApplicationBuilder ConfigureDatabase()
    {
        using var scope = _app!.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.EnsureCreated();
        
        return this;
    }

    public CsvApplicationBuilder ConfigureRoutes()
    {
        using var scope = _app!.Services.CreateScope();
        var routes = scope.ServiceProvider.GetRequiredService<Routes>();
        routes.Setup(_app);
        
        return this;
    }

    public WebApplication Build() => _app!;
}