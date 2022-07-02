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
    public class Action : BaseEntity
    {
        [DisplayName("Id hành động")]
        [PrimaryKey]
        public Guid ActionId { get; set; }
        [RequiredAttr]
        [DisplayName("Mã hành động")]
        [MaxLengthAttr(20)]
        [Duplicated]
        [StartWith("AC-")] 
        [IsCode]
        public string ActionCode { get; set; }
        public string ActionName { get; set; }
    }
}
