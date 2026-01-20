using System.Linq;

namespace CsvApi.Domain;

public class ResultFactory
{
    public Result Create(Process process, Operation[] operations)
    {
        return new Result()
        {
            Id = Guid.NewGuid(),
            ProcessId = process.Id,
            DeltaTime = GetDeltaTime(operations),
            FirstOperationTime = GetFirstOperatationDateTime(operations),
            AverageExecutionTime = GetAverageExecutionTime(operations),
            ValueMean = GetAverageValue(operations),
            ValueMedian = GetMedianValue(operations),
            ValueMin = GetMinValue(operations),
            ValueMax = GetMaxValue(operations),
        };
    }

    private double GetDeltaTime(Operation[] operations)
    {
        var max = operations.Max(op => op.StartDate);
        var min = operations.Min(op => op.StartDate);
        var delta = max - min;

        return delta.TotalSeconds;
    }

    private DateTime GetFirstOperatationDateTime(Operation[] operations)
    {
        return operations.Min(op => op.StartDate);
    }

    private double GetAverageExecutionTime(Operation[] operations)
    {
        return operations.Average(op => op.ExecutionTimeSeconds);
    }

    private double GetAverageValue(Operation[] operations)
    {
        return operations.Average(op => op.Value);
    }

    private double GetMedianValue(Operation[] operations)
    {
        var sortedValues = operations
            .Select(op => op.Value)
            .OrderBy(v => v)
            .ToArray();

        int n = sortedValues.Length;

        if (n % 2 == 1)
        {
            return sortedValues[n / 2];
        }
        else
        {
            return (sortedValues[n / 2 - 1] + sortedValues[n / 2]) / 2.0;
        }
    }

    private double GetMinValue(Operation[] operations)
    {
        return operations.Min(op => op.Value);
    }

    private double GetMaxValue(Operation[] operations)
    {
        return operations.Max(op => op.Value);
    }
}