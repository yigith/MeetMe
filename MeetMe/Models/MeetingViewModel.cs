using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetMe.Models
{
    public class MeetingViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? MeetingTime { get; set; }

        public string Place { get; set; }

        public string PhotoPath { get; set; }

        public bool IsJoined { get; set; }
    }
}
