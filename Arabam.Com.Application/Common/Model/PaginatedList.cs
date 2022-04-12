namespace Arabam.Com.Application.Common.Model
{
    public class PaginatedList<T>
    {
        public List<T> Data { get; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }

        public PaginatedList(List<T> data, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Data = data;
        }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

    }
}
