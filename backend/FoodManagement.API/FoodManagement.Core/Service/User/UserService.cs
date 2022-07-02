using FoodManagement.Core.Consts;
using FoodManagement.Core.Entities.FMUser;
using FoodManagement.Core.General.Entities;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMUser;
using FoodManagement.Core.Interfaces.Infrastructure.IService.FMUser;
using FoodManagement.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagement.Core.Service.FMUser
{
    public class UserService: BaseService<User>, IUserService
    {
        IUserRepository _UserRepository;
        public UserService(IUserRepository UserRepository):base(UserRepository)
        {
            _UserRepository = UserRepository;
        }

        public ServiceResult AddObj(CartDetail entity)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Login(string userName, string pass)
        {
            try
            {
                var res = _UserRepository.Login(userName, pass);
                string msg = Properties.Resources.LoginFail;
                string resultCode = _const.LoginFail;
                if(res != null)
                {
                    msg = Properties.Resources.LoginSuccess;
                    resultCode = _const.Success;
                }
                return new ServiceResult(true, resultCode, res, msg);
            }
            catch(Exception e)
            {
                return new ServiceResult(false, _const.ErrorException, e, Properties.Resources.ExceptionError);
            }
        }

        public ServiceResult UpdateUser(string userName, User user)
        {
            var orgUser = _UserRepository.GetUserByUserName(userName);
            if(orgUser != null)
            {
                orgUser.UserName = user.UserName;
                orgUser.Phone = user.Phone;
                orgUser.Pass = user.Pass!= null ? _UserRepository.GetMD5(user.Pass): orgUser.Pass;
                orgUser.Address = user.Address;
                orgUser.EntityState = EntityState.Update;
                var isValid = Validate(orgUser);
                // dữ liệu khoogn hợp lệ
                if (isValid == false)
                {
                    serviceResult.Success = false;
                    return serviceResult;
                }
                else
                {
                    // res là kết quả của việc update dữ liệu
                    var res = _UserRepository.UpdateObj(orgUser, user, orgUser);
                    int rowEffects;
                    // nếu res không convert sang int được thì trả về exception
                    if (Int32.TryParse(res, out rowEffects) == false)
                    {
                        serviceResult.ResultCode = _const.ErrorException;
                        serviceResult.UserMgs = Properties.Resources.ExceptionError;
                        serviceResult.Success = false;
                        serviceResult.Data = res;
                        return serviceResult;
                    }
                    // nếu được thì trả về số lượng bản ghi bị tác động
                    else
                    {
                        serviceResult.ResultCode = _const.Success;
                        serviceResult.UserMgs = Properties.Resources.PutSuccess;
                        serviceResult.Success = true;
                        serviceResult.Data = rowEffects;
                        return serviceResult;
                    }
                }
            }
            else
            {
                serviceResult.Success = false;
                serviceResult.UserMgs = string.Format(Properties.Resources.DataNotExist, "Khách hàng này");
                serviceResult.Data = string.Format(Properties.Resources.DataNotExist, "Khách hàng này");
                serviceResult.ResultCode = _const.NotExist;
                return serviceResult;
            }
        }


        /*public ServiceResult AddUser(User User)
        {
            var checkUser = _UserRepository.GetObjByCode(User.UserCode);
            // 1. check ma nhan vien da co chua
            if (string.IsNullOrEmpty(User.UserCode))
            {
                serviceResult.Success = false;
                serviceResult.UserMgs = Properties.Resources.UserCodeNullError;
                serviceResult.MISACode = _const.MISACodeError;
                return serviceResult;
            }

            // 2. check ma nhan vien co trung khong?
            else if (checkUser != null)
            {
                serviceResult.Success = false;
                serviceResult.UserMgs = Properties.Resources.UserCodeExist;
                serviceResult.MISACode = _const.MISACodeExist;
                return serviceResult;
            }

            // 3. check email co dung dinh dang khong
            else
            {
                serviceResult.Success = true;
                serviceResult.Data = _UserRepository.AddObj(User);
                return serviceResult;
            }
        }*/
    }
}
