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
    [DisplayName("Tương tác")]
    public class Contact : BaseEntity
    {
        [DisplayName("Id Mục iên hệ")]
        [PrimaryKey]
        public Guid ContactId { get; set; }
        [RequiredAttr]
        [DisplayName("Người dùng")]
        public Guid UserId { get; set; }
        public string ContactTitle { get; set; }
        public string ContactContent { get; set; }
        public string ContactToEmail { get; set; }
        public int? ContactStatus { get; set; }
        public string UserName { get; set; }
        public string UserCode { get; set; }
        public bool? IsEmployee { get; set; }
    }
}
