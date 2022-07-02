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
    [DisplayName("Chi tiết món ăn")]
    public class FoodDetail : BaseEntity
    {
        [DisplayName("Id thực phẩm chi tiết")]
        [PrimaryKey]
        public Guid FoodDetailId { get; set; }
        [RequiredAttr]
        [DisplayName("Thực phẩm")]
        public Guid FoodId { get; set; }
        public Guid? ToppingId { get; set; }
        public Guid? SizeId { get; set; }
        public int? FoodDetailStatus { get; set; }
        public int? Quantity { get; set; }
        [RequiredAttr]
        [DisplayName("Giá thực phẩm")]
        [IsNumber]
        [LogAudit]
        public int? Amount { get; set; }
        public string FoodName { get; set; }
        public string FoodCode { get; set; }
        [LogAudit]
        [DisplayName("Kích thước")]
        public string SizeName { get; set; }
        [LogAudit]
        [DisplayName("Mã kích thước")]
        public string SizeCode { get; set; }
    }
}
