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

        public IActionResult ThongBao()
        {
            return View();
        }

        public async Task<IActionResult> Add()
        {
            var danhSachDichVu = await _bangGiaRepository.GetAllAsync();
            ViewBag.DichVuList = danhSachDichVu.Select(dv => new SelectListItem { Value = dv.MaDichVu.ToString(), Text = dv.TenDichVu }).ToList();
            return View();
        }

        public async Task<IActionResult> DanhSachDichVu()
        {
            var danhSachDichVu = await _bangGiaRepository.GetAllAsync(); // Phương thức GetAll() để lấy tất cả các dịch vụ từ cơ sở dữ liệu

            return PartialView("_DanhSachDichVu", danhSachDichVu); // Trả về một partial view chứa danh sách dịch vụ
        }

        [HttpPost]
        public async Task<IActionResult> Add(DatHen datHen)
        {
            if (ModelState.IsValid)
            {
                // Lưu giá trị HoTen vào TempData
                TempData["HoTen"] = datHen.HoTen;

                await _datHenRepository.AddAsync(datHen);
                return RedirectToAction(nameof(xacNhanDatHen));
            }

            var danhSachDichVu = await _bangGiaRepository.GetAllAsync();
            ViewBag.DichVuList = danhSachDichVu.Select(dv => new SelectListItem { Value = dv.MaDichVu.ToString(), Text = dv.TenDichVu }).ToList();
            return View(datHen);
        }

        public IActionResult xacNhanDatHen()
        {
            ViewBag.HoTen = TempData["HoTen"];
            ViewBag.SDT = TempData["SDT"];
            return View();
        }


    }
}
