using RestaurantManager.DTO;
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
            //Mã hóa MD5
            byte[] temp = Encoding.ASCII.GetBytes(passWord);
            byte[] hashData = new MD5CryptoServiceProvider().ComputeHash(temp);
            //var list = hashData.ToString();
            //list.Reverse();//Lật ngược
            string hashPass = "";
            foreach (byte item in hashData)
            {
                hashPass += item;
            }

            string query = "EXECUTE dbo.USP_Login @user , @pass";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { userName, hashPass });
            if (data.Rows.Count > 0) return true;
            return false;
        }
        public Account getAccount(string userName,string passWord)
        {
            MD5 md5 = MD5.Create();
            byte[] temp = Encoding.ASCII.GetBytes(passWord);
            byte[] hashData = md5.ComputeHash(temp);
            string hashPass = "";
            foreach (byte item in hashData)
            {
                hashPass += item;
            }

            DataTable data=DataProvider.Instance.ExecuteQuery("EXECUTE dbo.USP_GetAccount @userName , @passWord",new object[] { userName, hashPass });
            Account account = new Account(data.Rows[0]);
            return account;
        }
        public bool changeAccountOnlyDisplayName(string userName, string displayName)
        {
            return DataProvider.Instance.ExecuteNoneQuery("EXEC dbo.USP_ChangeAccountOnlyDislayName @userName , @displayName", new object[] { userName,displayName})>0;
        }
        public bool changeAccount(string userName, string displayName, string passWord )
        {
            MD5 md = MD5.Create();
            byte[] temp = Encoding.ASCII.GetBytes(passWord);
            byte[] hashData = md.ComputeHash(temp);
            string hashPass = "";
            foreach (byte item in hashData)
            {
                hashPass += item;
            }
            return DataProvider.Instance.ExecuteNoneQuery("EXECUTE dbo.USP_ChangeAccount @userName , @displayName , @newPassWord", new object[] { userName, displayName, hashPass}) > 0;
        }
        public Account getDisplayNameByUserName(string userName)
        {
           DataTable data= DataProvider.Instance.ExecuteQuery("EXECUTE dbo.USP_GetDisplayNameByUserName @userName",new object[] { userName });
            Account acc = new Account(data.Rows[0]);
            return acc;
        }
        public DataTable loadListAccForAdmin()
        {
           return DataProvider.Instance.ExecuteQuery("SELECT UserName[Tên đăng nhập],DisplayName[Tên hiển thị],Type[Loại tài khoản] FROM dbo.Account where  type=0");
        }
        public DataTable loadListAccForRoot()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT UserName[Tên đăng nhập],DisplayName[Tên hiển thị],Type[Loại tài khoản] FROM dbo.Account where type in(0,1)");
        }
        public bool insertAcc(string user,string display, string pass, int type)
        {
            MD5 md = MD5.Create();
            byte[] temp = Encoding.ASCII.GetBytes(pass);
            byte[] hashData = md.ComputeHash(temp);
            string hashPass = "";
            foreach (byte item in hashData)
            {
                hashPass += item;
            }
            string query = "EXEC dbo.USP_InsertAcc @user , @display , @pass , @type";
            int data = DataProvider.Instance.ExecuteNoneQuery(query, new object[] {user,display,hashPass,type});
            return data > 0;
        }
        public bool updateAcc(string user, string display,int type)
        {
            string query = "EXECUTE dbo.USP_UpdateAcc @display , @type , @user";
            int data = DataProvider.Instance.ExecuteNoneQuery(query, new object[] { display,type,user});
            return data > 0;
        }
        public bool deleteAcc(string user)
        {
            string query = "EXECUTE dbo.USP_DeleteAcc @user";
            int data = DataProvider.Instance.ExecuteNoneQuery(query,new object[] { user});
            return data > 0;
        }
        public int checkUserName(string user)
        {
            string query = "EXEC dbo.USP_CheckUserName @user";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { user });
            return data.Rows.Count;
        }
    }
}
