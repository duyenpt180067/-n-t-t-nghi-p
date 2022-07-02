using FoodManagement.Core.Entities.FMOrder;
using FoodManagement.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMOrder
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public string UpdateOrder(OrderMerge OrderMerge);
        public string AddOrder(OrderMerge OrderMerge);
        public OrderMerge GetOrderMerge(string OrderCode);
        public string EmployeeUpdateOrder(int OrderStatus, Guid OrderId, string OrderReason);
    }
}
