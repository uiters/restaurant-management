using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;
        public void insertBillInfo(int idBill, int idFood, int count)
        {
            DataProvider.Instance.ExecuteNoneQuery("EXEC dbo.USP_InsertBillInfo @idBill , @idFood , @count", new object[] { idBill,idFood,count});
        }
        public void deleteBillInfoByIdFood(int idFood)
        {
            DataProvider.Instance.ExecuteNoneQuery("DELETE dbo.BillInfo WHERE idFood=" + idFood.ToString());
        }

        public static BillInfoDAO Instance {
            get { if (instance == null) instance = new BillInfoDAO();return instance; }
            private set => instance = value; }
        private BillInfoDAO() { }
    }
}
