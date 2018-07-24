using RestaurantManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;
        public void insertBill(int idTable)
        {
            DataProvider.Instance.ExecuteNoneQuery("EXECUTE dbo.USP_InsertBill @idTable", new object[] { idTable });
        }
        public int getUncheckBillByIdTable(int idTable)//kiểm tra Table có tồn tại Bill chưa thanh toán không
        {
            string query = "SELECT * FROM dbo.Bill WHERE status=0 AND  idTable=" + idTable.ToString();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.Id;
            }
            return -1;
            
        }
        public int getMaxIdBill()
        {
            Object data = DataProvider.Instance.ExecuteScalar("SELECT MAX(id) FROM dbo.Bill");
            return (int)data;
        }
        public void checkOut(int id)
        {
            string query = "UPDATE dbo.Bill SET status=1, DateCheckOut=GETDATE() WHERE id="+id.ToString();
            DataProvider.Instance.ExecuteNoneQuery(query);

        }
        public void discount(int id, int discount)
        {
            string query = "UPDATE dbo.Bill SET discount="+discount.ToString()+" WHERE id="+id.ToString();
            DataProvider.Instance.ExecuteNoneQuery(query);
        }
        public DataTable getListBillByDate(DateTime fromDate,DateTime toDate)
        {
           return DataProvider.Instance.ExecuteQuery("EXECUTE USP_GetListBill @fromDate , @toDate", new object[] { fromDate, toDate });
        }

        public static BillDAO Instance {
            get { if (instance == null) instance = new BillDAO();return instance; }
            private set => instance = value; }
        private BillDAO() { }
    }
}
