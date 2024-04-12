using Microsoft.AspNetCore.Mvc;
using NhaKhoaQuangVu.Models;
using NhaKhoaQuangVu.Repositories;
using System.Diagnostics;

namespace NhaKhoaQuangVu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBangGiaRepository _bangGiaRepository;

        public HomeController(ILogger<HomeController> logger, IBangGiaRepository bangGiaRepository)
        {
            _logger = logger;
            _bangGiaRepository = bangGiaRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> BangGiaAsync()
        {
            var bangGias = await _bangGiaRepository.GetAllAsync();
            return View(bangGias);
        }

        public IActionResult LienHe()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                // Nếu query là null hoặc rỗng, trả về trang tìm kiếm không có kết quả
                return View(new List<BangGia>());
            }

            var searchResults = await _bangGiaRepository.SearchAsync(query);
            return View(searchResults);
        }

    }
}
