using Microsoft.EntityFrameworkCore;
using CsvApi.Domain;

namespace CsvApi.Infrastructure;

public class ResultRepository : IResultRepository
{
    private readonly AppDbContext _context;

    public ResultRepository(AppDbContext context)
    {
        _context = context;
    }

    public Result GetResultById(Guid id)
    {
        return _context.Results
            .FirstOrDefault(r => r.Id == id);
    }

    public Result GetResultByProcessId(Guid processId)
    {
        return _context.Results
            .FirstOrDefault(r => r.ProcessId == processId);
    }

    public Result[] FindByProcessName(string name)
    {
        return _context.Results
            .Join(_context.Processes.Where(p => p.Name.Contains(name)),
                  r => r.ProcessId,
                  p => p.Id,
                  (r, p) => r)
            .ToArray();
    }

    public Result[] FindByFirstOperationTime(DateTime start, DateTime end)
    {
        var startUtc = start.ToUniversalTime();
        var endUtc = end.ToUniversalTime();

        return _context.Results
            .Where(r => r.FirstOperationTime >= startUtc &&
                        r.FirstOperationTime <= endUtc)
            .ToArray();
    }
    
    public void CreateResult(Result result)
    {
        _context.Results.Add(result);
        _context.SaveChanges();
    }

    public void UpdateResult(Result result)
    {
        _context.Results.Update(result);
        _context.SaveChanges();
    }
}