using FoodManagement.Core.General.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FoodManagement.Core.General.Entities.BaseEntity;

namespace FoodManagement.Core.Entities
{
    public class Subject : BaseEntity
    {
        [DisplayName("Id quyền hạn")]
        [PrimaryKey]
        public Guid SubjectId { get; set; }
        [RequiredAttr]
        [DisplayName("Mã đối tượng")]
        [MaxLengthAttr(20)]
        [Duplicated]
        [StartWith("SC-")]
        public string SubjectCode { get; set; }
        [RequiredAttr]
        [DisplayName("Tên đối tượng")]
        public string SubjectName { get; set; }
        [RequiredAttr]
        [DisplayName("Id đối tượng cha")]
        public Guid? SubjectParentId { get; set; }
    }
}
