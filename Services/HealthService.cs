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

        public HealthService(UserDbContext context)
        {
            _context = context;
        }

        public string AddFood(Food food)
        {
            try
            {
                if (_context.Food.Any(x => x.FoodName == food.FoodName))
                    return $"{food.FoodName} already exists.";

                _context.Food.Add(food);
                _context.SaveChanges();
                return "True";
            }
            catch (Exception)
            {
                return "An error occurred while registering entry. Try again later.";
            }
            
        }

        public string UpdateFood(Food food)
        {
            try
            {
                Console.WriteLine(food.FoodId);
                if (_context.Food.Any(x => x.FoodId == food.FoodId))
                {
                    _context.Food.Update(food);
                    _context.SaveChanges();
                    return "True";
                }

                return "Unknown food Id. Please update with a valid food Id";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "An error occurred while registering entry update. Try again later.";
            }

        }

        public Food GetFoodById(int id)
        {
            return _context.Food.Where(x => x.FoodId == id).ToList()[0];
        }

        public List<Food> GetFoodRecords(int page, string category, string search)
        {
            int startIndex = (page-1) * 15;
            List<Food> records = new List<Food>();
            
            if (category == "All")
            {
                if (String.IsNullOrEmpty(search))
                    records = _context.Food.Skip(startIndex).Take(startIndex + 15).ToList();
                else
                    records = _context.Food.Where(x => x.FoodName.ToLower().Contains(search.ToLower()))
                        .Skip(startIndex).Take(startIndex + 15).ToList();
            }
            else
            {
                if (String.IsNullOrEmpty(search))
                    records = _context.Food.Where(x => x.Category == category).Skip(startIndex).Take(startIndex + 15).ToList();
                else
                    records = _context.Food.Where(x => x.Category == category && x.FoodName.ToLower().Contains(search.ToLower())).Skip(startIndex).Take(startIndex + 15).ToList();
            }

            return records;
        }

        public List<Food> GetAllFoodRecords()
        {
            return _context.Food.ToList();
        }

        public void RemoveFood(int foodId)
        {
            Food foodItem = _context.Food.Where(x => x.FoodId == foodId).ToList()[0];
            _context.Food.Remove(foodItem);
            _context.SaveChanges();
        }

        public string AddMeals(List<Meals> meals)
        {
            try
            {
                _context.Meals.AddRange(meals);
                _context.SaveChanges();
                return "True";
            }
            catch (Exception e)
            {
                return "An error occurred while registering your record. Try again later.";
            }
        }

        public string UpdateMeals(List<Meals> meals)
        {
            try
            {
                _context.Meals.UpdateRange(meals);
                _context.SaveChanges();
                return "True";
            }
            catch (Exception e)
            {
                return "An error occurred while registering your record. Try again later.";
            }
        }

        public string RemoveMeals(int userId)
        {
            try
            {
                List<Meals> userTodayMeals = _context.Meals.Where(x => x.UserId == userId && x.Date == DateTime.Today).ToList();

                _context.Meals.RemoveRange(userTodayMeals);
                _context.SaveChanges();
                return "True";
            }
            catch (Exception e)
            {
                return "An error occurred while resetting today's record. Try again later.";
            }
        }

        public List<Meals> GetTodayRecordAdded(int userId)
        {
            List<Meals> mealsList = _context.Meals.Where(m => m.UserId == userId && m.Date == DateTime.Today)
                                                .Join(_context.Food,
                                                m => m.FoodId,
                                                f => f.FoodId,
                                                (m, f) => new Meals { 
                                                    Id = m.Id,
                                                    UserId = m.UserId,
                                                    FoodId = m.FoodId,
                                                    MealType = m.MealType,
                                                    Quantity = m.Quantity,
                                                    Date = m.Date,
                                                    FoodDetails = f
                                                })
                                                .ToList();
            return mealsList;
        }

        public List<Meals> GetMealsRecord(int userId)
        {
            List<Meals> mealsList = _context.Meals.Where(m => m.UserId == userId)
                                                .Join(_context.Food,
                                                m => m.FoodId,
                                                f => f.FoodId,
                                                (m, f) => new Meals
                                                {
                                                    Id = m.Id,
                                                    UserId = m.UserId,
                                                    FoodId = m.FoodId,
                                                    MealType = m.MealType,
                                                    Quantity = m.Quantity,
                                                    Date = m.Date,
                                                    FoodDetails = f
                                                })
                                                .ToList();
            return mealsList;
        }
    }

    // Combine 2 Returned Lists of Objects
    public class UnionList<first, second>
    {
        private first firstList;
        private second secondList;
        public UnionList(first firstList, second secondList)
        {
            this.firstList = firstList;
            this.secondList = secondList;
        }
        public first Left
        {
            get { return this.firstList; }
        }
        public second Right
        {
            get { return this.secondList; }
        }

    }
}
