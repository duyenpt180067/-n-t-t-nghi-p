
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
    public class AccessLogController : BaseSubjectController<AccessLog>
    {
        #region Fields
        IBaseRepository<AccessLog> _AccessLogRepository;
        IBaseService<AccessLog> _AccessLogService;
        #endregion
        public AccessLogController(IBaseRepository<AccessLog> AccessLogRepository, IBaseService<AccessLog> AccessLogService) :base(AccessLogRepository, AccessLogService)
        {
            _AccessLogRepository = AccessLogRepository;
            _AccessLogService = AccessLogService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }

    }
}
