namespace CsvApi.Application;

public struct ValueDTO
{
    public DateTime StartDate { get; set; }

    public double ExecutionTimeSeconds { get; set; }

    public double Value { get; set; }
}