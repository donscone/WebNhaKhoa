using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaKhoaQuangVu.DataAccess;
using Microsoft.AspNetCore.Authorization;

namespace NhaKhoaQuangVu.Areas.Admin.Pages.Role
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        protected readonly ApplicationDbContext _context;

        public DeleteModel(RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public IdentityRole Role { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string roleid)
        {
            if (string.IsNullOrEmpty(roleid))
            {
                return NotFound("Không tìm thấy role");
            }

            Role = await _roleManager.FindByIdAsync(roleid);
            if (Role == null)
            {
                return NotFound("Không tìm thấy role");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string roleid)
        {
            if (string.IsNullOrEmpty(roleid))
            {
                return NotFound("Không tìm thấy role");
            }

            var role = await _roleManager.FindByIdAsync(roleid);
            if (role == null)
            {
                return NotFound("Không thấy role cần xóa");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                StatusMessage = "Đã xóa " + role.Name;
                return RedirectToPage("./Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
