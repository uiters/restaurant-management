using RestaurantManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;
        public List<Table> loadListTable()
        {
            List<Table> listTable = new List<Table>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT* FROM dbo.TableFood");
            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                listTable.Add(table);
            }
            return listTable;
        }
        internal static TableDAO Instance { get { if (instance == null) instance = new TableDAO();return instance; }
            private set => instance = value; }
        private TableDAO() { }
    }
}
