using FoodManagement.Core.General.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FoodManagement.Core.General.Entities.BaseEntity;

namespace FoodManagement.Core.Entities.AdditionalElement
{
    [DisplayName("Kích thước")]
    public class Size : BaseEntity
    {
        [DisplayName("Id cỡ")]
        [PrimaryKey]
        public Guid SizeId { get; set; }
        [MaxLengthAttr(20)]
        [Duplicated]
        [RequiredAttr]
        [DisplayName("Mã kích cỡ")]
        [StartWith("SC-")]
        [IsCode]
        [LogAudit]
        public string SizeCode { get; set; }
        [LogAudit]
        [DisplayName("Kích cỡ")]
        public string SizeName { get; set; }
        [LogAudit]
        [DisplayName("Trạng thái kích cỡ")]
        public int? SizeStatus { get; set; }
    }
}
