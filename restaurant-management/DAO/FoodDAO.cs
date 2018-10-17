using RestaurantManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RestaurantManager.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance { get { if (instance == null) instance = new FoodDAO();return instance; }
            private set => instance = value; }
        private FoodDAO() { }
        public List<Food> loadFoodListByIdCategory(int id)
        {
            List<Food> listFood = new List<Food>();
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_LoadFoodListByIdCategory @idCate", new object[] { id });
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);
            }
            return listFood;
        }
        public DataTable loadFoodList()
        {         
            DataTable data = DataProvider.Instance.ExecuteQuery("EXEC dbo.USP_getFoodList");
            return data;
        }
        public bool insertFood(string name, int idCate, float price)
        {
            string query = "EXEC dbo.USP_InsertFood @name , @idCate , @price";
            int data = DataProvider.Instance.ExecuteNoneQuery(query, new object[] { name, idCate, price });
            return data > 0;
        }
        public bool updateFood(string name, int idCate, float price, int id)
        {
            string query = "EXEC dbo.USP_UpdateFood @name , @idCate , @price , @id";
            int data = DataProvider.Instance.ExecuteNoneQuery(query, new object[] { name, idCate, price,id });
            return data > 0;
        }
        public bool deleteFood(int id)
        {
            string query = "DELETE dbo.Food WHERE id="+id.ToString();
            int data = DataProvider.Instance.ExecuteNoneQuery(query);
            return data > 0;
        }
        public DataTable getListFoodByName(string name)
        { 
            string query = "EXECUTE dbo.USP_getFoodListByName @name";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { name });
            return data;
        }
    }
}
