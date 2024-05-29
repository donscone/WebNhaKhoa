using System.ComponentModel.DataAnnotations;

namespace NhaKhoaQuangVu.Models
{
    public class DatHen
    {
        public int Id { get; set; }
        [Required]
        public string HoTen { get; set; }
        [Required]
        public string SDT { get; set; }
        [Required]
        public int MaDichVu { get; set; }
        public DateTime NgayHen { get; set; }
        [Required]
        public DateTime GioHen { get; set; }
        [Required]
        public string TrangThai { get; set; }
    }
}
