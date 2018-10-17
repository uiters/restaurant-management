using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.DTO
{
    public class Category
    {
        private int id;
        private string name;
        public Category(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public Category(DataRow data)
        {
            this.Id = (int)data["id"];
            this.Name = data["name"].ToString();
        }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
