using NhaKhoaQuangVu.DataAccess;
using NhaKhoaQuangVu.Models;
using Microsoft.EntityFrameworkCore;

namespace NhaKhoaQuangVu.Repositories
{
 
    public class EFBangGiaRepository : IBangGiaRepository
    {
        private readonly ApplicationDbContext _context;
        public EFBangGiaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BangGia bangGia)
        {
            _context.BangGias.Add(bangGia);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.BangGias.FindAsync(id);
            _context.BangGias.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BangGia>> GetAllAsync()
        {
            return await _context.BangGias.ToListAsync();
        }
        public async Task<BangGia> GetByIdAsync(int id)
        {
            return await _context.BangGias.FindAsync(id);
        }

        public async Task UpdateAsync(BangGia bangGia)
        {
            _context.BangGias.Update(bangGia);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BangGia>> SearchAsync(string query)
        {
            return await _context.BangGias.Where(p => p.TenDichVu.Contains(query)).ToListAsync();
        }

    }
}
