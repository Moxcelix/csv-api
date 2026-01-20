namespace CsvApi.Application;

public struct FindResultRequestDTO
{
    public string? Name;
    public DateTime? startTime; 
    public DateTime? endTime; 
    public double? minValue;
    public double? maxValue;
    public double? minExecutionTime;
    public double? maxExecutionTime;
}