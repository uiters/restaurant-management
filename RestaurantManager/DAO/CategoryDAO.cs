using RestaurantManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;
        private CategoryDAO() { }

        public static CategoryDAO Instance { get { if (instance == null) instance = new CategoryDAO();return instance; }
            private set => instance = value; }
        public List<Category> loadCategory()
        {
            List<Category> listCate = new List<Category>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.FoodCategory");
            foreach (DataRow item in data.Rows)
            {
                Category cate = new Category(item);
                listCate.Add(cate);
            }
            return listCate;
        }
        public DataTable loadCategory2()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT id [ID], name [Tên danh mục] FROM dbo.FoodCategory");
        }
        public bool insertCate(string name)
        {
            string query = "EXECUTE dbo.USP_InsertCate @name";
            int data = DataProvider.Instance.ExecuteNoneQuery(query, new object[] { name});
            return data > 0;
        }
        public bool updateCate(string name, int id)
        {
            string query = "EXECUTE USP_UpdateCate @name , @id";
            int data = DataProvider.Instance.ExecuteNoneQuery(query, new object[] { name,id});
            return data > 0;
        }
        public bool deleteCate(int id)
        {
            string query = "DELETE dbo.FoodCategory WHERE id=" + id.ToString();
            int data = DataProvider.Instance.ExecuteNoneQuery(query);
            return data > 0;
        }
    }
}
