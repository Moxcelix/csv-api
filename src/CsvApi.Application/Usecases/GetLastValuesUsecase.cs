using CsvApi.Domain;

namespace CsvApi.Application;

public class GetLastValuesUsecase
{
    private readonly ILastValuesQuery _lastValuesQuery;

    public GetLastValuesUsecase(ILastValuesQuery lastValuesQuery)
    {
        _lastValuesQuery = lastValuesQuery;
    }

    public LastValuesResponseDTO Execute(string processName)
    {
        var values = _lastValuesQuery.FindValues(processName);
        var valuesDTOs = new List<ValueDTO>();

        foreach (var val in values)
        {
            valuesDTOs.Add(new ValueDTO()
            {
                StartDate = val.StartDate,
                ExecutionTimeSeconds = val.ExecutionTimeSeconds,
                Value = val.Value
            });
        }

        return new LastValuesResponseDTO()
        {
            Values = valuesDTOs.ToArray()
        };
    }
}