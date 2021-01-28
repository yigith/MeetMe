using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetMe.Models
{
    public class HomeViewModel
    {
        public List<MeetingViewModel> Meetings { get; set; }

        public int TotalItemsCount { get; set; }

        public int ItemsCount { get; set; }

        public int PageCount { get; set; }

        public int PageSize { get; set; }

        public int Page { get; set; }

        public bool IsPrevious { get; set; }

        public bool IsNext { get; set; }
    }
}
