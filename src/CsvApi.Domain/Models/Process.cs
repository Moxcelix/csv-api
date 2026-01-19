namespace CsvApi.Domain;

public class Process
{
    public DateTime StartDate { get; set; }

    public double ExecutionTimeSeconds { get; set; }

    public double Value { get; set; }
}