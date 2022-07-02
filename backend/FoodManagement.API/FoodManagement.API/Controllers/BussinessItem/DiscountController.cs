
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
    public class DiscountController : BaseSubjectController<Discount>
    {
        #region Fields
        IBaseRepository<Discount> _DiscountRepository;
        IBaseService<Discount> _DiscountService;
        #endregion
        public DiscountController(IBaseRepository<Discount> DiscountRepository, IBaseService<Discount> DiscountService) : base(DiscountRepository, DiscountService)
        {
            _DiscountRepository = DiscountRepository;
            _DiscountService = DiscountService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }

    }
}
