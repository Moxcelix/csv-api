namespace CsvApi.Domain;

public class OperationFactory
{
    public Operation Create(DateTime startDate, double executionTimeSeconds, double value)
    {
        Validate(startDate, executionTimeSeconds, value);

        return new Operation
        {
            Id = $"operation_{DateTime.UtcNow:yyyyMMddHHmmssfff}_{Guid.NewGuid():N8}",
            StartDate = startDate,
            ExecutionTimeSeconds = executionTimeSeconds,
            Value = value,
        };
    }

    private void Validate(DateTime startDate, double executionTimeSeconds, double value)
    {
        if (startDate > DateTime.UtcNow)
            throw new CannotBeFutureException();

        if (startDate < new DateTime(2000, 1, 1))
            throw new CannotBeBeforeException();

        if (executionTimeSeconds < 0)
            throw new ExecutionTimeCannotBeNegativeException();

        if (value < 0)
            throw new ValueCannotBeNegativeException();
    }
}
