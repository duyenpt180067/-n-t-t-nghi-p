using FoodManagement.Core.Consts;
using FoodManagement.Core.Entities.FMFood;
using FoodManagement.Core.General.Entities;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMFood;
using FoodManagement.Core.Interfaces.Infrastructure.IService.FMFood;
using FoodManagement.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagement.Core.Service.FMFood
{
    public class FoodService : BaseService<Food>, IFoodService
    {
        IFoodRepository _FoodRepository;
        public FoodService(IFoodRepository FoodRepository) : base(FoodRepository)
        {
            _FoodRepository = FoodRepository;
        }
        public ServiceResult AddFood(FoodMerge FoodMerge)
        {
            var listBankAccount = FoodMerge.FoodDetails;
            Food Food = FoodMerge.Food;
            Food.EntityState = EntityState.Add;
            var isValidate = Validate(Food);
            // nếu validate bị sai
            if (isValidate == false)
            {
                serviceResult.Success = false;
                return serviceResult;
            }
            // validate đúng
            else
            {
                // số lượng bản ghi bị ảnh hưởng
                int rowEffects;
                //kết quả trả về của việc thêm mới dữ liệu vào db
                var res = _FoodRepository.AddFood(FoodMerge);
                // nếu kết quả trả về không convert sang được số thì trả về exception
                if (Int32.TryParse(res, out rowEffects) == false)
                {
                    serviceResult.ResultCode = _const.ErrorException;
                    serviceResult.UserMgs = Properties.Resources.ExceptionError;
                    serviceResult.Success = false;
                    serviceResult.Data = res;
                    return serviceResult;
                }
                //nếu convert sang được số thì trả về số lượng bản ghi bị tác động
                else
                {
                    serviceResult.ResultCode = _const.Success;
                    serviceResult.UserMgs = Properties.Resources.PostSuccess;
                    serviceResult.Success = true;
                    serviceResult.Data = rowEffects;
                    return serviceResult;
                }
            }
        }

        public ServiceResult UpdateFood(FoodMerge FoodMerge)
        {
            Guid valueId;
            try
            {
                valueId = Guid.Parse(FoodMerge.Food.FoodId.ToString());
            }
            catch (Exception)
            {
                serviceResult.Success = false;
                serviceResult.UserMgs = string.Format(Properties.Resources.DataNotExist, "");
                serviceResult.ResultCode = _const.NotExist;
                return serviceResult;
            }
            if(FoodMerge.FoodDetails.Count == 0)
            {
                serviceResult.Success = false;
                serviceResult.UserMgs = "Bạn chưa thêm kích thước món!";
                serviceResult.ResultCode = _const.NotExist;
                return serviceResult;
            }
            // kiểm tra xem đối tượng có id là guid có trong db không
            var entityCheck = _FoodRepository.GetObjById(valueId);
            //nếu không trả về không có dữ liệu
            if (entityCheck == null)
            {
                serviceResult.Success = false;
                serviceResult.UserMgs = string.Format(Properties.Resources.DataNotExist, "");
                serviceResult.ResultCode = _const.NotExist;
                return serviceResult;
            }
            // nếu có
            else
            {
                // gán state của entity = update
                FoodMerge.Food.EntityState = EntityState.Update;
                var isValidate = Validate(FoodMerge.Food);
                // dữ liệu khoogn hợp lệ
                if (isValidate == false)
                {
                    serviceResult.Success = false;
                    return serviceResult;
                }
                // dữ liệu hợp lệ
                else
                {
                    // res là kết quả của việc update dữ liệu
                    var res = _FoodRepository.UpdateFood(FoodMerge);
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
                        serviceResult.UserMgs = Properties.Resources.DeleteSuccess;
                        serviceResult.Success = true;
                        serviceResult.Data = rowEffects;
                        return serviceResult;
                    }
                }
            }
        }
    }
}
