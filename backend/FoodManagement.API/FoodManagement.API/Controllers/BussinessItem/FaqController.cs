
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
    public class FaqController : BaseSubjectController<Faq>
    {
        #region Fields
        IBaseRepository<Faq> _FaqRepository;
        IBaseService<Faq> _FaqService;
        #endregion
        public FaqController(IBaseRepository<Faq> FaqRepository, IBaseService<Faq> FaqService) : base(FaqRepository, FaqService)
        {
            _FaqRepository = FaqRepository;
            _FaqService = FaqService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }

    }
}
