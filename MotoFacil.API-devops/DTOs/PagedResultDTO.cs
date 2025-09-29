﻿namespace MotoFacil.API.DTOs
{
    public class PagedResultDTO<T>
    {
        public required IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}