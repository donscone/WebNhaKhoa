using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NhaKhoaQuangVu.DataAccess;
using NhaKhoaQuangVu.Models;

namespace NhaKhoaQuangVu.Repositories
{
    public class EFEmployeeRepository : IEmployeeRepository
    {
        
        
            private readonly ApplicationDbContext _context;
            public EFEmployeeRepository(ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task AddAsync(NhanVien nhanVien)
            {
                _context.nhanViens.Add(nhanVien);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var nhanvien = await _context.nhanViens.FindAsync(id);
                _context.nhanViens.Remove(nhanvien);
                await _context.SaveChangesAsync();
            }

            public async Task<IEnumerable<NhanVien>> GetAllAsync()
            {
                return await _context.nhanViens.ToListAsync();
            }

            public async Task<NhanVien> GetByIdAsync(int id)
            {
                return await _context.nhanViens.FindAsync(id);
            }

            public async Task UpdateAsync(NhanVien lichHen)
            {
                _context.nhanViens.Update(lichHen);
                await _context.SaveChangesAsync();
            }

        
    }
}
