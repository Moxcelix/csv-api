namespace CsvApi.Domain;

public class Operation
{
    public string Id { get; set; }

    public string ProcessId { get; set; }

    public DateTime StartDate { get; set; }

    public double ExecutionTimeSeconds { get; set; }

    public double Value { get; set; }
}