using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NhaKhoaQuangVu.DataAccess;
using Microsoft.AspNetCore.Authorization;

namespace NhaKhoaQuangVu.Areas.Admin.Pages.Role
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public IndexModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public List<IdentityRole> roles { set; get; }

        [TempData] 
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            roles = await _roleManager.Roles.ToListAsync();

            return Page();
        }
    }
}
