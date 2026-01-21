namespace CsvApi.Domain;

public interface ILastValuesQuery
{
    public Operation[] FindValues(string processName);
}