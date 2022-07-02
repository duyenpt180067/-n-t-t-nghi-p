using FoodManagement.Core.Consts;
using FoodManagement.Core.Entities.FMOrder;
using FoodManagement.Core.General.Entities;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMOrder;
using FoodManagement.Core.Interfaces.Infrastructure.IService.FMOrder;
using FoodManagement.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagement.Core.Service.FMOrder
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        IOrderRepository _OrderRepository;
        public OrderService(IOrderRepository OrderRepository) : base(OrderRepository)
        {
            _OrderRepository = OrderRepository;
        }
        public ServiceResult AddOrder(OrderMerge OrderMerge)
        {
            var listBankAccount = OrderMerge.OrderDetails;
            Order Order = OrderMerge.Order;
            Order.EntityState = EntityState.Add;
            var isValidate = Validate(Order);
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
                var res = _OrderRepository.AddOrder(OrderMerge);
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

        public ServiceResult UpdateOrder(OrderMerge OrderMerge)
        {
            Guid valueId;
            try
            {
                valueId = Guid.Parse(OrderMerge.Order.OrderId.ToString());
            }
            catch (Exception)
            {
                serviceResult.Success = false;
                serviceResult.UserMgs = string.Format(Properties.Resources.DataNotExist, "");
                serviceResult.ResultCode = _const.NotExist;
                return serviceResult;
            }
            // kiểm tra xem đối tượng có id là guid có trong db không
            var entityCheck = _OrderRepository.GetObjById(valueId);
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
                OrderMerge.Order.EntityState = EntityState.Update;
                var isValidate = Validate(OrderMerge.Order);
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
                    var res = _OrderRepository.UpdateOrder(OrderMerge);
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
