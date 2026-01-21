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