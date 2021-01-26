using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MeetMe.Services
{
    public class HelperService
    {
        private readonly IWebHostEnvironment env;

        public HelperService(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public bool DeletePhoto(string photoPath)
        {
            if (string.IsNullOrEmpty(photoPath))
            {
                return false;
            }

            string deletePath = Path.Combine(env.WebRootPath, "img", photoPath);

            try
            {
                File.Delete(deletePath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
