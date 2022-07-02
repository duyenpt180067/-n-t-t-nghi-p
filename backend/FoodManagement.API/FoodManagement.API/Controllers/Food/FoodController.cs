
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
using FoodManagement.Core.Interfaces.Service;
using System.Text.Json;

namespace FoodManagement.API.Controllers.FMFood
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FoodController : BaseSubjectController<Food>
    {
        #region Fields
        IFoodRepository _FoodRepository;
        IFoodService _FoodService;
        #endregion
        public FoodController(IFoodRepository FoodRepository, IFoodService FoodService) : base(FoodRepository, FoodService)
        {
            _FoodRepository = FoodRepository;
            _FoodService = FoodService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }

        [HttpGet("GetByCode/{FoodCode}")]
        public IActionResult GetFoodMergeByCode(string FoodCode)
        {
            try
            {
                var entity = _FoodRepository.GetFoodMerge(FoodCode);
                if (entity != null)
                {
                    return Ok(JsonSerializer.Serialize(entity, new JsonSerializerOptions {
                        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = true
                    }));
                }   
                else
                {
                    var mgs = "Mã code = '" + FoodCode + "'";
                    res.UserMgs = string.Format(Core.Properties.Resources.DataNotExist, mgs);
                    res.MISACode = _const.NotExist;
                    return StatusCode(200, res);
                }
            }
            catch (Exception e)
            {
                res = new ServiceResult(false, _const.ErrorException, e, Core.Properties.Resources.ExceptionError);
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// lấy tất cả các object
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <returns>statusCode và serviceResult</returns>
        [HttpPost("GetPopularFood")]
        public IActionResult GetPopularFood([FromBody] Guid CategoryId)
        {
            try
            {
                var entities = _FoodRepository.GetPopularFood(CategoryId);
                if (entities.Count > 0)
                {
                    return Ok(JsonSerializer.Serialize(entities, new JsonSerializerOptions
                    {
                        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = true
                    }));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                res = new ServiceResult(false, _const.ErrorException, e, Core.Properties.Resources.ExceptionError);
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Them moi du lieu
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="entity">truyen du lieu muon them</param>
        /// <returns>status code va serviceResult</returns>
        [HttpPost]
        [Route("AddFood")]
        public IActionResult AddFood([FromBody] FoodMerge entity)
        {
            try
            {
                serviceResult = _FoodService.AddFood(entity);
                // neu success = false
                if (serviceResult.Success == false)
                {
                    // id khong dung dinh dang
                    if (serviceResult.ResultCode == _const.IdInvalid)
                    {
                        return BadRequest(serviceResult);
                    }
                    // Dữ liệu không hợp lệ
                    else if (serviceResult.ResultCode == _const.DataInvalid)
                    {
                        return Ok(serviceResult);
                    }
                    // xay ra exception
                    else
                    {
                        res.ResultCode = serviceResult.ResultCode;
                        res.UserMgs = serviceResult.UserMgs;
                        return StatusCode(500, res);
                    }
                }
                // neu success = true
                else
                {
                    serviceResult.Data = 1;
                    return StatusCode(201, serviceResult);
                }
            }
            catch (Exception e)
            {
                serviceResult.UserMgs = Core.Properties.Resources.ExceptionError;
                serviceResult.ResultCode = _const.ErrorException;
                res = new ServiceResult(false, _const.ErrorException, e, Core.Properties.Resources.ExceptionError);
                return StatusCode(500, res);
            }
        }
        /// <summary>
        /// Cap nhat du lieu
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="FoodMerge">truyen du lieu muon cap nhat</param>
        /// <returns>status code va serviceResult</returns>
        [HttpPut]
        [Route("UpdateFood")]
        public IActionResult UpdateFood([FromBody] FoodMerge FoodMerge)
        {
            try
            {

                serviceResult = _FoodService.UpdateFood(FoodMerge);

                // 1. Validate du lieu (trong hoac bi trung)
                if (serviceResult.Success == false)
                {
                    // id khong dung dinh dang
                    if (serviceResult.ResultCode == _const.IdInvalid)
                    {
                        return BadRequest(serviceResult);
                    }
                    // Dữ liệu không hợp lệ
                    else if (serviceResult.ResultCode == _const.DataInvalid)
                    {
                        return Ok(serviceResult);
                    }
                    // xay ra exception
                    else
                    {
                        res.ResultCode = serviceResult.ResultCode;
                        res.UserMgs = serviceResult.UserMgs;
                        return StatusCode(500, res);
                    }
                }
                // 2. Sua thanh cong
                else
                {
                    serviceResult.UserMgs = Core.Properties.Resources.PutSuccess;
                    //serviceResult.Data = (int)serviceResult.Data;
                    serviceResult.Data = 1;
                    return StatusCode(202, serviceResult);
                }

            }
            catch (Exception e)
            {
                serviceResult.UserMgs = Core.Properties.Resources.ExceptionError;
                serviceResult.ResultCode = _const.ErrorException;
                res = new ServiceResult(false, _const.ErrorException, e, Core.Properties.Resources.ExceptionError);
                return StatusCode(500, res);
            }
        }
    }
}

