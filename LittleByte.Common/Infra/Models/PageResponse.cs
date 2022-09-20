namespace LittleByte.Common.Infra.Models;

public class PageResponse<T>
{
    public int PageSize { get; }
    public int Page { get; }
    public int TotalPages { get; }
    public int TotalResults { get; }
    public IReadOnlyList<T> Results { get; }

    public PageResponse(int pageSize, int page, int totalPages, int totalResults, IReadOnlyList<T> results)
    {
        PageSize = pageSize;
        Page = page;
        TotalPages = totalPages;
        TotalResults = totalResults;
        Results = results;
    }

    public PageResponse<TTo> CastResults<TTo>(Func<T, TTo> castDelegate)
    {
        return new(
            PageSize,
            Page,
            TotalPages,
            TotalResults,
            Results.Select(castDelegate).ToList());
    }

    public async Task<PageResponse<TTo>> CastResultsAsync<TTo>(Func<T, Task<TTo>> castDelegate)
    {
        var results = new List<TTo>(Results.Count);
        foreach(var result in Results)
        {
            var dto = await castDelegate(result);
            results.Add(dto);
        }

        return new(
            PageSize,
            Page,
            TotalPages,
            TotalResults,
            results
        );
    }
}
