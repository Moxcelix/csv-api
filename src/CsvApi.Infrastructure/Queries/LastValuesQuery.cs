using Microsoft.EntityFrameworkCore;
using CsvApi.Domain;

namespace CsvApi.Infrastructure;

public class LastValuesQuery : ILastValuesQuery
{
    private readonly AppDbContext _context;

    public LastValuesQuery(AppDbContext context)
    {
        _context = context;
    }
    
    public Operation[] FindValues(string processName)
    {
        return _context.Operations
            .Join(_context.Processes.Where(p => p.Name == processName),
                o => o.ProcessId,
                p => p.Id,
                (o, p) => o)
            .OrderByDescending(o => o.StartDate)
            .Take(10)
            .ToArray();
    }
}