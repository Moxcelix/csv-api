using CsvApi.Infrastructure;
using CsvApi.Domain;
using CsvApi.Application;
using CsvApi.API;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Infra
builder.Services.AddSingleton<Env>();
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped<IProcessRepository, ProcessRepository>();
builder.Services.AddScoped<IOperationRepository, OperationRepository>();
// Domain
builder.Services.AddScoped<OperationFactory>();
builder.Services.AddScoped<ProcessFactory>();
builder.Services.AddScoped<ProcessBindService>();
// Application
builder.Services.AddScoped<AddProcessUsecase>();
// Api
builder.Services.AddScoped<CsvController>();

builder.Services.AddSingleton<Routes>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = long.MaxValue; 
    options.MemoryBufferThreshold = int.MaxValue;
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var routes = scope.ServiceProvider.GetRequiredService<Routes>();
    routes.Setup(app);

    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();