using Microsoft.EntityFrameworkCore;
using CsvApi.Domain;

namespace CsvApi.Infrastructure;

public class OperationRepository : IOperationRepository
{
    private readonly AppDbContext _context;

    public OperationRepository(AppDbContext context)
    {
        _context = context;
    }

    public void DeleteOperationsByProcessId(Guid processId)
    {
        var operations = _context.Operations
            .Where(o => o.ProcessId == processId)
            .ToList();
        _context.Operations.RemoveRange(operations);
        _context.SaveChanges();
    }

    public void CreateOperations(Operation[] operations)
    {
        _context.Operations.AddRange(operations);
        _context.SaveChanges();
    }
}
