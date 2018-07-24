using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.DTO
{
    public class Bill
    {
        int id;
        DateTime? dateCheckIn;
        DateTime? dateCheckOut;
        int idTable;
        int status;
        int discount;
        public Bill(int id, DateTime? dateCheckIn,DateTime? dateCheckOut, int idTable, int status, int discount)
        {
            this.Id = id;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.IdTable = idTable;
            this.Status = status;
            this.Discount = discount;
        }
        public Bill(DataRow data)
        {
            this.Id = (int)data["id"];
            this.DateCheckIn =(DateTime)data["dateCheckIn"];
            try
            {
                this.DateCheckOut = (DateTime)data["dateCheckOut"];
            }catch
            {
                this.DateCheckOut = null;               
            }
           
            this.IdTable = (int)data["idTable"];
            this.Status =(int)data["status"];
            this.Discount =(int)data["discount"];
        }
        public int Id { get => id; set => id = value; }
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public int IdTable { get => idTable; set => idTable = value; }
        public int Status { get => status; set => status = value; }
        public int Discount { get => discount; set => discount = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
    }
}
