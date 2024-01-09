namespace LittleByte.Data;

public record Page<T>(
    int PageSize,
    int PageIndex,
    int TotalPages,
    int TotalResults,
    IReadOnlyList<T> Results)
{
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
        foreach (var result in Results)
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