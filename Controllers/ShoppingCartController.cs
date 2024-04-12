using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NhaKhoaQuangVu.DataAccess;
using NhaKhoaQuangVu.Extensions;
using NhaKhoaQuangVu.Migrations;
using NhaKhoaQuangVu.Models;
using NhaKhoaQuangVu.Repositories;

namespace NhaKhoaQuangVu.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IBangGiaRepository _bangGiaRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ShoppingCartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IBangGiaRepository bangGiaRepository)
        {
            _bangGiaRepository = bangGiaRepository;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            return View(cart);
        }

        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            // Phương thức lấy thông tin sản phẩm từ productId
            var product = await GetProductFromDatabase(productId);

            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();

            // Kiểm tra xem sản phẩm đã tồn tại trong giỏ hàng chưa
            var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);
            if (existingItem == null)
            {
                // Nếu sản phẩm chưa tồn tại, thêm vào giỏ hàng
                var cartItem = new CartItem
                {
                    ProductId = productId,
                    Name = product.TenDichVu,
                    Price = product.GiaDichVu,
                    Quantity = quantity
                };
                cart.AddItem(cartItem);

                // Lưu lại giỏ hàng vào Session sau khi đã thêm mục mới
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return RedirectToAction("BangGia", "Home");
        }

        private async Task<BangGia> GetProductFromDatabase(int MaDichVu)
        {
            // Truy vấn cơ sở dữ liệu để lấy thông tin sản phẩm
            var product = await _bangGiaRepository.GetByIdAsync(MaDichVu);
            return product;
        }

        public IActionResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            var cart =
           HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart == null || !cart.Items.Any())
            {
                // Xử lý giỏ hàng trống...
                return RedirectToAction("Index");
            }
            var user = await _userManager.GetUserAsync(User);
            order.UserId = user.Id;
            order.OrderDate = DateTime.UtcNow;
            order.TotalPrice = cart.Items.Sum(i => i.Price * i.Quantity);
            order.OrderDetails = cart.Items.Select(i => new OrderDetail
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList();
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            HttpContext.Session.Remove("Cart");
            return View("OrderCompleted", order.Id);
 }





        public IActionResult OrderCompleted(int orderId)
        {
            // Lấy thông tin đơn hàng từ cơ sở dữ liệu bằng orderId
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                // Xử lý khi không tìm thấy đơn hàng
                return RedirectToAction("BangGia", "Home");

            }
            return View(order);
        }

        public IActionResult RemoveFromCart(int productId)
        {
           var cart =
           HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart is not null)
            {
                cart.RemoveItem(productId);
                // Lưu lại giỏ hàng vào Session sau khi đã xóa mục
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult OrderList()
        {
            // Lấy UserId của người dùng đang đăng nhập
            var userId = _userManager.GetUserId(User);

            // Lấy danh sách các đơn hàng của UserId đang đăng nhập từ cơ sở dữ liệu
            var orderList = _context.Orders
                .Where(o => o.UserId == userId)
                .ToList();

            return View(orderList);
        }


    }
}
