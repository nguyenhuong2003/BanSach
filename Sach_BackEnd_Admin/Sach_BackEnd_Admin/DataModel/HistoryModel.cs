using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class HistoryModel
    {
        public int MaDetail  {get; set;}
        public int user_id { get; set;}
        public int MaCheckOut { get; set; }
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public int SoLuong { get; set; }
        public float TongTien1Loai { get; set; }
        public string HinhAnh { get; set; }
    }
}
