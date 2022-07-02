using FoodManagement.Core.Consts;
using FoodManagement.Core.Entities.FMUser;
using FoodManagement.Core.General.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodManagement.Core.Interfaces.Infrastructure.IService.FMUser;
using Newtonsoft.Json;

namespace FoodManagement.API.Controllers.FMUser
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : BaseSubjectController<User>
    {
        #region Fields
        IUserRepository _UserRepository;
        IUserService _UserService;
        #endregion
        public UserController(IUserRepository UserRepository, IUserService UserService) :base(UserRepository, UserService)
        {
            _UserRepository = UserRepository;
            _UserService = UserService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }

        /// <summary>
        /// lấy tất cả các object
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <returns>statusCode và serviceResult</returns>
        [HttpPost("Login")]
        public IActionResult Login(User user)
        {
            try
            {
                var result = _UserService.Login(user.UserName, user.Pass);
                return Ok(result);
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
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(dynamic param)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<User>(param.user.ToString());
                var userName = param.userName.ToString();
                serviceResult = _UserService.UpdateUser(userName, user);

                // 1. Validate du lieu (trong hoac bi trung)
                if (serviceResult.Success == false)
                {
                    // id khong dung dinh dang
                    if (serviceResult.ResultCode == _const.IdInvalid)
                    {
                        return BadRequest(serviceResult);
                    }
                    // Dữ liệu không hợp lệ
                    else if ((serviceResult.ResultCode == _const.DataInvalid) || (serviceResult.ResultCode == _const.Incurred) || (serviceResult.ResultCode == _const.Exist))
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
    }
}
