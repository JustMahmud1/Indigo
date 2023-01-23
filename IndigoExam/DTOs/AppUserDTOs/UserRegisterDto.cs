using System.ComponentModel.DataAnnotations;

namespace IndigoExam.DTOs.AppUserDTOs
{
    public class UserRegisterDto
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
