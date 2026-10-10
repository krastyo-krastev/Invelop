using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Application.Contacts.Models
{
    public sealed class PaginatedResult<T>
    {
        public PaginatedResult(IReadOnlyList<T> data, long page, int pageSize, long total)
        {
            Data = data;
            Pagination = new PaginationMeta(page, pageSize, total);
        }

        public IReadOnlyList<T> Data { get; set; } = null!;

        public PaginationMeta Pagination { get; set; } = null!;
    }
}
