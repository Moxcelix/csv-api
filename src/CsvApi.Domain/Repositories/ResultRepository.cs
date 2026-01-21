namespace CsvApi.Domain;

public interface IResultRepository
{
    public Result GetResultById(Guid id);
    public Result GetResultByProcessId(Guid id);
    public void CreateResult(Result result);
    public void UpdateResult(Result result);
}