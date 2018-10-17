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
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT* FROM dbo.TableFood WHERE status!=N'Đã xóa'");
            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                listTable.Add(table);
            }
            return listTable;
        }
        public DataTable loadListTable2()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT id [ID],name[Tên bàn],status [Trạng thái] FROM dbo.TableFood  WHERE status!=N'Đã xóa'");
        }

        public Table loadTable(int idTable)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT* FROM dbo.TableFood where id="+idTable.ToString());
            Table table = new Table(data.Rows[0]);
            return table;
        }
        public void switchTable(int id1, int id2)
        {
            DataProvider.Instance.ExecuteNoneQuery("EXEC dbo.SwitchTable @idTable1 , @idTable2", new object[] { id1, id2 });
        }
        public bool insertTable(string name, string status)
        {
            string query = "EXEC dbo.USP_InsertTable @name , @status";
            int data = DataProvider.Instance.ExecuteNoneQuery(query, new object[] { name,status });
            return data > 0;
        }
        public bool updateTable(int id, string name, string status)
        {
            string query = "EXECUTE dbo.USP_UpdateTable @id , @name , @status";
            int data = DataProvider.Instance.ExecuteNoneQuery(query, new object[] { id,name,status });
            return data > 0;
        }
        public bool deleteTable(int id)
        {
            //Đối với trường hợp Xóa bàn, nếu xóa bàn thì sẽ phải xóa BillInfo và Bill=> ảnh hưởng
            //đến danh thu do đó không thể xóa trực tiếp bàn đó.
            //Theo suy nghĩ của bản thân thì: Chỉ cần update trạng thái bàn đó thành"Đã xóa"
            //sau đó không show bàn đó nữa =))
            string query = "EXEC dbo.USP_DeleteTable @id";
            int data = DataProvider.Instance.ExecuteNoneQuery(query,new object[] { id });
            return data > 0;
        }
        internal static TableDAO Instance { get { if (instance == null) instance = new TableDAO();return instance; }
            private set => instance = value; }
        private TableDAO() { }
    }
}
