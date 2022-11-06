namespace API.Application.Common.Models
{
    public class PaginatedList<T>
    {
        public IReadOnlyCollection<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int NextPage { get; set; }
        public int PreviousPage { get; set; }
        public int PageSize { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, int nextPage, int previousPage)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
            NextPage = nextPage;
            PreviousPage = previousPage;
            PageSize = pageSize;
        }
        public bool HasNextPage { get => PageIndex < TotalPages; }
        public bool HasPreviousPage { get => PageIndex > 0; }

        public static PaginatedList<T> Create(List<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();

            var lastPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(count) / pageSize));

            var nextPage = pageIndex >= 1 && pageIndex < lastPage ? pageIndex + 1 : 0;

            var previousPage = pageIndex > 1 ? pageIndex - 1 : 1;

            var items = source.Skip((pageIndex -1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<T>(items, count, pageIndex, pageSize, nextPage, previousPage);
        }
    }
}
