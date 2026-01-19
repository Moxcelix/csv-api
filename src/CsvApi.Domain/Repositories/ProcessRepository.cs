namespace CsvApi.Domain;

public interface IProcessRepository
{
    public Process GetProcessByName(string processName);
    public Process GetProcessById(string processId);
    public void CreateProcess(Process process);
}
