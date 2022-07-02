
using FoodManagement.Core.Consts;
using FoodManagement.Core.Entities.FMOrder;
using FoodManagement.Core.General.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMOrder;
using FoodManagement.Core.Interfaces.Infrastructure.IService.FMOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace FoodManagement.API.Controllers.FMOrder
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : BaseSubjectController<Order>
    {
        #region Fields
        IOrderRepository _OrderRepository;
        IOrderService _OrderService;
        #endregion
        public OrderController(IOrderRepository OrderRepository, IOrderService OrderService) :base(OrderRepository, OrderService)
        {
            _OrderRepository = OrderRepository;
            _OrderService = OrderService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }
        [HttpGet("GetByCode/{OrderCode}")]
        public IActionResult GetOrderMergeByCode(string OrderCode)
        {
            try
            {
                var entity = _OrderRepository.GetOrderMerge(OrderCode);
                if (entity != null)
                {
                    return Ok(JsonSerializer.Serialize(entity, new JsonSerializerOptions
                    {
                        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = true
                    }));
                }
                else
                {
                    var mgs = "Mã code = '" + OrderCode + "'";
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
        /// Them moi du lieu
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="entity">truyen du lieu muon them</param>
        /// <returns>status code va serviceResult</returns>
        [HttpPost]
        [Route("AddOrder")]
        public IActionResult AddOrder([FromBody] OrderMerge entity)
        {
            try
            {
                serviceResult = _OrderService.AddOrder(entity);
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
                        return StatusCode(500, serviceResult);
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
        /// <param name="OrderMerge">truyen du lieu muon cap nhat</param>
        /// <returns>status code va serviceResult</returns>
        [HttpPut]
        [Route("UpdateOrder")]
        public IActionResult UpdateOrder([FromBody] OrderMerge OrderMerge)
        {
            try
            {
                serviceResult = _OrderService.UpdateOrder(OrderMerge);

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

        /// <summary>
        /// Cap nhat du lieu
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="OrderMerge">truyen du lieu muon cap nhat</param>
        /// <returns>status code va serviceResult</returns>
        [HttpPut]
        [Route("UpdateEmployeeOrder")]
        public IActionResult EmployeeUpdateOrder([FromBody] Order order)
        {
            try
            {
                int rowEffects = 0;
                var row = _OrderRepository.EmployeeUpdateOrder((int)order.OrderStatus, order.OrderId, order.OrderReason);

                if (Int32.TryParse(row, out rowEffects) == false)
                {
                    res.ResultCode = serviceResult.ResultCode;
                    res.UserMgs = serviceResult.UserMgs;
                    return StatusCode(500, res);
                }
                // 2. Sua thanh cong
                else
                {
                    serviceResult.Success = true;
                    serviceResult.UserMgs = Core.Properties.Resources.PutSuccess;
                    //serviceResult.Data = (int)serviceResult.Data;
                    serviceResult.Data = row;
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
