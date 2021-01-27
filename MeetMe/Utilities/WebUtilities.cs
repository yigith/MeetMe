using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MeetMe.Utilities
{
    public static class WebUtilities
    {
        public static string GenerateFileName(this IFormFile formFile)
        {
            var ext = Path.GetExtension(formFile.FileName);
            return Guid.NewGuid().ToString() + ext;
        }

        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
