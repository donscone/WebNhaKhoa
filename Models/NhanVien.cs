using System.ComponentModel.DataAnnotations;

namespace NhaKhoaQuangVu.Models
{
    public class NhanVien
    {
        public int ID { get; set; }
        [Required, StringLength(100)]
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string ChucVu { get; set; }
        public string DiaChi { get; set; }
    }
}
