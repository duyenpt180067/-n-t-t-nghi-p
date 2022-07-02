
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
    public class SliderController : BaseSubjectController<Slider>
    {
        #region Fields
        IBaseRepository<Slider> _SliderRepository;
        IBaseService<Slider> _SliderService;
        #endregion
        public SliderController(IBaseRepository<Slider> SliderRepository, IBaseService<Slider> SliderService) : base(SliderRepository, SliderService)
        {
            _SliderRepository = SliderRepository;
            _SliderService = SliderService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }

    }
}
