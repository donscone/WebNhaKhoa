using NhaKhoaQuangVu.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NhaKhoaQuangVu.Repositories
{
    public interface IDatHenRepository
    {
        Task AddAsync(DatHen datHen);
        Task DeleteAsync(int id);
        Task<IEnumerable<DatHen>> GetAllAsync();
        Task<DatHen> GetByIdAsync(int id);
        Task UpdateAsync(DatHen datHen);
        Task<int> ThongBaoTuVanCountAsync();
    }
}
