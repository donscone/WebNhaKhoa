using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NhaKhoaQuangVu.Models;
using NhaKhoaQuangVu.Repositories;

namespace NhaKhoaQuangVu.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = $"{SD.Role_Employee},{SD.Role_Admin}")]
    public class LichHenController : Controller
    {
        private readonly IDatHenRepository _datHenRepository;
        private readonly IBangGiaRepository _bangGiaRepository;

        public LichHenController(IDatHenRepository datHenRepository, IBangGiaRepository bangGiaRepository)
        {
            _datHenRepository = datHenRepository;
            _bangGiaRepository = bangGiaRepository;
        }

        public async Task<IActionResult> Index(string trangThai = "", string soDienThoai = "")
        {
            var datHenList = await _datHenRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(trangThai))
            {
                datHenList = datHenList.Where(d => d.TrangThai == trangThai).ToList();
            }
            if (!string.IsNullOrEmpty(soDienThoai))
            {
                datHenList = datHenList.Where(d => d.SDT.Contains(soDienThoai)).ToList();
            }

            // Sắp xếp theo thứ tự trạng thái
            var orderedStatuses = new List<string> { "Đã đặt lịch", "Đã tư vấn", "Đã hoàn thành", "Đã huỷ" };
            datHenList = datHenList.OrderBy(d => orderedStatuses.IndexOf(d.TrangThai)).ToList();

            var unconsultedCount = await _datHenRepository.ThongBaoTuVanCountAsync();
            ViewBag.UnconsultedCount = unconsultedCount;
            ViewBag.SelectedStatus = trangThai;
            return View(datHenList);
        }

        public async Task<IActionResult> DanhSachDichVu()
        {
            var danhSachDichVu = await _bangGiaRepository.GetAllAsync(); // Phương thức GetAll() để lấy tất cả các dịch vụ từ cơ sở dữ liệu

            return PartialView("_DanhSachDichVu", danhSachDichVu); // Trả về một partial view chứa danh sách dịch vụ
        }

        public async Task<IActionResult> Update(int id)
        {
            var danhSachDichVu = await _bangGiaRepository.GetAllAsync();
            ViewBag.DichVuList = danhSachDichVu.Select(dv => new SelectListItem { Value = dv.MaDichVu.ToString(), Text = dv.TenDichVu }).ToList();
            var LichHen = await _datHenRepository.GetByIdAsync(id);
            if (LichHen == null)
            {
                return NotFound();
            }
            return View(LichHen);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, DatHen lichHen)
        {
            var danhSachDichVu = await _bangGiaRepository.GetAllAsync();
            ViewBag.DichVuList = danhSachDichVu.Select(dv => new SelectListItem { Value = dv.MaDichVu.ToString(), Text = dv.TenDichVu }).ToList();

            if (id != lichHen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _datHenRepository.UpdateAsync(lichHen);
                return RedirectToAction("ThongTinLichHen", new { id = lichHen.Id });
            }

            return View(lichHen);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var datHen = await _datHenRepository.GetByIdAsync(id);
            if (datHen == null)
            {
                return NotFound();
            }
            var danhSachDichVu = await _bangGiaRepository.GetAllAsync();
            ViewBag.DichVuList = danhSachDichVu.Select(dv => new SelectListItem { Value = dv.MaDichVu.ToString(), Text = dv.TenDichVu }).ToList();
            return View(datHen);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _datHenRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Cancel(int id)
        {
            var lichHen = await _datHenRepository.GetByIdAsync(id);
            if (lichHen == null)
            {
                return NotFound();
            }

            lichHen.TrangThai = "Đã huỷ";
            await _datHenRepository.UpdateAsync(lichHen);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> HoanThanhTuVan(int id)
        {
            var lichHen = await _datHenRepository.GetByIdAsync(id);
            if (lichHen == null)
            {
                return NotFound();
            }

            lichHen.TrangThai = "Đã tư vấn";
            await _datHenRepository.UpdateAsync(lichHen);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ThongTinLichHen(int id, string TenDichVu)
        {
            var datHen = await _datHenRepository.GetByIdAsync(id);
            if (datHen == null)
            {
                return NotFound(); 
            }
            var dichVu = await _bangGiaRepository.GetByIdAsync(datHen.MaDichVu);
            if (dichVu == null)
            {
                return NotFound();
            }
            var danhSachDichVu = await _bangGiaRepository.GetAllAsync();
            ViewBag.DanhSachDichVu = danhSachDichVu;

            return View(datHen);
        }

        public async Task<IActionResult> Details(int id, string TenDichVu)
        {
            var datHen = await _datHenRepository.GetByIdAsync(id);
            if (datHen == null)
            {
                return NotFound();
            }
            var dichVu = await _bangGiaRepository.GetByIdAsync(datHen.MaDichVu);
            if (dichVu == null)
            {
                return NotFound();
            }
            ViewBag.TenDichVu = dichVu.TenDichVu;

            return View(datHen);
        }

        public async Task<IActionResult> CapNhat(int id, string TenDichVu)
        {
            var datHen = await _datHenRepository.GetByIdAsync(id);
            if (datHen == null)
            {
                return NotFound();
            }
            var dichVu = await _bangGiaRepository.GetByIdAsync(datHen.MaDichVu);
            if (dichVu == null)
            {
                return NotFound();
            }
            ViewBag.TenDichVu = dichVu.TenDichVu;

            return View(datHen);
        }

        public async Task<IActionResult> DaHoanThanhHen(int id)
        {
            var lichHen = await _datHenRepository.GetByIdAsync(id);
            if (lichHen == null)
            {
                return NotFound();
            }

            lichHen.TrangThai = "Đã hoàn thành";
            await _datHenRepository.UpdateAsync(lichHen);

            return RedirectToAction(nameof(Index));
        }

    }
}
