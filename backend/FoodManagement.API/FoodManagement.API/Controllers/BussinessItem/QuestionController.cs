
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
    public class QuestionController : BaseController<Question>
    {
        #region Fields
        IQuestionRepository _QuestionRepository;
        IQuestionService _QuestionService;
        #endregion
        public QuestionController(IQuestionRepository QuestionRepository, IQuestionService QuestionService) :base(QuestionRepository, QuestionService)
        {
            _QuestionRepository = QuestionRepository;
            _QuestionService = QuestionService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }

    }
}
