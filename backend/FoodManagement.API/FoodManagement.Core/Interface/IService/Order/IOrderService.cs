using FoodManagement.Core.Entities.FMOrder;
using FoodManagement.Core.General.Entities;
using FoodManagement.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagement.Core.Interfaces.Infrastructure.IService.FMOrder
{
    public interface IOrderService : IBaseService<Order>
    {
        public ServiceResult AddOrder(OrderMerge OrderMerge);
        public ServiceResult UpdateOrder(OrderMerge OrderMerge);
    }
}
