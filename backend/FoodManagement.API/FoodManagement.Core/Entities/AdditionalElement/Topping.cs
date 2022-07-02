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
    [DisplayName("Topping")]
    public class Topping : BaseEntity
    {
        [DisplayName("Id topping")]
        [PrimaryKey]
        public Guid ToppingId { get; set; }
        [LogAudit]
        [DisplayName("Tên Topping")]
        public string ToppingName { get; set; }
        [MaxLengthAttr(20)]
        [Duplicated]
        [RequiredAttr]
        [DisplayName("Mã Topping")]
        [StartWith("TC-")]
        [IsCode]
        [LogAudit]
        public string ToppingCode { get; set; }
        [LogAudit]
        [DisplayName("Link hình ảnh")]
        public string ToppingImage { get; set; }
        [LogAudit]
        [DisplayName("Giá Topping")]
        [IsNumber]
        public int? ToppingAmount { get; set; }

    }
}
