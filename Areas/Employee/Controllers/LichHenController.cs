using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NhaKhoaQuangVu.Models;
using NhaKhoaQuangVu.Repositories;

namespace NhaKhoaQuangVu.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = SD.Role_Employee)]
    public class LichHenController : Controller
    {
        private readonly IDatHenRepository _datHenRepository;
        private readonly IBangGiaRepository _bangGiaRepository;

        public LichHenController(IDatHenRepository datHenRepository, IBangGiaRepository bangGiaRepository)
        {
            _datHenRepository = datHenRepository;
            _bangGiaRepository = bangGiaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var datHenList = await _datHenRepository.GetAllAsync();
            return View(datHenList);
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
            if (ModelState.IsValid)
            {
                await _datHenRepository.UpdateAsync(lichHen);
            }

            if (id != lichHen.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _datHenRepository.UpdateAsync(lichHen);
                return RedirectToAction(nameof(Index));
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
    }
}
