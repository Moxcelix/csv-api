namespace CsvDomain;

public interface IFindResultQuery
{
    public (Result result, Process[] processes) FindByFilter(ResultFilter filter);
}