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
    [DisplayName("Danh mục")]
    public class Category : BaseEntity
    {
        [DisplayName("Id danh mục")]
        [PrimaryKey]
        public Guid CategoryId { get; set; }
        [MaxLengthAttr(20)]
        [Duplicated]
        [RequiredAttr]
        [DisplayName("Mã danh mục")]
        [StartWith("CM-")]
        [IsCode]
        [LogAudit]
        public string CategoryCode { get; set; }
        [RequiredAttr]
        [DisplayName("Tên danh mục")]
        [LogAudit]
        public string CategoryName { get; set; }
        [LogAudit]
        [DisplayName("Link hình ảnh")]
        public string CategoryImage { get; set; }
        [LogAudit]
        [DisplayName("Mô tả")]
        public string Descriptions { get; set; } 
        public Guid? CategoryParent { get; set; }
        public int? CategoryStatus { get; set; }
    }
}
