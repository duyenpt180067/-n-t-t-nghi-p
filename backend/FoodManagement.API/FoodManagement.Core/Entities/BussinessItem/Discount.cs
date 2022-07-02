using FoodManagement.Core.General.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FoodManagement.Core.General.Entities.BaseEntity;

namespace FoodManagement.Core.Entities.BussinessItem
{
    [DisplayName("Mã giảm giá")]
    public class Discount : BaseEntity
    {
        [DisplayName("Id Discount")]
        [PrimaryKey]
        public Guid DiscountId { get; set; }
        [RequiredAttr]
        [DisplayName("Mã giảm giá")]
        [MaxLengthAttr(20)]
        [Duplicated]
        [StartWith("DC-")]
        [IsCode]
        [LogAudit]
        public string DiscountCode { get; set; }
        [LogAudit]
        [DisplayName("Tiêu đề")]
        public string DiscountTitle { get; set; }
        //[RequiredAttr]
        [DisplayName("Lý do giảm giá")]
        public Guid? DiscountConditionId { get; set; }
        // 0-giảm giá trên món (của cửa hàng), 1-áp mã khuyến mãi
        [LogAudit]
        [DisplayName("Loại giảm giá")]
        public int? DiscountType { get; set; }
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
        public string DiscountConditionCode { get; set; }
        public int? DiscountConditionMin { get; set; }
        public int? DiscountConditionMax { get; set; }
        public string DiscountConditionReason { get; set; }
        public string DiscountConditionFor { get; set; }
    }
}