using CsvApi;

var app = new CsvApplicationBuilder(args)
    .ConfigureServices()
    .ConfigureApplication()
    .ConfigureDatabase()
    .ConfigureRoutes()
    .Build();

app.Run();