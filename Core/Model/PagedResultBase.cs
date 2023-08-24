using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model
{
    public class PagedResultBase
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int NumberOfPage { get; set; }
        public int TotalCount { get; set; }
        public int FirstRowOnPage
        {
            get { return (PageIndex - 1) * PageSize + 1; }
        }
        public int LastRowOnPage
        {
            get { return Math.Min(PageIndex * PageSize, TotalCount); }
        }
    }
}
