using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FoodManagement.Core.Entities.FMFood;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMFood;

namespace FoodManagement.Repository.Repository.FMUser
{
    public class CategoryRepository : DBContext<Category>, ICategoryRepository
    {
        public List<Category> GetPopularCategories()
        {
            var entities = DBConnection.Query<Category>($"Proc_Category_PopularDish", commandType: CommandType.StoredProcedure).ToList();
            return entities;
        }

        public List<Category> GetCategoryByCode(string categoryCode)
        {
            var entities = DBConnection.Query<Category>($"Proc_Category_PopularDish", commandType: CommandType.StoredProcedure).ToList();
            return entities;
        }
    }
}
