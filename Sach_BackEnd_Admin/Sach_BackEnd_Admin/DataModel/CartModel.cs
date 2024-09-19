using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class CartModel
    {
        public int? MaCart { get; set; }
        public int? User_id { get; set; }
        public int? MaSach { get; set; }
        public string? HinhAnh { get; set; }
        public string? TenSach { get; set; }
        public int? SoLuong { get; set; }
        public float? Gia { get; set; }
        public float? TongTien1Loai { get; set; }
        public float? TongTienCart { get; set; }
      
    }
}
