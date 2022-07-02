using FoodManagement.Core.Entities.FMFood;
using FoodManagement.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMFood
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        public List<Category> GetPopularCategories();
        public List<Category> GetCategoryByCode(string categoryCode);
    }
}
