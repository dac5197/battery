using BatteryApp.Data;
using BatteryApp.Models.BatteryModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.CategoryModel
{
    public class CategoryService : ICategoryService
    {

        private readonly IDbContextFactory<AppDbContextFactory> _contextFactory;

        public CategoryService(IDbContextFactory<AppDbContextFactory> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Category>> GetAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Categories.ToListAsync();
        }

        public async Task<List<Category>> GetAsync(Battery battery)
        {
            using var context = _contextFactory.CreateDbContext();
            var categories = await context.Categories.ToListAsync();
            return categories.Where(x => x.BatteryId == battery.Id).ToList();
        }

        public async Task<List<Category>> GetAsync(string userId)
        {
            using var context = _contextFactory.CreateDbContext();
            var categories = await context.Categories.ToListAsync();
            return categories.Where(x => x.OwnerId == userId).ToList();
        }

        public async Task<Category> GetAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var category = await context.Categories.FindAsync(id);
            return category;
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            using var context = _contextFactory.CreateDbContext();
            var categories = await context.Categories.ToListAsync();
            var category = categories.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
            return category;
        }

        public async Task<Category> GetDefaultChargeCategoryAsync(int batteryId)
        {
            using var context = _contextFactory.CreateDbContext();
            var categories = await context.Categories.ToListAsync();
            var category = categories.Where(x => x.BatteryId == batteryId)
                                     .Where(x => x.IsDefaultChargeCategory == true)
                                     .FirstOrDefault();
            return category;
        }

        public async Task<Category> GetDefaultChildCategoryAsync(int batteryId)
        {
            using var context = _contextFactory.CreateDbContext();
            var categories = await context.Categories.ToListAsync();
            var category = categories.Where(x => x.BatteryId == batteryId)
                                     .Where(x => x.IsDefaultChildCategory == true)
                                     .FirstOrDefault();
            return category;
        }

        public List<Category> GetDefaultValues()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\data\\batterydefaults.json");
            var json = File.ReadAllText(file);
            dynamic jsonObject = JsonConvert.DeserializeObject(json);

            List<Category> categories = new();

            foreach (var item in jsonObject["Categories"])
            {
                Category category = new()
                {  
                    Name = item["Name"],
                    Icon = item["Icon"],
                    IconColor = item["IconColor"],
                    IsDefaultChargeCategory = item["IsDefaultChargeCategory"],
                    IsDefaultChildCategory = item["IsDefaultChildCategory"],
                };

                categories.Add(category);
            }

            return categories;
        }

        public List<string> GetIconList()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\data\\OpenIconicList.csv");

            List<string> icons = File.ReadAllLines(file)
                                     .Skip(1)
                                     .ToList();

            return icons;

        }

        public async Task<Category> AddAsync(Category category)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(category).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var category = await context.Categories.FindAsync(id);
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }
}
