using NhaKhoaQuangVu.Models;

namespace NhaKhoaQuangVu.Repositories
{
    public interface IBangGiaRepository
    {
        Task AddAsync(BangGia bangGia);
        Task DeleteAsync(int id);
        Task<IEnumerable<BangGia>> GetAllAsync();
        Task<BangGia> GetByIdAsync(int MaDichVu);
        Task UpdateAsync(BangGia bangGia);
        Task<IEnumerable<BangGia>> SearchAsync(string query);
    }
}
