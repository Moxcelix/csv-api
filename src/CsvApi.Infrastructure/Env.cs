namespace CsvApi.Infrastructure;

public class Env
{
    public string AppEnvironment { get; set;}
    public string PgHost { get; set;}
    public string PgPort { get; set;}
    public string PgUser { get; set;}
    public string PgPass { get; set;}
    public string PgName { get; set;}
    public string PgSsl { get; set; }

    public Env()
    {
        LoadFromEnvironment();
    }

       private void LoadFromEnvironment()
    {
        AppEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
        PgHost = Environment.GetEnvironmentVariable("PG_HOST");
        PgPort = Environment.GetEnvironmentVariable("PG_PORT");
        PgUser = Environment.GetEnvironmentVariable("PG_USER");
        PgPass = Environment.GetEnvironmentVariable("PG_PASS");
        PgName = Environment.GetEnvironmentVariable("PG_NAME");
        PgSsl = Environment.GetEnvironmentVariable("PG_SSL");
    }

    private void LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
            return;

        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split('=', 2);
            if (parts.Length != 2) continue;

            var key = parts[0].Trim();
            var value = parts[1].Trim();

            switch (key)
            {
                case "APP_ENV": AppEnvironment = value; break;
                case "PG_HOST": PgHost = value; break;
                case "PG_PORT": PgPort = value; break;
                case "PG_USER": PgUser = value; break;
                case "PG_PASS": PgPass = value; break;
                case "PG_NAME": PgName = value; break;
                case "PG_SSL": PgSsl = value; break;
            }
        }
    }

    public string GetPostgresConnectionString()
    {
        return $"Host={PgHost};Port={PgPort};Database={PgName};Username={PgUser};Password={PgPass};SslMode={PgSsl}";
    }
}
