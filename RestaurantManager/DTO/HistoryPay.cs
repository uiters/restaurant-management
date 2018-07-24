using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.DTO
{
    public class HistoryPay
    {
        string tableName;
        DateTime? dateCheckIn;
        DateTime? dateCheckOut;
        int discount;
        float totalPrice;
        float finalPrice;
        public HistoryPay(string tableName,DateTime? dateCheckIn,DateTime? dateCheckOut, int discount, float totalPrice, float finalPrice)
        {
            this.TableName = tableName;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.Discount = discount;
            this.TotalPrice = totalPrice;
            this.FinalPrice = finalPrice;
        }
        public HistoryPay(DataRow data)
        {
            this.TableName = data["tableName"].ToString();
            this.DateCheckIn =(DateTime) data["dateCheckIn"];
            this.DateCheckOut = (DateTime)data["dateCheckOut"];
            this.Discount =(int) data["discount"];
            this.TotalPrice = (float)Convert.ToDouble(data["totalPrice"].ToString());
            this.FinalPrice = (float)Convert.ToDouble(data["finalPrice"].ToString());
        }
        public string TableName { get => tableName; set => tableName = value; }
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int Discount { get => discount; set => discount = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
        public float FinalPrice { get => finalPrice; set => finalPrice = value; }
    }
}
