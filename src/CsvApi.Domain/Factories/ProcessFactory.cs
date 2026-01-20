namespace CsvApi.Domain;

public class ProcessFactory
{
    public Process Create(string name)
    {
        return new Process
        {
            Id = Guid.NewGuid(),
            Name = name,
        };
    }
}
