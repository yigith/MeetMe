using MeetMe.Areas.Admin.Models;
using MeetMe.Data;
using MeetMe.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MeetMe.Areas.Admin.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class EventsController : AdminBaseController
    {
        public EventsController(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IActionResult Index()
        {
            return View(_db.Meetings.ToList());
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(NewMeetingViewModel vm, [FromServices] IWebHostEnvironment env)
        {
            if (ModelState.IsValid)
            {
                // https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-5.0
                // https://stackoverflow.com/questions/35379309/how-to-upload-files-in-asp-net-core
                string fileName = null;
                if (vm.Photo != null && vm.Photo.Length > 0)
                {
                    fileName = vm.Photo.GenerateFileName();
                    var savePath = Path.Combine(env.WebRootPath, "img", fileName);
                    vm.Photo.CopyTo(new FileStream(savePath, FileMode.Create));
                }

                var meeting = new Meeting()
                {
                    Title = vm.Title,
                    Description = vm.Description,
                    MeetingTime = vm.MeetingTime,
                    Place = vm.Place,
                    PhotoPath = fileName
                };
                _db.Meetings.Add(meeting);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            Meeting meeting = _db.Meetings.Find(id);

            if (meeting == null)
            {
                return NotFound();
            }

            var vm = new EditMeetingViewModel()
            {
                Id = meeting.Id,
                Title = meeting.Title,
                Description = meeting.Description,
                MeetingTime = meeting.MeetingTime,
                ExistingPhotoPath = meeting.PhotoPath,
                Place = meeting.Place
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(EditMeetingViewModel vm, [FromServices] IWebHostEnvironment env)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (vm.Photo != null && vm.Photo.Length > 0)
                {
                    fileName = vm.Photo.GenerateFileName();
                    var savePath = Path.Combine(env.WebRootPath, "img", fileName);
                    vm.Photo.CopyTo(new FileStream(savePath, FileMode.Create));
                }

                var meeting = _db.Meetings.Find(vm.Id);
                meeting.MeetingTime = vm.MeetingTime;
                meeting.Description = vm.Description;
                meeting.Place = vm.Place;
                meeting.Title = vm.Title;
                if (!string.IsNullOrEmpty(fileName))
                {
                    // todo: mevcut resim varsa sil
                    meeting.PhotoPath = fileName;
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
