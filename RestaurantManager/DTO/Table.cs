using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.DTO
{
    public class Table
    {
        int id;
        string name;
        string status;
        public Table() { }
        public Table(int id,string name, string status)
        {
            this.Id = id;
            this.Name = name;
            this.Status = status;
        }
        public Table(DataRow data)
        {
            this.Id = (int)data["id"];
            this.Name = data["name"].ToString();
            this.Status = data["status"].ToString();
        }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Status { get => status; set => status = value; }
    }
}
