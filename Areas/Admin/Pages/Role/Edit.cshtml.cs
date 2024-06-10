using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaKhoaQuangVu.Models;
using Microsoft.AspNetCore.Authorization;

namespace NhaKhoaQuangVu.Areas.Admin.Pages.Role
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EditUserModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EditUserModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public class InputModel
        {
            public string Id { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
        }

        [BindProperty]
        public InputModel User { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var User = await _userManager.FindByIdAsync(id);
            if (User == null)
            {
                return NotFound($"Không tìm thấy người dùng với ID '{id}'.");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByIdAsync(User.Id);

            if (user == null)
            {
                return NotFound($"Không tìm thấy người dùng với ID '{User.Id}'.");
            }
            user.FullName = User.FullName;
            user.Email = User.Email;
            user.PhoneNumber = User.PhoneNumber;
            user.Address = User.Address;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToPage("./User", new { id = User.Id });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
