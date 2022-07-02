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
    [DisplayName("Điều kiện giảm giá")]
    public class DiscountCondition : BaseEntity
    {
        [DisplayName("Id DiscountCondition")]
        [PrimaryKey]
        public Guid DiscountConditionId { get; set; }
        [RequiredAttr]
        [DisplayName("Mã điều kiện giảm giá")]
        [MaxLengthAttr(20)]
        [Duplicated]
        [StartWith("DCC-")]
        [IsCode]
        public string DiscountConditionCode { get; set; }
        public string DiscountConditionName { get; set; }
        public int? DiscountConditionMin { get; set; }
        public int? DiscountConditionMax { get; set; }
        public string DiscountConditionReason { get; set; }
        // điều kiện giảm cho đơn hàng hay user
        public string DiscountConditionFor { get; set; }
    }
}
