using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NhaKhoaQuangVu.Models;
using NhaKhoaQuangVu.Repositories;

namespace NhaKhoaQuangVu.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = $"{SD.Role_Employee},{SD.Role_Admin}")]
    public class BangGiaController : Controller
    {
        private readonly IBangGiaRepository _bangGiaRepository;

        public BangGiaController(IBangGiaRepository productRepository)
        {
            _bangGiaRepository = productRepository;

        }
        public async Task<IActionResult> Index()
        {
            var BangGia = await _bangGiaRepository.GetAllAsync();
            return View(BangGia);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(BangGia bangGia)
        {
            if (ModelState.IsValid)
            {
                await _bangGiaRepository.AddAsync(bangGia);
                return RedirectToAction(nameof(Index));
            }
            return View(bangGia);
        }
        public async Task<IActionResult> Update(int id)
        {
            var BangGia = await _bangGiaRepository.GetByIdAsync(id);
            if (BangGia == null)
            {
                return NotFound();
            }
            return View(BangGia);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, BangGia bangGia)
        {
            if (ModelState.IsValid)
            {
                await _bangGiaRepository.UpdateAsync(bangGia);
            }

            if (id != bangGia.MaDichVu)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _bangGiaRepository.UpdateAsync(bangGia);
                return RedirectToAction(nameof(Index));
            }
            return View(bangGia);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var bangGia = await _bangGiaRepository.GetByIdAsync(id);
            if (bangGia == null)
            {
                return NotFound();
            }
            return View(bangGia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bangGiaRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }

}

