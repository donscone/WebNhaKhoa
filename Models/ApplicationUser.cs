using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NhaKhoaQuangVu.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Age { get; set; }
        public string? SDT {  get; set; }
    }
}
