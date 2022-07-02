
using FoodManagement.Core.Consts;
using FoodManagement.Core.Entities.BussinessItem;
using FoodManagement.Core.General.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.BussinessItem;
using FoodManagement.Core.Interfaces.Infrastructure.IService.BussinessItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodManagement.API.Controllers.BussinessItem
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AnswerController : BaseController<Answer>
    {
        #region Fields
        IAnswerRepository _AnswerRepository;
        IAnswerService _AnswerService;
        #endregion
        public AnswerController(IAnswerRepository AnswerRepository, IAnswerService AnswerService) :base(AnswerRepository, AnswerService)
        {
            _AnswerRepository = AnswerRepository;
            _AnswerService = AnswerService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }

    }
}
