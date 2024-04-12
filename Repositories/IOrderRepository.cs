using NhaKhoaQuangVu.Models;

namespace NhaKhoaQuangVu.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task DeleteAsync(int id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int id);
        Task UpdateAsync(Order order);
    }
}
