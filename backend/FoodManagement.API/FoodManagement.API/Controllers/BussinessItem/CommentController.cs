
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
using FoodManagement.Core.Interfaces.Repository;
using FoodManagement.Core.Interfaces.Service;
using Newtonsoft.Json;

namespace FoodManagement.API.Controllers.BussinessItem
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CommentController : BaseSubjectController<Comment>
    {
        #region Fields
        IBaseRepository<Comment> _CommentRepository;
        IBaseService<Comment> _CommentService;
        #endregion
        public CommentController(IBaseRepository<Comment> CommentRepository, IBaseService<Comment> CommentService) : base(CommentRepository, CommentService)
        {
            _CommentRepository = CommentRepository;
            _CommentService = CommentService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }

        /// <summary>
        /// Them moi du lieu
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="lstEntity">truyen du lieu muon them</param>
        /// <returns>status code va serviceResult</returns>
        [HttpPost("AddMultiComment")]
        public IActionResult AddMultiComment([FromBody] List<Comment> lstEntity)
        {
            try
            {
                //var listComment = JsonConvert.DeserializeObject<List<Comment>>(lstEntity);
                var listComment = lstEntity;
                serviceResult = _baseService.AddMultiObj(listComment);
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
                res = new ServiceResult(false, _const.ErrorException, e, Core.Properties.Resources.ExceptionError);
                return StatusCode(500, res);
            }
        }
    }
}
