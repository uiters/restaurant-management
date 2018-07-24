using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.DTO
{
    public class Menu
    {
        private string name;
        private int count;
        private float price;
        
        public Menu(string name, int count, float price)
        {
            this.Name = name;
            this.Count = count;
            this.Price = price;
        }
        public Menu(DataRow data)
        {
            this.Name = data["name"].ToString();
            this.Count = (int)data["count"];
            this.Price = (float)Convert.ToDouble(data["price"].ToString());
        }
        public string Name { get => name; set => name = value; }
        public int Count { get => count; set => count = value; }
        public float Price { get => price; set => price = value; }
    }
}
