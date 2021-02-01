using MeetMe.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetMe.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly ApplicationDbContext db;

        public MeetingsController(ApplicationDbContext applicationDbContext)
        {
            db = applicationDbContext;
        }

        // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-5.0#ar
        [Route("meetings/{id:int}/{slug}")]
        public IActionResult Details(int id, string slug)
        {
            var meeting = db.Meetings.Find(id);

            if (meeting == null)
            {
                return NotFound();
            }

            if (meeting.Slug != slug)
            {
                return RedirectToAction("Details", new { id = id, slug = meeting.Slug });
            }

            return View(meeting);
        }
    }
}
