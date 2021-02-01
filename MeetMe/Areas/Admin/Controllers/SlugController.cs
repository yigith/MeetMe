using MeetMe.Data;
using MeetMe.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetMe.Areas.Admin.Controllers
{
    public class SlugController : AdminBaseController
    {
        public SlugController(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        [HttpPost]
        public string Generate(string text)
        {
            return WebUtilities.URLFriendly(text);
        }
    }
}
