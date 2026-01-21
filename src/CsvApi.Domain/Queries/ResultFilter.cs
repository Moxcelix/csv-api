namespace CsvApi.Domain;

public class ResultFilter
{
    public string? ProcessName { get; set; }
    public DateTime? FirstOperationTimeStart { get; set; }
    public DateTime? FirstOperationTimeEnd { get; set; }
    public double? MinMeanValue { get; set; }
    public double? MaxMeanValue { get; set; }
    public double? MinAvgExecutionTime { get; set; }
    public double? MaxAvgExecutionTime { get; set; }

    public int Page { get; set; }
    public int PageSize { get; set; }

    public bool HasPagination => Page > 0 && PageSize > 0;
    public int SkipCount => (Page - 1) * PageSize;
}