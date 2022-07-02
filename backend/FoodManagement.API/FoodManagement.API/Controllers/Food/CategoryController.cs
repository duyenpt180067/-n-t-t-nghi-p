
using FoodManagement.Core.Consts;
using FoodManagement.Core.Entities.FMFood;
using FoodManagement.Core.General.Entities;
using Microsoft.AspNetCore.Mvc;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMFood;
using FoodManagement.Core.Interfaces.Service;
using System;
using System.Text.Json;

namespace FoodManagement.API.Controllers.FMFood
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : BaseSubjectController<Category>
    {
        #region Fields
        ICategoryRepository _CategoryRepository;
        IBaseService<Category> _CategoryService;
        #endregion
        public CategoryController(ICategoryRepository CategoryRepository, IBaseService<Category> CategoryService) :base(CategoryRepository, CategoryService)
        {
            _CategoryRepository = CategoryRepository;
            _CategoryService = CategoryService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }
        /// <summary>
        /// lấy tất cả các object
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <returns>statusCode và serviceResult</returns>
        [HttpGet("GetPopularCategories")]
        public IActionResult GetPopularCategories()
        {
            try
            {
                var entities = _CategoryRepository.GetPopularCategories();
                if (entities.Count > 0)
                {
                    return Ok(JsonSerializer.Serialize(entities, new JsonSerializerOptions
                    {
                        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = true
                    }));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                res = new ServiceResult(false, _const.ErrorException, e, Core.Properties.Resources.ExceptionError);
                return StatusCode(500, res);
            }
        }

        [HttpPost("GetByCode")]
        public IActionResult GetCategoryByCode([FromBody] string categoryCode)
        {
            try
            {
                var entities = _CategoryRepository.GetCategoryByCode(categoryCode);
                if (entities.Count > 0)
                {
                    return Ok(JsonSerializer.Serialize(entities, new JsonSerializerOptions
                    {
                        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = true
                    }));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                res = new ServiceResult(false, _const.ErrorException, e, Core.Properties.Resources.ExceptionError);
                return StatusCode(500, res);
            }
        }
    }
}
