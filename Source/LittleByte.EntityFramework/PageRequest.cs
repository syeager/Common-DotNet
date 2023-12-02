namespace LittleByte.EntityFramework;

public class PageRequest
{
    private const int DefaultPageSize = 10;

    public int PageSize { get; set; } = DefaultPageSize;
    public int PageIndex { get; set; }

    public PageRequest() { }

    public PageRequest(int pageSize = DefaultPageSize, int pageIndex = 0)
    {
        PageSize = pageSize;
        PageIndex = pageIndex;
    }
}