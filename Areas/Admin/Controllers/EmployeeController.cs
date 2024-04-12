using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NhaKhoaQuangVu.Models;
using NhaKhoaQuangVu.Repositories;

namespace NhaKhoaQuangVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository productRepository)
        {
            _employeeRepository = productRepository;

        }
        public async Task<IActionResult> Index()
        {
            var NhanVien = await _employeeRepository.GetAllAsync();
            return View(NhanVien);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                await _employeeRepository.AddAsync(nhanVien);
                return RedirectToAction(nameof(Index));
            }
            return View(nhanVien);
        }
        public async Task<IActionResult> Update(int id)
        {
            var NhanVien = await _employeeRepository.GetByIdAsync(id);
            if (NhanVien == null)
            {
                return NotFound();
            }
            return View(NhanVien);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {


                await _employeeRepository.UpdateAsync(nhanVien);
            }

            if (id != nhanVien.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _employeeRepository.UpdateAsync(nhanVien);
                return RedirectToAction(nameof(Index));
            }
            return View(nhanVien);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var NhanVien = await _employeeRepository.GetByIdAsync(id);
            if (NhanVien == null)
            {
                return NotFound();
            }
            return View(NhanVien);
        }
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var NhanVien = await _employeeRepository.GetByIdAsync(id);
            if (NhanVien != null)
            {
                await _employeeRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
