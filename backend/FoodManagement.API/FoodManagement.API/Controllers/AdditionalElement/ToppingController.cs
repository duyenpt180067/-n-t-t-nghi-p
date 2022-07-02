
using FoodManagement.Core.Consts;
using FoodManagement.Core.Entities.AdditionalElement;
using FoodManagement.Core.General.Entities;
using Microsoft.AspNetCore.Mvc;
using FoodManagement.Core.Interfaces.Repository;
using FoodManagement.Core.Interfaces.Service;

namespace FoodManagement.API.Controllers.AdditionalElement
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ToppingController : BaseSubjectController<Topping>
    {
        #region Fields
        IBaseRepository<Topping> _ToppingRepository;
        IBaseService<Topping> _ToppingService;
        #endregion
        public ToppingController(IBaseRepository<Topping> ToppingRepository, IBaseService<Topping> ToppingService) :base(ToppingRepository, ToppingService)
        {
            _ToppingRepository = ToppingRepository;
            _ToppingService = ToppingService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }

    }
}
