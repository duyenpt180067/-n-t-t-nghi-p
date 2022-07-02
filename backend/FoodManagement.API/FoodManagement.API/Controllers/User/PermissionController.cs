using FoodManagement.Core.Consts;
using FoodManagement.Core.Entities.FMUser;
using FoodManagement.Core.General.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMUser;
using FoodManagement.Core.Interfaces.Infrastructure.IService.FMUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodManagement.API.Controllers.FMUser
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PermissionController : BaseController<Permission>
    {
        #region Fields
        IPermissionRepository _PermissionRepository;
        IPermissionService _PermissionService;
        #endregion
        public PermissionController(IPermissionRepository PermissionRepository, IPermissionService PermissionService) :base(PermissionRepository, PermissionService)
        {
            _PermissionRepository = PermissionRepository;
            _PermissionService = PermissionService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }

    }
}
