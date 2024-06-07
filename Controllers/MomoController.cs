﻿using Microsoft.AspNetCore.Mvc;
using NhaKhoaQuangVu.Models.Momo;
using NhaKhoaQuangVu.Models.OrderMomo;
using NhaKhoaQuangVu.Services;

namespace NhaKhoaQuangVu.Controllers
{
    public class MomoController : Controller
    {
        private IMomoService _momoService;

        public MomoController(IMomoService momoService)
        {
            _momoService = momoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentUrl(OrderInfoModel model)
        {
            var response = await _momoService.CreatePaymentAsync(model);
            return Redirect(response.PayUrl);
        }

        [HttpGet]
        public IActionResult PaymentCallBack()
        {
            var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
            return View(response);
        }
    }
}
