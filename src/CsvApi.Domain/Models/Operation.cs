namespace CsvApi.Domain;

public class Operation
{
    public Guid Id { get; set; }

    public Guid ProcessId { get; set; }

    public DateTime StartDate { get; set; }

    public double ExecutionTimeSeconds { get; set; }

    public double Value { get; set; }
}