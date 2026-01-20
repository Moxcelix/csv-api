using System.Linq;

namespace CsvApi.Domain;

public class ResultFactory
{
    public Result Create(Process process)
    {
        return new Result()
        {
            Id = Guid.NewGuid(),
            ProcessId = process.Id,
        };
    }
}