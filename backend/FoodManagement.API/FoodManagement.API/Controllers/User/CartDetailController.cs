
using FoodManagement.Core.Consts;
using FoodManagement.Core.General.Entities;
using Microsoft.AspNetCore.Mvc;
using FoodManagement.Core.Interfaces.Repository;
using FoodManagement.Core.Interfaces.Service;
using FoodManagement.Core.Entities.FMUser;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMUser;
using System;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace FoodManagement.API.Controllers.FMUser
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartDetailController : BaseSubjectController<CartDetail>
    {
        #region Fields
        ICartDetailRepository _CartDetailRepository;
        IBaseService<CartDetail> _CartDetailService;
        #endregion
        public CartDetailController(ICartDetailRepository CartDetailRepository, IBaseService<CartDetail> CartDetailService) :base(CartDetailRepository, CartDetailService)
        {
            _CartDetailRepository = CartDetailRepository;
            _CartDetailService = CartDetailService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }

        [HttpPost("GetFilterPaging")]
        public override IActionResult GetFilterPaging(JObject parameter)
        {
            try
            {
                //var param = JsonSerializer.Deserialize(parameter);
                var entities = _CartDetailRepository.GetFilterPaging(parameter);
                return Ok(JsonSerializer.Serialize(entities, new JsonSerializerOptions
                {
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                }));
            }
            catch (Exception e)
            {
                res = new ServiceResult(false, _const.ErrorException, e, Core.Properties.Resources.ExceptionError);
                return StatusCode(500, res);
            }
        }
    }
}
