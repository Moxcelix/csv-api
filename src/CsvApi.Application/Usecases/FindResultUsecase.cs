using CsvApi.Domain;

namespace CsvApi.Application;

public class FindResultUsecase
{
    private readonly IFindResultQuery _findQuery;

    public FindResultUsecase(IFindResultQuery findQuery)
    {
        _findQuery = findQuery;
    }

    public FindResultResponseDTO Execute(FindResultRequestDTO requestDTO)
    {
        var filter = new ResultFilter()
        {
            ProcessName = requestDTO.Name,
            FirstOperationTimeStart = requestDTO.StartTime,
            FirstOperationTimeEnd = requestDTO.EndTime,
            MinMeanValue = requestDTO.MinValue,
            MaxMeanValue = requestDTO.MaxValue,
            MinAvgExecutionTime = requestDTO.MinExecutionTime,
            MaxAvgExecutionTime = requestDTO.MaxExecutionTime,
            Page = requestDTO.Page ?? 0,
            PageSize = requestDTO.PageSize ?? 0
        };

        var results = _findQuery.FindByFilter(filter);
        var resultDTOs = new List<ResultDTO>();

        foreach (var (result, process) in results)
        {
            resultDTOs.Add(new ResultDTO()
            {
                ProcessName = process.Name,
                DeltaTime = result.DeltaTime, 
                FirstOperationTime = result.FirstOperationTime,
                AverageExecutionTime = result.AverageExecutionTime,
                ValueMean = result.ValueMean,
                ValueMedian = result.ValueMedian,
                ValueMin = result.ValueMin,
                ValueMax = result.ValueMax,
            });
        }

        return new FindResultResponseDTO()
        {
            Results = resultDTOs.ToArray()
        };
    }
}