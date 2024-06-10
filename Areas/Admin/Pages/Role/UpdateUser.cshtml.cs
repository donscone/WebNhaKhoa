using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NhaKhoaQuangVu.Models;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using NhaKhoaQuangVu.DataAccess;

namespace NhaKhoaQuangVu.Areas.Admin.Pages.Role
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UpdateUserModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        protected readonly ApplicationDbContext _context;


        public UpdateUserModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
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

        //public async Task<IActionResult> OnGetAsync(string userid)
        //{
        //    if (userid == null)
        //    {
        //        StatusMessage = "Không có ID người dùng.";
        //        return RedirectToPage("./User");
        //    }

        //    var user = await _userManager.FindByIdAsync(userid);

        //    if (user == null)
        //    {
        //        StatusMessage = $"Không tìm thấy người dùng với ID '{userid}'.";
        //        return RedirectToPage("./User");
        //    }

        //     User = new InputModel
        //    {
        //        Id = user.Id,
        //        FullName = user.FullName,
        //        Email = user.Email,
        //        PhoneNumber = user.PhoneNumber,
        //        Address = user.Address,
        //    };

        //    return Page();
        //}
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                StatusMessage = "Không có ID người dùng.";
                return RedirectToPage("./User");
            }

            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    StatusMessage = $"Không tìm thấy người dùng với ID '{id}'.";
                    return RedirectToPage("./User");
                }

                User = new InputModel
                {
                    Id = user.Id,
                   
                    FullName = user.FullName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address
                };

                return Page();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Đã xảy ra lỗi khi truy xuất thông tin người dùng: {ex.Message}";
                return RedirectToPage("./User");
            }
        }


        //public async Task<IActionResult> OnPostAsync( )
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    var user = await _userManager.FindByIdAsync(User.Id);

        //    if (user == null)
        //    {
        //        StatusMessage = $"Không tìm thấy người dùng với ID '{User.Id}'.";
        //        return RedirectToPage("./User");
        //    }
        //    user.FullName = User.FullName;
        //    user.Email = User.Email;
        //    user.PhoneNumber = User.PhoneNumber;
        //    user.Address = User.Address;

        //    var result = await _userManager.UpdateAsync(user);

        //    if (!result.Succeeded)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //        return Page();
        //    }

        //    StatusMessage = "Thông tin người dùng đã được cập nhật.";
        //    return RedirectToPage("./User");
        //}
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var user = await _userManager.FindByIdAsync(User.Id);

                if (user == null)
                {
                    StatusMessage = $"Không tìm thấy người dùng với ID '{User.Id}'.";
                    return RedirectToPage("./User");
                }

                user.FullName = User.FullName;
                user.Email = User.Email;
                user.PhoneNumber = User.PhoneNumber;
                user.Address = User.Address;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }

                StatusMessage = "Thông tin người dùng đã được cập nhật.";
                return RedirectToPage("./User");
            }
            catch (Exception ex)
            {
                StatusMessage = $"Đã xảy ra lỗi khi cập nhật thông tin người dùng: {ex.Message}";
                return RedirectToPage("./User");
            }
        }

    }

}
