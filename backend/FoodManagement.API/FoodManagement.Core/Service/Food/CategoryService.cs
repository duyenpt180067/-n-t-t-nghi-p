using FoodManagement.Core.Entities.FMFood;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMFood;
using FoodManagement.Core.Interfaces.Infrastructure.IService.FMFood;
using FoodManagement.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagement.Core.Service.FMCategory
{
    public class CategoryService: BaseService<Category>, ICategoryService
    {
        ICategoryRepository _CategoryRepository;
        public CategoryService(ICategoryRepository CategoryRepository):base(CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }
    }
}
