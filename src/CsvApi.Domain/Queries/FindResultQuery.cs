namespace CsvApi.Domain;

public interface IFindResultQuery
{
    public (Result result, Process process)[] FindByFilter(ResultFilter filter);
}