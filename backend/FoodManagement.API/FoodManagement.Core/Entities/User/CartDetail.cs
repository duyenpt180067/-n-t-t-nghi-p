using FoodManagement.Core.Entities.AdditionalElement;
using FoodManagement.Core.Entities.FMFood;
using FoodManagement.Core.General.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagement.Core.Entities.FMUser
{
    [DisplayName("Giỏ hàng")]
    public class CartDetail: BaseEntity
    {
        [PrimaryKey]
        [DisplayName("Id cart")]
        public Guid? CartDetailId { get; set; }
        [RequiredAttr]
        [DisplayName("Khách hàng")]
        public string UserName { get; set; }

        [RequiredAttr]
        [DisplayName("Thực phẩm")]
        public Guid? FoodDetailId { get; set; }
        public string ListTopping { get; set; }
        public List<Topping> Toppings { get; set; } = null;
        public Guid? DiscountId { get; set; }

        public List<FoodDetail> ListFoodDetailSame { get; set; } = null;

        [LogAudit]
        [DisplayName("Số lượng")]
        public int? Quantity { get; set; }
        [LogAudit]
        [DisplayName("Thành tiền")]
        public int? Amount { get; set; }
        [LogAudit]
        [DisplayName("Mã món")]
        public string FoodName { get; set; }
        [LogAudit]
        [DisplayName("Tên món ăn")]
        public string FoodCode { get; set; }
        public Guid? FoodId { get; set; }
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
        public List<Topping> ListOrgTopping { get; set; } = null;
    }
}
