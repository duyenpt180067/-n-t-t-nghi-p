
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

namespace FoodManagement.API.Controllers.FMOrder
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderDetailController : BaseController<OrderDetail>
    {
        #region Fields
        IOrderDetailRepository _OrderDetailRepository;
        IOrderDetailService _OrderDetailService;
        #endregion
        public OrderDetailController(IOrderDetailRepository OrderDetailRepository, IOrderDetailService OrderDetailService) :base(OrderDetailRepository, OrderDetailService)
        {
            _OrderDetailRepository = OrderDetailRepository;
            _OrderDetailService = OrderDetailService;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
        }

    }
}
