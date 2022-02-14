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

        //--------------- Meals methods ---------------

        public string AddFood(Food food)
        {
            try
            {
                if (_context.Food.Any(x => x.FoodName == food.FoodName))
                    return $"{food.FoodName} already exists.";

                _context.Food.Add(food);
                _context.SaveChanges();
                return "true";
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
                    return "true";
                }

                return "Unknown food Id. Please update with a valid food Id";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "An error occurred while registering entry update. Try again later.";
            }

        }

        public string RemoveFood(int foodId)
        {
            try 
            {
                Food foodItem = _context.Food.Where(x => x.FoodId == foodId).ToList()[0];
                _context.Food.Remove(foodItem);
                _context.SaveChanges();
                return "true";
            }
            catch (Exception e)
            {
                return "An error occurred while resetting today's record. Try again later.";
            }
}

        public Food GetFoodById(int id)
        {
            return _context.Food.Where(x => x.FoodId == id).ToList()[0];
        }

        public object GetFoodRecords(string search, string sort, string order, int limit, int offset)
        {
            List<Food> foodList = new List<Food>();

            var uncompletedfoodList = _context.Food.AsQueryable();

            if (!string.IsNullOrEmpty(sort) && string.IsNullOrEmpty(order))
            {
                uncompletedfoodList = uncompletedfoodList.OrderBy(x => x.GetType().GetProperty(sort).GetValue(x));
            }

            if (!string.IsNullOrEmpty(search))
            {
                uncompletedfoodList = uncompletedfoodList.Where(x => x.Calories.ToString().Contains(search)
                                || x.FoodName.Contains(search)
                                || x.FoodId.ToString().Contains(search)
                                || x.Category.Contains(search));
            }

            foodList = uncompletedfoodList.Skip(offset * limit).Take(limit).ToList();
            int total = _context.Food.Count();

            var jsonData = new { total = total, rows = foodList };
            return jsonData;
        }

        public List<Food> GetAllFoodRecords()
        {
            return _context.Food.ToList();
        }

        public string AddMeals(List<Meals> meals)
        {
            try
            {
                _context.Meals.AddRange(meals);
                _context.SaveChanges();
                return "true";
            }
            catch (Exception e)
            {
                return "An error occurred while registering your record. Try again later.";
            }
        }

        public string UpdateMeals(List<Meals> meals, int userId)
        {
            try
            {
                _context.Meals.RemoveRange(_context.Meals.Where(x => x.UserId == userId && x.Date == meals[0].Date));
                _context.Meals.AddRange(meals);
                
                _context.SaveChanges();
                return "true";
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
                return "true";
            }
            catch (Exception e)
            {
                return "An error occurred while resetting today's record. Try again later.";
            }
        }

        public List<Meals> GetTodayMealAdded(int userId)
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

        enum enumMeals { Breakfast, Lunch, Dinner }
        public object GetMealsRecord(int userId, string search, string sort, string order, int limit, int offset)
        {
            List<Meals> mealsList = new List<Meals>();

            try
            {
                var uncompletedMealsList = _context.Meals.Where(m => m.UserId == userId)
                                                    .Join(_context.Food,
                                                    m => m.FoodId, f => f.FoodId,
                                                    (m, f) => new Meals
                                                    {
                                                        Id = m.Id,
                                                        UserId = m.UserId,
                                                        FoodId = m.FoodId,
                                                        MealType = m.MealType,
                                                        Quantity = m.Quantity,
                                                        Date = m.Date,
                                                        FoodDetails = f
                                                    });

                if (!string.IsNullOrEmpty(sort) && string.IsNullOrEmpty(order))
                {
                    uncompletedMealsList = uncompletedMealsList.OrderBy(x => x.GetType().GetProperty(sort).GetValue(x));
                    Console.WriteLine(uncompletedMealsList);
                }

                if (!string.IsNullOrEmpty(search))
                {
                    uncompletedMealsList = uncompletedMealsList.Where(x => x.Date.ToString().Contains(search)
                                    || x.FoodDetails.FoodName.Contains(search));
                }

                // Offset & limit 7 by DATE
                int todayDay = (int) (Weekdays) Enum.Parse(typeof(Weekdays), DateTime.Today.DayOfWeek.ToString());
                DateTime maxDay = DateTime.Today.AddDays(-todayDay);
                int actualLimit = uncompletedMealsList.Where(x => x.Date >= maxDay).ToList().Count;

                mealsList = uncompletedMealsList.Skip(offset * actualLimit).Take(actualLimit).ToList();


                // Format results to the right format
                List<MealsRecord> resultList = new List<MealsRecord>();

                DateTime dateChecking;
                List<DateTime> dateChecked = new List<DateTime>();

                foreach (var row in mealsList)
                {
                    // Get all the rows of same day
                    dateChecking = row.Date;

                    if (!dateChecked.Contains(dateChecking))
                    {
                        Console.WriteLine(row.Date);
                        Meals[] sameDayArray = mealsList.Where(x => x.Date == dateChecking).ToArray();
                        // All meals of e.g. 01/01/2022
                        dateChecked.Add(dateChecking);

                        // [[row1, row2] -- Breakfast]
                        List<List<Meals>> sameMealArray = new List<List<Meals>>() { new List<Meals>(), new List<Meals>(), new List<Meals>() };

                        foreach (var row2 in sameDayArray)
                        {
                            // Organize the rows into their meals
                            Meals currentDict = row2;
                            switch (row2.MealType)
                            {
                                case "Breakfast":
                                    sameMealArray[(int)enumMeals.Breakfast].Add(row2);
                                    break;
                                case "Lunch":
                                    sameMealArray[(int)enumMeals.Lunch].Add(row2);
                                    break;
                                case "Dinner":
                                    sameMealArray[(int)enumMeals.Dinner].Add(row2);
                                    break;
                            }
                        }

                        MealsRecord mealRecord = new MealsRecord();

                        for (var i = 0; i < sameMealArray.Count; i++)
                        {
                            var list = sameMealArray[i];
                            // all rows r e.g. Breakfast
                            var mealString = "";

                            foreach (var row2 in list)
                            {
                                // Run through all breakfast meals
                                mealString += row2.FoodDetails.FoodName;
                                if (row2.Quantity > 1)
                                {
                                    mealString += " x" + row2.Quantity;
                                }
                                mealString += "<br />";
                            }

                            var resultDate = sameDayArray[0].Date;
                            mealRecord.Date = resultDate.ToShortDateString() + "<br />" + resultDate.DayOfWeek;
                            switch (((enumMeals)i).ToString())
                            {
                                case "Breakfast":
                                    mealRecord.Breakfast = mealString;
                                    break;
                                case "Lunch":
                                    mealRecord.Lunch = mealString;
                                    break;
                                case "Dinner":
                                    mealRecord.Dinner = mealString;
                                    break;
                            }
                            mealString = "";
                        }

                        resultList.Add(mealRecord);
                    }
                    else
                    {
                        continue;
                    }
                }

                var jsonData = new { total = resultList.Count, rows = resultList };
                return jsonData;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new object();
            }

        }

        public class MealsRecord
        {
            public string Date { get; set; }
            public string Breakfast { get; set; }
            public string Lunch { get; set; }
            public string Dinner { get; set; }
        }

        //--------------- Exercise methods ---------------

        public string AddExercise(Exercise exercise)
        {
            try
            {
                if (_context.Exercise.Any(x => x.ExerciseName == exercise.ExerciseName))
                    return $"{exercise.ExerciseName} already exists.";

                _context.Exercise.Add(exercise);
                _context.SaveChanges();
                return "true";
            }
            catch (Exception)
            {
                return "An error occurred while registering entry. Try again later.";
            }

        }

        public string UpdateExercise(Exercise exercise)
        {
            try
            {
                Console.WriteLine(exercise.ExerciseId);
                if (_context.Exercise.Any(x => x.ExerciseId == exercise.ExerciseId))
                {
                    _context.Exercise.Update(exercise);
                    _context.SaveChanges();
                    return "true";
                }

                return "Unknown exercise Id. Please update with a valid exercise Id";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "An error occurred while registering entry update. Try again later.";
            }

        }

        public string RemoveExercise(int exerciseId)
        {
            try 
            {
                Exercise exercise = _context.Exercise.Where(x => x.ExerciseId == exerciseId).ToList()[0];
                _context.Exercise.Remove(exercise);
                _context.SaveChanges();
                return "true";
            }
            catch (Exception e)
            {
                return "An error occurred while resetting today's record. Try again later.";
            }
}

        public Exercise GetExerciseById(int id)
        {
            return _context.Exercise.Where(x => x.ExerciseId == id).ToList()[0];
        }

        public object GetExerciseRecords(string search, string sort, string order, int limit, int offset)
        {
            List<Exercise> exerciseList = new List<Exercise>();

            var uncompletedexerciseList = _context.Exercise.AsQueryable();

            if (!string.IsNullOrEmpty(sort) && string.IsNullOrEmpty(order))
            {
                uncompletedexerciseList = uncompletedexerciseList.OrderBy(x => x.GetType().GetProperty(sort).GetValue(x));
            }

            if (!string.IsNullOrEmpty(search))
            {
                uncompletedexerciseList = uncompletedexerciseList.Where(x => x.CaloriesBurnPerUnit.ToString().Contains(search)
                                || x.ExerciseName.Contains(search)
                                || x.ExerciseId.ToString().Contains(search)
                                || x.Measurement.Contains(search));
            }

            exerciseList = uncompletedexerciseList.Skip(offset * limit).Take(limit).ToList();
            int total = _context.Exercise.Count();

            var jsonData = new { total = total, rows = exerciseList };
            return jsonData;
        }

        public List<Exercise> GetAllExerciseRecords()
        {
            return _context.Exercise.ToList();
        }

        public List<ExerciseRoutines> GetRoutine(int userId)
        {
            return _context.ExerciseRoutines.Where(x => x.UserId == userId).ToList();
        }

        public List<string> GetUniqueRoutineDays(int userId)
        {
            return _context.ExerciseRoutines.Where(x => x.UserId == userId).Select(y => y.Days).Distinct().ToList();
        }
        public List<int> GetUniqueDaysCount(int userId)
        {
            List<int> result = new List<int>();
            var qryList = from row in _context.ExerciseRoutines
                      where row.UserId == userId
                      group row by row.Days
                      into grp
                      select new { Count = grp.Select(x => x.Days).Count() };

            foreach (var obj in qryList)
                result.Add(obj.Count);

            return result;
        }

        public string UpdateRoutines(List<ExerciseRoutines> exerciseRoutines, int userId)
        {
            try
            {
                _context.ExerciseRoutines.RemoveRange(_context.ExerciseRoutines.Where(x => x.UserId == userId));
                _context.ExerciseRoutines.AddRange(exerciseRoutines);

                _context.SaveChanges();
                return "true";
            }
            catch (Exception e)
            {
                return "An error occurred while registering your record. Try again later.";
            }
        }

        //--------------- Calories Intake methods ---------------

        public string CalculateCalories(int userId)
        {
            // Calculate calories intake
            List<Meals> todayMeals = GetTodayMealAdded(userId);
            double totalCaloriesIntake = todayMeals.Aggregate(0d, (total, y) => total + (y.Quantity * y.FoodDetails.Calories));

            string day = DateTime.Today.DayOfWeek.ToString();
            List<ExerciseRoutines> todayExerciseRoutines = _context.ExerciseRoutines.Where(x => x.UserId == userId).Where(x => x.Days.Contains(day)).ToList();
            double totalCaloriesBurned = 1800 + todayExerciseRoutines.Aggregate(0d, (total, y) => total + (y.Intensity * y.ExerciseDetails.CaloriesBurnPerUnit));

            // Save to db
            CaloriesIntakes newRecord = new CaloriesIntakes 
            { Date = DateTime.Today, 
            UserId = userId, 
            Day = DateTime.Today.DayOfWeek.ToString(), 
            CaloriesIntake = totalCaloriesIntake, 
            CaloriesBurned = totalCaloriesBurned };

            try
            {
                if (_context.CaloriesIntakes.Any(x => x.UserId == userId && x.Date == DateTime.Today))
                    _context.CaloriesIntakes.Update(newRecord);
                else
                    _context.CaloriesIntakes.Add(newRecord);
                _context.SaveChanges();

                return "true";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "An error occurred while registering your record. Try again later.";
            }

        }

        enum Weekdays
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }
        public List<CaloriesIntakes> GetChartCaloriesIntake(int userId)
        {
            DateTime maxDay = DateTime.Today.AddDays(-1 * (7 + (DateTime.Today.DayOfWeek - DayOfWeek.Monday)) % 7).Date;

            List<CaloriesIntakes> resultList = _context.CaloriesIntakes.Where(x => x.UserId == userId && x.Date > maxDay).Skip(0).Take(7).ToList();
            int frontIndex = (int)(DayOfWeek) Enum.Parse(typeof(Weekdays), resultList[0].Day);
            int backIndex = (int)(DayOfWeek)Enum.Parse(typeof(Weekdays), resultList[resultList.Count-1].Day);

            //If not 7 records fill rest with empty records
            for (var i = frontIndex-1; i >= 0; i -= 1)
            {
                resultList.Insert(0, new CaloriesIntakes() { Day = ((Weekdays) i).ToString() });
            }
            for (var i = 0; i < (resultList.Count-1) - backIndex; i++)
            {
                resultList.Add(new CaloriesIntakes() { Day = ((Weekdays) resultList.Count + i).ToString() });
            }

            return resultList;
        }

        public object GetCaloriesIntakeRecord(int userId, string search, string sort, string order, int limit, int offset)
        {
            List<CaloriesIntakes> caloriesIntakeList = new List<CaloriesIntakes>();
            var uncompletedList = _context.CaloriesIntakes.Where(m => m.UserId == userId);

            if (!String.IsNullOrEmpty(sort) && String.IsNullOrEmpty(order))
            {
                uncompletedList = uncompletedList.OrderBy(x => x.GetType().GetProperty(sort).GetValue(x));
                Console.WriteLine(uncompletedList);
            }

            if (!String.IsNullOrEmpty(search))
            {
                uncompletedList = uncompletedList.Where(x => x.Date.ToString().Contains(search)
                                || x.CaloriesBurned.ToString().Contains(search)
                                || x.CaloriesIntake.ToString().Contains(search)
                                || (x.CaloriesIntake - x.CaloriesBurned).ToString().Contains(search));
            }

            // Offset & limit 7 by DATE
            DateTime maxDay = DateTime.Today.AddDays(-1 * (7 + (DateTime.Today.DayOfWeek - DayOfWeek.Monday)) % 7).Date;
            int actualLimit = uncompletedList.Where(x => x.Date > maxDay).ToList().Count;

            caloriesIntakeList = uncompletedList.Skip(offset * actualLimit).Take(actualLimit).ToList();

            List<CaloriesIntakeRecord> recordList = new List<CaloriesIntakeRecord>();
            foreach (var row in caloriesIntakeList)
            {
                CaloriesIntakeRecord record = new CaloriesIntakeRecord();
                record.Date = row.Date.ToShortDateString() + "<br />" + row.Date.DayOfWeek.ToString();
                record.CaloriesIntake = row.CaloriesIntake.ToString();
                record.CaloriesBurned = row.CaloriesBurned.ToString();
                double overallIntake = row.CaloriesIntake - row.CaloriesBurned;
                if (overallIntake > 0)
                {
                    record.NetGainLoss = "+";
                }

                record.NetGainLoss += overallIntake;
                recordList.Add(record);
            }
            double averageIntake = recordList.Aggregate(0d, (total, x) => total + double.Parse(x.NetGainLoss.Substring(1)) ) / recordList.Count;
            if (averageIntake > 0)
            {
                recordList.Add(new CaloriesIntakeRecord() { CaloriesBurned = "Week's average:", NetGainLoss = "+" + averageIntake });
            }
            else if (averageIntake == 0)
            {
                recordList.Add(new CaloriesIntakeRecord() { CaloriesBurned = "Week's average:", NetGainLoss = averageIntake.ToString() });
            }

            var jsonData = new { total = recordList.Count, rows = recordList };
            return jsonData;

        }

        public string[] EvaluateCalories(int userId, int option)
        {
            object rawRecord = GetCaloriesIntakeRecord(userId, "", "", "", 7, 0);
            List<CaloriesIntakeRecord> record = (List<CaloriesIntakeRecord>) rawRecord.GetType().GetProperty("rows").GetValue(rawRecord, null);
            string[] result;

            if (record.Count == 0)
            {
                if (option == 1)
                {
                    return new string[] { "We do not have your records yet to calculate your calories intake for the week", "text-secondary", "" };
                }
                else
                {
                    return new string[] { "We do not have your records yet to calculate your calories intake for the week", "text-secondary"};
                }
            }

            string averageValueStr = record[record.Count - 1].NetGainLoss;
            if (averageValueStr.Substring(0, 1) == "+")
            {
                averageValueStr.Substring(1);
            }

            double averageValue = double.Parse(averageValueStr);
            if (-200 < averageValue && averageValue < 1200)
            {
                result = new string []{ "<i class=\"fa-solid fa-circle-check\"></i> Your calories intake is within acceptable range", "text-success", ""};
                if (option == 1)
                {
                    result[2] += "alert-success";
                    return result;
                }
                else
                {
                    result[0] += " Keep up the good work!";
                    return result;
                }
            }
            else if (-600 < averageValue && averageValue < -200 || 1200 < averageValue && averageValue < 1600)
            {
                result = new string[] { "<i class=\"fa-solid fa-triangle-exclamation\"></i> Your calories intake is slightly out of the acceptable range.", "text-warning" };
                if (option == 1)
                {
                    result[2] += "alert-warning";
                    return result;
                }
                else
                {
                    result[0] += " May bring up concerns over the long run if no action is taken";
                    return result;
                }
            }
            else
            {
                result = new string[] { "<i class=\"fa-solid fa-triangle-exclamation\"></i> Your calories intake is not acceptable.", "text-danger" };
                if (option == 1)
                {
                    result[0] += "Immediate actions required";
                    result[2] += "alert-danger";
                    return result;
                }
                else
                {
                    result[0] += "Please readjust your food consumption and exercise routines or it might raise health concerns";
                    return result;
                }
            }
        }

        public class CaloriesIntakeRecord
        {
            public string Date { get; set; }
            public string CaloriesIntake { get; set; }
            public string CaloriesBurned { get; set; }
            public string NetGainLoss { get; set; }
        }
    }

}
