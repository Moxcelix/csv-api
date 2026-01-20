using Microsoft.EntityFrameworkCore;
using CsvApi.Domain;

namespace CsvApi.Infrastructure;

public class ProcessRepository : IProcessRepository
{
    private readonly AppDbContext _context;

    public ProcessRepository(AppDbContext context)
    {
        _context = context; 
    }

    public Process GetProcessByName(string processName)
    {
        return _context.Processes
            .FirstOrDefault(p => p.Name == processName);
    }

    public Process GetProcessById(Guid processId)
    {
        return _context.Processes
            .FirstOrDefault(p => p.Id == processId);
    }

    public void CreateProcess(Process process)
    {
        _context.Processes.Add(process);
        _context.SaveChanges();
    }
}