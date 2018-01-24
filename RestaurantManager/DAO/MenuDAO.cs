using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestaurantManager.DTO;

namespace RestaurantManager.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance {
            get { if (instance == null) instance = new MenuDAO();return instance; }
            private set => instance = value; }
        private MenuDAO() { }
        public List<DTO.Menu> loadMenuByIdTable(int idTable)
        {
            List<DTO.Menu> listMenu = new List<DTO.Menu>();
            DataTable data = DataProvider.Instance.ExecuteQuery("EXECUTE dbo.USP_LoadMenuByIdTable @idTable", new object[] { idTable});
            foreach (DataRow item in data.Rows)
            {
                DTO.Menu menu = new DTO.Menu(item);
                listMenu.Add(menu);
            }
            return listMenu;
        }
    }
}
