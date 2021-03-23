using BatteryApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<List<Category>> Get()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Categories.ToListAsync();
        }

        public async Task<List<Category>> Get(string userId)
        {
            using var context = _contextFactory.CreateDbContext();
            var categories = await context.Categories.ToListAsync();
            return categories.Where(x => x.OwnerId == userId).ToList();
        }

        public async Task<Category> Get(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var category = await context.Categories.FindAsync(id);
            return category;
        }

        public async Task<Category> GetByName(string name)
        {
            using var context = _contextFactory.CreateDbContext();
            var categories = await context.Categories.ToListAsync();
            var category = categories.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
            return category;
        }

        public async Task<Category> Add(Category category)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> Update(Category category)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Entry(category).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return category;
        }

        public async Task Delete(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var category = await context.Categories.FindAsync(id);
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }
}
