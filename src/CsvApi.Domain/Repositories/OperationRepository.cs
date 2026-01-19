namespace CsvApi.Domain;

public interface IOperationRepository
{
    public void DeleteOperationsByProcessId(string processId);
    public void CreateOperations(Operation[] operations);
}
