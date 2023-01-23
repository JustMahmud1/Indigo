using Microsoft.AspNetCore.Identity;

namespace IndigoExam.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
