using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance { get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set => instance = value; }
        private AccountDAO() { }
        public bool login(string userName, string passWord)
        {
           
            string query = "EXECUTE dbo.USP_Login @user , @pass";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { userName, passWord });
            if (data.Rows.Count > 0) return true;
            return false;
        }
    }
}
