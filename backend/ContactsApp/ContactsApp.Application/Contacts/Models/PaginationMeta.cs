using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Application.Contacts.Models
{
    public sealed record PaginationMeta(long Page, int PageSize, long Total)
    {
        public long TotalPages { get => (long)Math.Ceiling(Total / (double)PageSize); }

        public bool HasNextPage { get => Page < TotalPages; }

        public bool HasPreviousPage { get => Page > 1; }
    }
}
