
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
    public class DiscountConditionController : BaseSubjectController<DiscountCondition>
    {
        #region Fields
        IBaseRepository<DiscountCondition> _DiscountConditionRepository;
        IBaseService<DiscountCondition> _DiscountConditionService;
        #endregion
        public DiscountConditionController(IBaseRepository<DiscountCondition> DiscountConditionRepository, IBaseService<DiscountCondition> DiscountConditionService) : base(DiscountConditionRepository, DiscountConditionService)
        {
            _DiscountConditionRepository = DiscountConditionRepository;
            _DiscountConditionService = DiscountConditionService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }

    }
}
