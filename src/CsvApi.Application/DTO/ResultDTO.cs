namespace CsvApi.Application;

public struct ResultDTO
{
    public string ProcessName;
    public double DeltaTime;
    public DateTime FirstOperationTime;
    public double AverageExecutionTime;
    public double ValueMean;
    public double ValueMedian;
    public double ValueMin;
    public double ValueMax;
}