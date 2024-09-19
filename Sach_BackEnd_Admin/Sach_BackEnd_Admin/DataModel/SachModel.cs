using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class SachModel
    {
        public int MaLoai { get; set; }
        public int? MaSach { get; set; }
        public string? TenSach { get; set; }
        public int? slTon { get; set; }
        public string? TenNhaXuatBan { get; set; }
        public string? TenTacGia { get; set; }
        public string? TenLH { get; set; }

        public float? Gia { get; set; }

        public string? HinhAnh { get; set; }
        public int? MaNhaXuatBan { get; set; }
        public int? MaTacGia { get; set; }
        public string? MoTa { get; set; }
    }
}
