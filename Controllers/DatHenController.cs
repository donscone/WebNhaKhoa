using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NhaKhoaQuangVu.Models;
using NhaKhoaQuangVu.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace NhaKhoaQuangVu.Controllers
{
    [Authorize]
    public class DatHenController : Controller
    {
        private readonly IDatHenRepository _datHenRepository;
        private readonly IBangGiaRepository _bangGiaRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public DatHenController(UserManager<ApplicationUser> userManager, IDatHenRepository datHenRepository, IBangGiaRepository bangGiaRepository)
        {
            _datHenRepository = datHenRepository;
            _bangGiaRepository = bangGiaRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var datHenList = await _datHenRepository.GetAllAsync();         
            return View(datHenList);
        }

        public async Task<IActionResult> Add()
        {
            var danhSachDichVu = await _bangGiaRepository.GetAllAsync();
            ViewBag.DichVuList = danhSachDichVu.Select(dv => new SelectListItem { Value = dv.MaDichVu.ToString(), Text = dv.TenDichVu }).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(DatHen datHen)
        {
            if (ModelState.IsValid)
            {
                await _datHenRepository.AddAsync(datHen);
                return RedirectToAction(nameof(Index));
            }

            var danhSachDichVu = await _bangGiaRepository.GetAllAsync();
            ViewBag.DichVuList = danhSachDichVu.Select(dv => new SelectListItem { Value = dv.MaDichVu.ToString(), Text = dv.TenDichVu }).ToList();
            return View(datHen);
        }

    }
}
