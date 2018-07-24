using RestaurantManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.DAO
{
    public class HistoryPayDAO
    {
        private static HistoryPayDAO instance;
        public List<HistoryPay> loadListPay(DateTime date)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("EXECUTE dbo.USP_ListPay @dateCheckOut", new object[] { date});
            List<HistoryPay> listPay = new List<HistoryPay>();
            foreach (DataRow item in data.Rows)
            {
                HistoryPay pay = new HistoryPay(item);
                listPay.Add(pay);
            }
            return listPay;
        }
        public static HistoryPayDAO Instance { get { if (instance == null) instance = new HistoryPayDAO();return instance; }
            private set => instance = value; }
        private HistoryPayDAO() { }
    }
}
