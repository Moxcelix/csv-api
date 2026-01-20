namespace CsvApi.Domain;

public class Result
{
    public Guid Id { get; set; }

    public Guid ProcessId { get; set; }

    public double DeltaTime { get; set; }

    public DateTime FirstOperationTime { get; set; }

    public double AverageExecutionTime { get; set; }

    public double ValueMean { get; set; }
    
    public double ValuseMedian { get; set; }

    public double ValueMin { get; set; }

    public double ValueMax { get; set; }
}