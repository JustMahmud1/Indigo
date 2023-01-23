using IndigoExam.DAL;
using IndigoExam.DTOs.AppUserDTOs;
using IndigoExam.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IndigoExam.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(AppDbContext context, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            AppUser user = new AppUser
            {
                FullName = userRegisterDto.FullName,
                UserName = userRegisterDto.Username,
                Email = userRegisterDto.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, userRegisterDto.Password);


            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View();
                }
            }

            await _userManager.AddToRoleAsync(user, "Admin");

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            AppUser appUser = await _userManager.FindByNameAsync(userLoginDto.Username);

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, userLoginDto.Password, true, true);
            return RedirectToAction("Index", "Home");
            if (!result.Succeeded)
            {

                if (result.IsLockedOut)
                {

                    ModelState.AddModelError("", "your account blocekd for 5 minutes");
                    return View();
                }

                ModelState.AddModelError("", "UserName or Password incorrect");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }

    }

    //public async Task<IActionResult> CreateRole()
    //{
    //    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
    //    await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
    //    await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
    //    return Json("Okey");
    //}

}
