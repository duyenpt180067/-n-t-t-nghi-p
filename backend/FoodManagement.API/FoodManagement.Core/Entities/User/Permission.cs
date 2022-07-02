using FoodManagement.Core.General.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FoodManagement.Core.General.Entities.BaseEntity;

namespace FoodManagement.Core.Entities.FMUser
{
    public class Permission : BaseEntity
    {
        [DisplayName("Id quyền hạn")]
        [PrimaryKey]
        public Guid PermissionId { get; set; }
        [RequiredAttr]
        [DisplayName("Id hành động")]
        public Guid? ActionId { get; set; }
        [RequiredAttr]
        [DisplayName("Id đối tượng")]
        public Guid? SubjectId { get; set; }
    }
}
