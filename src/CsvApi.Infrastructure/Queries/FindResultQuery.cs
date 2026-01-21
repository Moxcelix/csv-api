using Microsoft.EntityFrameworkCore;
using CsvApi.Domain;

namespace CsvApi.Infrastructure;

public class FindResultQuery : IFindResultQuery
{
    private readonly AppDbContext _context;

    public FindResultQuery(AppDbContext context)
    {
        _context = context;
    }

    public (Result result, Process process)[] FindByFilter(ResultFilter filter)
    {
        var query = from result in _context.Results
                    join process in _context.Processes
                    on result.ProcessId equals process.Id
                    select new { Result = result, Process = process };

        if (!string.IsNullOrEmpty(filter.ProcessName))
        {
            query = query.Where(x => x.Process.Name.Contains(filter.ProcessName));
        }

        if (filter.FirstOperationTimeStart.HasValue)
        {
            var startUtc = filter.FirstOperationTimeStart.Value.ToUniversalTime();
            query = query.Where(x => x.Result.FirstOperationTime >= startUtc);
        }

        if (filter.FirstOperationTimeEnd.HasValue)
        {
            var endUtc = filter.FirstOperationTimeEnd.Value.ToUniversalTime();
            query = query.Where(x => x.Result.FirstOperationTime <= endUtc);
        }

        if (filter.MinMeanValue.HasValue)
            query = query.Where(x => x.Result.ValueMean >= filter.MinMeanValue.Value);

        if (filter.MaxMeanValue.HasValue)
            query = query.Where(x => x.Result.ValueMean <= filter.MaxMeanValue.Value);

        if (filter.MinAvgExecutionTime.HasValue)
            query = query.Where(x => x.Result.AverageExecutionTime >= filter.MinAvgExecutionTime.Value);

        if (filter.MaxAvgExecutionTime.HasValue)
            query = query.Where(x => x.Result.AverageExecutionTime <= filter.MaxAvgExecutionTime.Value);

        query = query.OrderByDescending(x => x.Result.FirstOperationTime);

        if (filter.HasPagination)
        {
            query = query
                .Skip(filter.SkipCount)
                .Take(filter.PageSize);
        }

        return query
            .AsEnumerable()
            .Select(x => (x.Result, x.Process))
            .ToArray();
    }
}