using FoodManagement.Core.Entities.AdditionalElement;
using FoodManagement.Core.Entities.BussinessItem;
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
    public class OrderDetail : BaseEntity
    {
        [DisplayName("Id đơn hàng chi tiết")]
        [PrimaryKey]
        public Guid OrderDetailId { get; set; }
        [RequiredAttr]
        [DisplayName("Đơn hàng")]
        public Guid OrderId { get; set; }
        [RequiredAttr]
        [DisplayName("Thực phẩm")]
        public Guid? FoodDetailId { get; set; }
        public string ListTopping { get; set; }
        public List<Topping> Toppings { get; set; }
        public Comment Comment { get; set; }
        public Guid? DiscountId { get; set; }
        public Guid? FoodDiscountId { get; set; }
        public Guid? CartDetailId { get; set; }
        
        public int? Quantity { get; set; }
        public int? Amount { get; set; }
        public string FoodName { get; set; }
        public string FoodCode { get; set; }
        public string ImageURL { get; set; }
        public int? UnitPrice { get; set; }
        public string SizeName { get; set; }
        public string SizeCode { get; set; }
        public string DiscountCode { get; set; }
        public DateTime? DiscountStartDate { get; set; }
        public DateTime? DiscountEndDate { get; set; }
        public float? DiscountAmount { get; set; }
        public int? DiscountMaxAmount { get; set; }
        public string FoodDiscountCode { get; set; }
        public DateTime? FoodDiscountStartDate { get; set; }
        public DateTime? FoodDiscountEndDate { get; set; }
        public float? FoodDiscountAmount { get; set; }
        public int? FoodDiscountMaxAmount { get; set; }
        public string JsonData { get; set; }
    }

    public class OrderDetailNotComment : BaseEntity {
        [DisplayName("Id đơn hàng chi tiết")]
        [PrimaryKey]
        public Guid OrderDetailId { get; set; }
        [RequiredAttr]
        [DisplayName("Đơn hàng")]
        public Guid OrderId { get; set; }
        [RequiredAttr]
        [DisplayName("Thực phẩm")]
        public Guid? FoodDetailId { get; set; }
        public string ListTopping { get; set; }
        public List<Topping> Toppings { get; set; }
        public Guid? DiscountId { get; set; }
        public Guid? FoodDiscountId { get; set; }
        public Guid? CartDetailId { get; set; }

        [LogAudit]
        [DisplayName("Số lượng")]
        public int? Quantity { get; set; }
        [LogAudit]
        [DisplayName("Thành tiền")]
        [IsNumber]
        public int? Amount { get; set; }
        [LogAudit]
        [DisplayName("Tên món")]
        public string FoodName { get; set; }
        [LogAudit]
        [DisplayName("Mã món ăn")]
        public string FoodCode { get; set; }
        public string ImageURL { get; set; }
        [LogAudit]
        [DisplayName("Đơn giá gốc")]
        public int? UnitPrice { get; set; }
        [LogAudit]
        [DisplayName("Kích thước")]
        public string SizeName { get; set; }
        [LogAudit]
        [DisplayName("Mã kích thước")]
        public string SizeCode { get; set; }
        [LogAudit]
        [DisplayName("Mã giảm giá trên món")]
        public string DiscountCode { get; set; }
        [LogAudit]
        [DisplayName("Ngày bắt đầu")]
        public DateTime? DiscountStartDate { get; set; }
        [LogAudit]
        [DisplayName("Ngày kết thúc")]
        public DateTime? DiscountEndDate { get; set; }
        [LogAudit]
        [DisplayName("% giảm giá")]
        public float? DiscountAmount { get; set; }
        [LogAudit]
        [DisplayName("Giảm giá tối đa")]
        [IsNumber]
        public int? DiscountMaxAmount { get; set; }
        /*[LogAudit]
        [DisplayName("Mã giảm giá trên món")]
        public string FoodDiscountCode { get; set; }
        public DateTime? FoodDiscountStartDate { get; set; }
        public DateTime? FoodDiscountEndDate { get; set; }
        [LogAudit]
        [DisplayName("% giảm giá trên món")]
        public float? FoodDiscountAmount { get; set; }
        public int? FoodDiscountMaxAmount { get; set; }*/
        public string JsonData { get; set; }
    }
}
