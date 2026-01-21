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
            ProcessName = requestDTO.name,
            FirstOperationTimeStart = requestDTO.startTime,
            FirstOperationTimeEnd = requestDTO.endTime,
            MinMeanValue = requestDTO.minValue,
            MaxMeanValue = requestDTO.maxValue,
            MinAvgExecutionTime = requestDTO.minExecutionTime,
            MaxAvgExecutionTime = requestDTO.maxExecutionTime,
            Page = requestDTO.page,
            PageSize = requestDTO.PageSize
        };

        var results = _findQuery.FindByFilter(filter);
        var resultDTOs = new List<ResultDTO>();

        foreach (var result in results)
        {
            resultDTOs.Add(new ResultDTO()
            {
                ProcessName = result.Process.Name,
                DeltaTime = result.Result.DeltaTime,
                FirstOperationTime = result.Result.FirstOperationTime,
                AverageExecutionTime = result.Result.AverageExecutionTime,
                ValueMean = result.Result.ValueMean,
                ValueMedian = result.Result.ValueMedian,
                ValueMin = result.Result.ValueMin,
                ValueMax = result.Result.ValueMax,
            });
        }

        return new FindResultResponseDTO()
        {
            Results = resultDTOs.ToArray()
        };
    }
}