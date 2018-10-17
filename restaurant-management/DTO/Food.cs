using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.DTO
{
    public class Food
    {
        int id;
        string name;
        int idCategory;
        float price;
        public Food(int id, string name, int idCategory, float price)
        {
            this.Id = id;
            this.Name = name;
            this.IdCategory = idCategory;
            this.Price = price;
        }
        public Food(DataRow data)
        {
            this.Id = (int)data["id"];
            this.Name = data["name"].ToString();
            this.IdCategory = (int)data["idCategory"];
            this.Price = (float)Convert.ToDouble(data["price"].ToString());
        }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int IdCategory { get => idCategory; set => idCategory = value; }
        public float Price { get => price; set => price = value; }
    }
}
