using System;

namespace FilmStudioApiManagementApp.Services.AppUser
{
    public class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
