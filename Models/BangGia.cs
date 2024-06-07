using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NhaKhoaQuangVu.Models
{
    public class BangGia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaDichVu { get; set; }
        public string? TenDichVu { get; set; }
        public decimal GiaDichVu { get; set; }
        public string? MoTa { get; set; }
        public string ChiTiet {  get; set; }
    }
}
