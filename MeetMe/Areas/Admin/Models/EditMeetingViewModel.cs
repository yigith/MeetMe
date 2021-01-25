using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetMe.Areas.Admin.Models
{
    public class EditMeetingViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? MeetingTime { get; set; }

        public string Place { get; set; }

        public string ExistingPhotoPath { get; set; }

        public IFormFile Photo { get; set; }
    }
}
