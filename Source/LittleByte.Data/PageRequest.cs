namespace LittleByte.Data;

public readonly record struct PageRequest(int PageIndex, int PageSize = PageRequest.DefaultPageSize)
{
    private const int DefaultPageSize = 10;
}