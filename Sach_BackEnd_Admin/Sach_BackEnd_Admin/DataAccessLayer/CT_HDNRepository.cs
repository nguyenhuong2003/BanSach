using DataAccessLayer.Interfaces;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CT_HDNRepository : ICTHDNRepository
    {
        private IDatabaseHelper _dbHelper;
        public CT_HDNRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public  List<CT_HDNModel> AllCT_HDN()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "AllCT_HDN");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<CT_HDNModel>().ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CT_HDNModel GetDatabyID(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "cthdn_get_by_id",
                     "@id", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<CT_HDNModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CT_HDNModel> Search(string keyword)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "chitietHDN_search",
                     "@maCTHDN", keyword);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<CT_HDNModel>().ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Create(CT_HDNModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "chitietHDN_insert ",
                "@hoadonNhapID", model.HoaDonnhapID,
                "@maSach", model.MaSach,
                "@soLuong", model.SoLuong,
                "@donGia", model.DonGia);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(CT_HDNModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "chitietHDN_update",
                 "@maCTHDN", model.MaCTHDN,
                "@hoadonNhapID", model.HoaDonnhapID,
                "@maSach", model.MaSach,
                "@soLuong", model.SoLuong,
                "@donGia", model.DonGia);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(int id)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "chitietHDN_delete", "@maCTHDN", id);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
