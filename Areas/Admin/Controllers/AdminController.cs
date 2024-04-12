using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NhaKhoaQuangVu.Models;
using System.Data;

namespace NhaKhoaQuangVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class AdminController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> CreateAdminAccount()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            var user = new ApplicationUser
            {
                UserName = "tien9291@gmail.com",
                Email = "tien9291@gmail.com"
            };
            var result = await _userManager.CreateAsync(user, "Hutech@123");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                return Content("Admin account created successfully!");
            }
            return BadRequest("Failed to create admin account!");

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
