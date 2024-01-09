namespace LittleByte.Data;

public record PageRequest(int PageIndex = 0, int PageSize = PageRequest.DefaultPageSize)
{
    public const int DefaultPageSize = 10;
}