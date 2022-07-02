
using FoodManagement.Core.Consts;
using FoodManagement.Core.Entities.BussinessItem;
using FoodManagement.Core.General.Entities;
using Microsoft.AspNetCore.Mvc;
using FoodManagement.Core.Interfaces.Repository;
using FoodManagement.Core.Interfaces.Service;

namespace FoodManagement.API.Controllers.BussinessItem
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BlogController : BaseSubjectController<Blog>
    {
        #region Fields
        IBaseRepository<Blog> _BlogRepository;
        IBaseService<Blog> _BlogService;
        #endregion
        public BlogController(IBaseRepository<Blog> BlogRepository, IBaseService<Blog> BlogService) :base(BlogRepository, BlogService)
        {
            _BlogRepository = BlogRepository;
            _BlogService = BlogService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }

    }
}
