namespace CsvApi.Application;

public struct ResultDTO
{
    public string ProcessName { get; set; }
    public double DeltaTime { get; set; }
    public DateTime FirstOperationTime { get; set; }
    public double AverageExecutionTime { get; set; }
    public double ValueMean { get; set; }
    public double ValueMedian { get; set; }
    public double ValueMin { get; set; }
    public double ValueMax { get; set; }
}