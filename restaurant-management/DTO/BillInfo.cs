using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.DTO
{
    public class BillInfo
    {
        int id;
        int idBill;
        int idFood;
        int count;
        public BillInfo(int id, int idBill, int idFood, int count)
        {
            this.Id = id;
            this.IdBill = idBill;
            this.IdFood = idFood;
            this.Count = count;
        }
        public BillInfo(DataRow data)
        {
            this.Id = (int)data["id"];
            this.IdBill = (int)data["idBill"];
            this.IdFood = (int)data["idFood"];
            this.Count = (int)data["count"];
        }
        public int Id { get => id; set => id = value; }
        public int IdBill { get => idBill; set => idBill = value; }
        public int IdFood { get => idFood; set => idFood = value; }
        public int Count { get => count; set => count = value; }
    }
}
