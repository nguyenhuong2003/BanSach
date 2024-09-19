using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataModel
{
    public class DetailCheckOutModel
    {
        public int? MaDetail { get; set; }
       
        public int? MaCheckOut { get; set; }
        public int? MaSach { get; set; }
        public int? SoLuong { get; set; }
        public float? TongTien1Loai { get; set; }
        public int? slTon { get; set; }
        public string? HinhAnh { get; set; }
        public int? MaNhaXuatBan { get; set; }
        public string? TenSach { get; set; }
        public int? MaTacGia { get; set; }
        public float? Gia { get; set; }
        public string? MoTa { get; set; }
    }
}
