using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NhaKhoaQuangVu.Models;

namespace NhaKhoaQuangVu.Areas.Employee.Controllers
{
    public class EmployeeController : Controller
    {
        [Area("Employee")]
        [Authorize(Roles = SD.Role_Employee)]
        public IActionResult Index()
        {
            return View();
        }

    }
}
