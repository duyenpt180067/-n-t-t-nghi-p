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
    [DisplayName("Nhật ký truy cập")]
    public class AccessLog:BaseEntity
    {
        [DisplayName("Id topping")]
        [PrimaryKey]
        public Guid AccessLogId { get; set; }
        public string Description { get; set; }
        public string Action { get; set; }
        [RequiredAttr]
        [DisplayName("Đối tượng")]
        public string Subject { get; set; }
        public string UserCode { get; set; }
        public bool? IsEmployee { get; set; }
        public bool? IsAdmin { get; set; }
    }
}
