using System;
using System.Collections.Generic;
using System.Linq;

namespace Titan.Models
{
    public class PagedList<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public string SearchTerm { get; set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => (CurrentPage > 1);

        public bool HasNext => (CurrentPage < TotalPages);

        public List<T> Items { get; private set; }

        internal PagedList(List<T> items, int totalCount, int currentPage, int pageSize)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            Items = items;
        }

        public PagedList(int currentPage, int totalPages, string searchTerm, int pageSize, int totalCount, List<T> items)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            SearchTerm = searchTerm;
            PageSize = pageSize;
            TotalCount = totalCount;
            Items = items;
        }

        public static PagedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize, string searchTerm)
        {
            var count = source.Count();
            var items = source.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize) { SearchTerm = searchTerm };
        }
    }
}
