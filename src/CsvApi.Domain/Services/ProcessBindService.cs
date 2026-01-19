namespace CsvApi.Domain;

public class ProcessBindService
{
    public void Bind(Process process, List<Operation> operations)
    {
        if (operations.Count > 10000)
            throw new ToManyOperationsException();

        if (operations.Count < 1)
            throw new NoOperationsException();

        foreach (var operation in operations)
        {
            operation.ProcessId = process.Id;
        }
    }
}
