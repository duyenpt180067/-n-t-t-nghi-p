using FoodManagement.Core.General.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FoodManagement.Core.General.Entities.BaseEntity;

namespace FoodManagement.Core.Entities.FMFood
{
    [DisplayName("Món ăn")]
    public class Food : BaseEntity
    {
        [DisplayName("Id food")]
        [PrimaryKey]
        public Guid FoodId { get; set; }
        [DisplayName("Danh mục")]
        public Guid? CategoryId { get; set; }
        [RequiredAttr]
        [DisplayName("Tên thực phẩm")]
        public string FoodName { get; set; }
        [MaxLengthAttr(20)]
        [Duplicated]
        [RequiredAttr]
        [DisplayName("Mã thực phẩm")]
        [StartWith("FC-")]
        [IsCode]
        [LogAudit]
        public string FoodCode { get; set; }
        [LogAudit]
        [DisplayName("Mô tả")]
        public string Descriptions { get; set; }
        [LogAudit]
        [DisplayName("Tiêu đề")]
        public string Title { get; set; }
        public int? FoodStatus { get; set; }
        public Guid? DiscountId { get; set; }
        [LogAudit]
        [DisplayName("Lượt xem")]
        public int? FoodView { get; set; }
        [LogAudit]
        [DisplayName("Số sao")]
        public float? FoodStar { get; set; } = 0;
        [LogAudit]
        [DisplayName("Link hình ảnh")]
        public string ImageURL { get; set; }
        [LogAudit]
        [DisplayName("Tên danh mục")]
        public string CategoryName { get; set; }
        [LogAudit]
        [DisplayName("Mã danh mục")]
        public string CategoryCode { get; set; }
        public string UserName { get; set; }
        public string UserCode { get; set; }
        public string SizeName { get; set; }
        public string SizeCode { get; set; }
        public string FullName { get; set; }
        [LogAudit]
        [DisplayName("Mã giảm giá")]
        public string DiscountCode { get; set; }
        public string DiscountTitle { get; set; }
        [LogAudit]
        [DisplayName("% giảm giá")]
        public float? DiscountAmount { get; set; }
        [LogAudit]
        [DisplayName("Giảm tối đa")]
        [IsNumber]
        public int? DiscountMaxAmount { get; set; }
        [LogAudit]
        [DisplayName("Ngày bắt đầu")]
        public DateTime? DiscountStartDate { get; set; }
        [LogAudit]
        [DisplayName("Ngày kết thúc")]
        public DateTime? DiscountEndDate { get; set; }
        [LogAudit]
        [DisplayName("Đơn giá gốc")]
        [IsNumber]
        public int? Amount { get; set; }
        public string ListTopping { get; set; }
    }
}
