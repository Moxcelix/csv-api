namespace CsvApi.Application;

public struct FindResultRequestDTO
{
    public string? name;
    public DateTime? startTime; 
    public DateTime? endTime; 
    public double? minValue;
    public double? maxValue;
    public double? minExecutionTime;
    public double? maxExecutionTime;
    public int? page;
    public int? pageSize;
}