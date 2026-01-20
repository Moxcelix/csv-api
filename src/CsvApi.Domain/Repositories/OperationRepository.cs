namespace CsvApi.Domain;

public interface IOperationRepository
{
    public void DeleteOperationsByProcessId(Guid processId);
    public void CreateOperations(Operation[] operations);
}
