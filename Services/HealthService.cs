using EDP_Project.Models;
using MySql.Data.MySqlClient;
using Pract2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Services
{
    public class HealthService
    {
        private readonly UserDbContext _context;

        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;database=it2166;username=root;password=password");

        public HealthService(UserDbContext context)
        {
            _context = context;
        }

        public bool AddFood(Food food)
        {
            MySqlCommand newFood = new MySqlCommand("INSERT INTO food VALUES(NULL, @FoodName, @Category, @Calories)", con);
            con.Open();
            newFood.Parameters.AddWithValue("@FoodName", food.FoodName);
            newFood.Parameters.AddWithValue("@Category", food.Category);
            newFood.Parameters.AddWithValue("@Calories", food.Calories);
            newFood.ExecuteNonQuery();
            con.Close();
            //if (_context.Food.Any(x => x.FoodId == food.FoodId))
            //    return false;

            //_context.Food.Add(food);
            //_context.SaveChanges();
            return true;
        }

        public bool UpdateFood(Food food)
        {
            if (_context.Food.Any(x => x.FoodId == food.FoodId))
            {
                _context.Food.Add(food);
                _context.SaveChanges();
                return true;
            }
            
            return false;
        }

        public List<Food> GetFoodRecords(int page)
        {
            int startIndex = page * 15;
            List<Food> records = new List<Food>();
            records = _context.Food.Skip(startIndex).Take(startIndex + 15).ToList();
            return records;
        }

        public void RemoveFood(int foodId)
        {
            Food foodItem = _context.Food.Where(x => x.FoodId == foodId).ToList()[0];
            _context.Food.Remove(foodItem);
        }
    }
}
