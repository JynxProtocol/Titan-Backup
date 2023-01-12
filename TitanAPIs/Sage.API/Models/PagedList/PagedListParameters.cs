namespace Titan.Models
{
    public class PagedListParameters
    {
        const int maxPageSize = 20;
        private int pageSize = 10;
        private string searchQuery = "";
        private int currentPage = 1;

        public int CurrentPage { get => currentPage; set => currentPage = ((value >= 1) ? value : 1); }
        public int PageSize
        {
            get => pageSize;
            set => pageSize = ((value >= 1 && value < maxPageSize) ? value : maxPageSize);
        }

        public string SearchTerm { get => searchQuery; set => searchQuery = (string.IsNullOrWhiteSpace(value) ? "" : value); }
    }
}
