
using FoodManagement.Core.Consts;
using FoodManagement.Core.Entities.AdditionalElement;
using FoodManagement.Core.General.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.AdditionalElement;
using FoodManagement.Core.Interfaces.Infrastructure.IService.AdditionalElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodManagement.Core.Interfaces.Repository;
using FoodManagement.Core.Interfaces.Service;

namespace FoodManagement.API.Controllers.AdditionalElement
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SizeController : BaseSubjectController<Size>
    {
        #region Fields
        IBaseRepository<Size> _SizeRepository;
        IBaseService<Size> _SizeService;
        #endregion
        public SizeController(IBaseRepository<Size> SizeRepository, IBaseService<Size> SizeService) :base(SizeRepository, SizeService)
        {
            _SizeRepository = SizeRepository;
            _SizeService = SizeService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }

    }
}
