using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodManagement.Core;
using FoodManagement.Core.Consts;
using FoodManagement.Core.General.Entities;
using FoodManagement.Core.Interfaces.Repository;
using FoodManagement.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace FoodManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseSubjectController<TEntity> : ControllerBase
    {
        #region Fields
        protected IBaseRepository<TEntity> _baseRepository;
        protected IBaseService<TEntity> _baseService;
        protected ServiceResult serviceResult;
        protected dynamic res;
        protected Core.Consts.EnumResource _const;
        #endregion
        public BaseSubjectController(IBaseRepository<TEntity> baseRepository, IBaseService<TEntity> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
            serviceResult = new ServiceResult();
            _const = new Core.Consts.EnumResource();
            res = new ExpandoObject();
        }

        #region Methods
        /// <summary>
        /// lấy tất cả các object
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <returns>statusCode và serviceResult</returns>
        [HttpGet]
        public IActionResult GetAllObj()
        {
            try
            {
                var entities = _baseRepository.GetAllObj();
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
        /// lay entities theo id
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="Id">Guid</param>
        /// <returns>status code va ket qua tra ve</returns>
        [HttpGet("{Id}")]
        public IActionResult GetObjById(string Id)
        {
            try
            {
                Guid entityId;
                if (Guid.TryParse(Id, out entityId))
                {
                    var entity = _baseRepository.GetObjById(entityId);
                    if (entity != null)
                    {
                        return Ok(entity);
                    }
                    else
                    {
                        var mgs = "id = '" + Id + "'";
                        res.UserMgs = string.Format(Core.Properties.Resources.DataNotExist, mgs);
                        res.Code = _const.NotExist;
                        return StatusCode(200, res);
                    }
                }
                else
                {
                    var mgs = "id = '" + Id + "'";
                    serviceResult.UserMgs = string.Format(Core.Properties.Resources.DataNotExist, mgs);
                    serviceResult.ResultCode = _const.NotExist;
                    return StatusCode(400, serviceResult);
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
        public virtual IActionResult AddObj([FromBody] TEntity entity)
        {
            try
            {
                serviceResult = _baseService.AddObj(entity);
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
                    // Dữ liệu bị trùng
                    else if (serviceResult.ResultCode == _const.Exist)
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
                    return StatusCode(201, serviceResult);
                }
            }
            catch (Exception e)
            {
                res = new ServiceResult(false, _const.ErrorException, e, Core.Properties.Resources.ExceptionError);
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Cap nhat du lieu
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="entity">truyen du lieu muon cap nhat</param>
        /// <returns>status code va serviceResult</returns>
        [HttpPut]
        public IActionResult UpdateObj([FromBody] TEntity entity)
        {
            try
            {
                serviceResult = _baseService.UpdateObj(entity);

                // 1. Validate du lieu (trong hoac bi trung)
                if (serviceResult.Success == false)
                {
                    // id khong dung dinh dang
                    if (serviceResult.ResultCode == _const.IdInvalid)
                    {
                        return BadRequest(serviceResult);
                    }
                    // Dữ liệu không hợp lệ
                    else if ((serviceResult.ResultCode == _const.DataInvalid) || (serviceResult.ResultCode == _const.Incurred))
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
                res = new ServiceResult(false, _const.ErrorException, e, Core.Properties.Resources.ExceptionError);
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Xoa du lieu theo id
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="listEntityId">mang id cua entity muon xoa</param>
        /// <returns>status code va serviceResult</returns>
        [HttpDelete("DeleteMulti")]
        public IActionResult DeleteMultiObj(dynamic param)
        {
            try
            {
                List<string> listEntityId = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(param.listEntityId.ToString());
                serviceResult = _baseService.DeleteMultiObj(listEntityId, param.userName.ToString());
                if (serviceResult.Success == false)
                {
                    //return BadRequest(serviceResult);
                    int rowEffect;
                    // nếu dữ liệu hợp lệ
                    if (Int32.TryParse(serviceResult.Data.ToString(), out rowEffect) == false)
                    {
                        res = new ServiceResult(false, serviceResult.ResultCode, serviceResult.Data, serviceResult.UserMgs);
                        return StatusCode(400, res);
                    }
                    // nếu dữ liệu hợp lệ nhưng việc xóa nhiều bị lỗi
                    else return StatusCode(200, serviceResult);
                }
                else
                {
                    var rowAffect = (int)serviceResult.Data;
                    return StatusCode(203, rowAffect);
                }
            }
            catch (Exception e)
            {
                res = new ServiceResult(false, _const.ErrorException, e, Core.Properties.Resources.ExceptionError);
                return StatusCode(500, res);
            }
        }

        [HttpPost("GetFilterPaging")]
        public virtual IActionResult GetFilterPaging(JObject parameter)
        {
            try
            {
                //var param = JsonSerializer.Deserialize(parameter);
                var entities = _baseRepository.GetFilterPaging(parameter);
                return Ok(entities);
            }
            catch (Exception e)
            {
                res = new ServiceResult(false, _const.ErrorException, e, Core.Properties.Resources.ExceptionError);
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// lay entities theo id
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="Id">Guid</param>
        /// <returns>status code va ket qua tra ve</returns>
        [HttpGet("GetLayoutConfig")]
        public IActionResult GetLayoutConfig()
        {
            try
            {
                var entity = _baseRepository.GetLayoutConfig();
                return Ok(entity);
            }
            catch (Exception e)
            {
                res = new ServiceResult(false, _const.ErrorException, e, Core.Properties.Resources.ExceptionError);
                return StatusCode(500, res);
            }
        }

        [HttpPost("GetObjByCode")]
        public IActionResult GetObjByCode([FromBody] string code)
        {
            try
            {
                var entities = _baseRepository.GetByCode(code);
                if (entities != null)
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
        /// lay entities theo id
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="Id">Guid</param>
        /// <returns>status code va ket qua tra ve</returns>
        [HttpGet("NewCode")]
        public IActionResult GetNewCode()
        {
            try
            {
                var entity = _baseRepository.GetNewCode();
                return Ok(entity);
            }
            catch (Exception e)
            {
                res = new ServiceResult(false, _const.ErrorException, e, Core.Properties.Resources.ExceptionError);
                return StatusCode(500, res);
            }
        }
        #endregion
    }
}
