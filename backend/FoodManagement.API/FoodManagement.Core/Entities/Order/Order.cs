using FoodManagement.Core.General.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FoodManagement.Core.General.Entities.BaseEntity;

namespace FoodManagement.Core.Entities.FMOrder
{
    [DisplayName("Đơn hàng")]
    public class Order : BaseEntity
    {
        [DisplayName("Id đơn hàng")]
        [PrimaryKey]
        public Guid OrderId { get; set; }
        [RequiredAttr]
        [DisplayName("Khách hàng")]
        public Guid? UserId { get; set; }
        [RequiredAttr]
        [DisplayName("Tên đơn hàng")]
        public string OrderName { get; set; }
        [MaxLengthAttr(20)]
        [Duplicated]
        [RequiredAttr]
        [DisplayName("Mã đơn hàng")]
        [LogAudit]
        [StartWith("OC-")]
        [IsCode]
        public string OrderCode { get; set; }
        [LogAudit]
        [DisplayName("Tổng tiền")]
        [IsNumber]
        public int? TotalAmount { get; set; }
        [PhoneNumberAttr]
        [RequiredAttr]
        [DisplayName("Số điện thoại")]
        [LogAudit]
        public string Phone { get; set; }
        [PhoneNumberAttr]
        [RequiredAttr]
        [DisplayName("Địa chỉ")]
        [LogAudit]
        public string Address { get; set; }
        [LogAudit]
        [DisplayName("Trạng thái đơn hàng")]
        public int? OrderStatus { get; set; }
        [LogAudit]
        [DisplayName("Lý do hủy")]
        public string OrderReason { get; set; }
        [LogAudit]
        [DisplayName("Khách hàng")]
        public string UserName { get; set; }
        public string UserCode { get; set; }
        public Guid? DiscountId { get; set; }
        [LogAudit]
        [DisplayName("Mã giảm giá")]
        public string DiscountCode { get; set; }
        public DateTime? DiscountStartDate { get; set; }
        public DateTime? DiscountEndDate { get; set; }
        [LogAudit]
        [DisplayName("% giảm giá")]
        public float? DiscountAmount { get; set; }
        [LogAudit]
        [DisplayName("Giảm tối đa")]
        [IsNumber]
        public int? DiscountMaxAmount { get; set; }
        public bool? IsRated { get; set; } = false;
    }
}
