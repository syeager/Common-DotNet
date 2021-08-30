namespace LittleByte.Domain
{
    public class PageRequest
    {
        private const int DefaultPageSize = 10;

        public int PageSize { get; set; } = DefaultPageSize;
        public int Page { get; set; }

        public PageRequest() {}

        public PageRequest(int pageSize = DefaultPageSize, int page = 0)
        {
            PageSize = pageSize;
            Page = page;
        }
    }
}