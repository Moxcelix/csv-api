namespace CsvApi.Domain;

public class ProcessBindService
{
    public void Bind(Process process, Operation[] operations)
    {
        if (operations.Length > 10000)
            throw new ToManyOperationsException();

        if (operations.Length < 1)
            throw new NoOperationsException();

        foreach (var operation in operations)
        {
            operation.ProcessId = process.Id;
        }
    }
}
