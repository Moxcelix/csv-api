namespace CsvApi.Application;

public struct FindResultRequestDTO
{
    public string? Name;
    public DateTime? StartTime; 
    public DateTime? EndTime; 
    public double? MinValue;
    public double? MaxValue;
    public double? MinExecutionTime;
    public double? MaxExecutionTime;
    public int? Page;
    public int? PageSize;
}