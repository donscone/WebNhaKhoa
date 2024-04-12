using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NhaKhoaQuangVu.Models;
using NhaKhoaQuangVu.Repositories;

namespace NhaKhoaQuangVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class OrderController : Controller
    {
        
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IActionResult> Index()
        {
            var NhanVien = await _orderRepository.GetAllAsync();
            return View(NhanVien);
        }
        public async Task<IActionResult> Update(int id)
        {
            var BangGia = await _orderRepository.GetByIdAsync(id);
            if (BangGia == null)
            {
                return NotFound();
            }
            return View(BangGia);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, Order order)
        {
            if (ModelState.IsValid)
            {


                await _orderRepository.UpdateAsync(order);
            }

            if (id != order.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _orderRepository.UpdateAsync(order);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _orderRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _orderRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
