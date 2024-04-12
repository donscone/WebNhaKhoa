using NhaKhoaQuangVu.Models;

namespace NhaKhoaQuangVu.Repositories
{
    public interface IEmployeeRepository
    {
        Task AddAsync(NhanVien nhanVien);
        Task DeleteAsync(int id);
        Task<IEnumerable<NhanVien>> GetAllAsync();
        Task<NhanVien> GetByIdAsync(int id);
        Task UpdateAsync(NhanVien nhanVien);
    }
}
