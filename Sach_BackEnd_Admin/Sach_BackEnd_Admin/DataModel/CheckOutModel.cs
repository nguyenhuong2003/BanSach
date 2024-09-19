using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
	public class CheckOutModel
	{

		public int? MaCheckOut {get; set;}
		public List<int> MaCart {get;set;}
        public string? NameUser { get; set; }
        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }
        public string? QuocGia { get; set; }
        public string? Tinh { get; set; }
        public string? Quan { get; set; }
        public string? DiaChi { get; set; }
        public string? PhuongTienThanhToan { get; set; }

    }
}
