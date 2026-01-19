namespace CsvApi.Domain;

public class ProcessFactory
{
    public Process Create(string name)
    {
        return new Process
        {
            Id = $"process_{DateTime.UtcNow:yyyyMMddHHmmssfff}_{Guid.NewGuid():N8}",
            Name = name,
        };
    }
}
