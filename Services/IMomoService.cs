using NhaKhoaQuangVu.Models.Momo;
using NhaKhoaQuangVu.Models.OrderMomo;

namespace NhaKhoaQuangVu.Services;

public interface IMomoService
{
    Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInfoModel model);
    MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);
}