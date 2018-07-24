using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.DTO
{
    public class Account
    {
        string userName;
        string displayName;
        string passWord;
        int type;
        public Account(string user, string display, string pass, int type)
        {
            this.UserName = user;
            this.DisplayName = display;
            this.PassWord = pass;
            this.Type = type;
        }
        public Account(DataRow data)
        {
            this.UserName = data["username"].ToString();
            this.DisplayName = data["displayname"].ToString();
            this.PassWord = data["password"].ToString();
            this.Type = (int)data["type"];
        }
        public string UserName { get => userName; set => userName = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public string PassWord { get => passWord; set => passWord = value; }
        public int Type { get => type; set => type = value; }
    }
}
