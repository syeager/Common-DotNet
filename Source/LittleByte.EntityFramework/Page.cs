namespace LittleByte.EntityFramework;

public class Page<T>
{
    public int PageSize { get; }
    public int PageIndex { get; }
    public int TotalPages { get; }
    public int TotalResults { get; }
    public IReadOnlyList<T> Results { get; }

    public Page(int pageSize, int pageIndex, int totalPages, int totalResults, IReadOnlyList<T> results)
    {
        PageSize = pageSize;
        PageIndex = pageIndex;
        TotalPages = totalPages;
        TotalResults = totalResults;
        Results = results;
    }

    public Page<TTo> CastResults<TTo>(Func<T, TTo> castDelegate)
    {
        return new Page<TTo>(
            PageSize,
            PageIndex,
            TotalPages,
            TotalResults,
            Results.Select(castDelegate).ToList());
    }

    public async Task<Page<TTo>> CastResultsAsync<TTo>(Func<T, Task<TTo>> castDelegate)
    {
        var results = new List<TTo>(Results.Count);
        foreach(var result in Results)
        {
            var dto = await castDelegate(result);
            results.Add(dto);
        }

        return new Page<TTo>(
            PageSize,
            PageIndex,
            TotalPages,
            TotalResults,
            results
        );
    }
}