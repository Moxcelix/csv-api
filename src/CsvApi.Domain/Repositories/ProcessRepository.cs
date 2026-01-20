namespace CsvApi.Domain;

public interface IProcessRepository
{
    public Process GetProcessByName(string processName);
    public Process GetProcessById(Guid processId);
    public void CreateProcess(Process process);
}
