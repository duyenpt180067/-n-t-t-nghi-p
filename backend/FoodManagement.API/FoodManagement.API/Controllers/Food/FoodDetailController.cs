
using FoodManagement.Core.Consts;
using FoodManagement.Core.Entities.FMFood;
using FoodManagement.Core.General.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMFood;
using FoodManagement.Core.Interfaces.Infrastructure.IService.FMFood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodManagement.Core.Interfaces.Repository;
using FoodManagement.Core.Interfaces.Service;

namespace FoodManagement.API.Controllers.FMFood
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FoodDetailController : BaseSubjectController<FoodDetail>
    {
        #region Fields
        IBaseRepository<FoodDetail> _FoodDetailRepository;
        IBaseService<FoodDetail> _FoodDetailService;
        #endregion
        public FoodDetailController(IBaseRepository<FoodDetail> FoodDetailRepository, IBaseService<FoodDetail> FoodDetailService) :base(FoodDetailRepository, FoodDetailService)
        {
            _FoodDetailRepository = FoodDetailRepository;
            _FoodDetailService = FoodDetailService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }
    }
}
