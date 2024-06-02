using Microsoft.EntityFrameworkCore;
using NhaKhoaQuangVu.DataAccess;
using NhaKhoaQuangVu.Models;

namespace NhaKhoaQuangVu.Repositories
{
    public class EFDatHenRepository : IDatHenRepository
    {
        private readonly ApplicationDbContext _context;

        public EFDatHenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(DatHen datHen)
        {
            _context.datHens.Add(datHen);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var datHen = await _context.datHens.FindAsync(id);
            if (datHen != null)
            {
                _context.datHens.Remove(datHen);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DatHen>> GetAllAsync()
        {
            return await _context.datHens.ToListAsync();
        }

        public async Task<DatHen> GetByIdAsync(int id)
        {
            return await _context.datHens.FindAsync(id);
        }

        public async Task UpdateAsync(DatHen datHen)
        {
            _context.Entry(datHen).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> ThongBaoTuVanCountAsync()
        {
            return await _context.datHens.CountAsync(dh => dh.TrangThai == "Đã đặt lịch");
        }

    }
}
