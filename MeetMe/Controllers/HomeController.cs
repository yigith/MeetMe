using MeetMe.Data;
using MeetMe.Models;
using MeetMe.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MeetMe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _db = applicationDbContext;
        }

        public IActionResult Index()
        {
            var loggedIn = User.Identity.IsAuthenticated;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var meetings = _db.Meetings
                .OrderByDescending(x => x.MeetingTime)
                .Select(x => new MeetingViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    MeetingTime = x.MeetingTime,
                    PhotoPath = x.PhotoPath,
                    Place = x.Place,
                    IsJoined = loggedIn && x.Participants.Any(p => p.Id == userId)
                })
                .ToList();
            return View(meetings);
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public IActionResult JoinMeeting(int meetingId)
        {
            var meeting = _db.Meetings.Include(x => x.Participants)
                .FirstOrDefault(x => x.Id == meetingId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _db.Users.Find(userId);

            if (user == null) return Unauthorized();
            if (meeting == null) return NotFound();

            string result;
            // eğer toplantının katılımcılarında user var ise kaldır
            if (meeting.Participants.Contains(user))
            {
                meeting.Participants.Remove(user);
                result = "unjoined";
            }
            // yoksa ekle
            else
            {
                meeting.Participants.Add(user);
                result = "joined";
            }
            _db.SaveChanges();

            return Json(new { result }); 
        }

        [Authorize]
        public IActionResult MyMeetings()
        {
            var userId = User.Id();
            return View(_db.Meetings.Where(x => x.Participants.Any(p => p.Id == userId)).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
