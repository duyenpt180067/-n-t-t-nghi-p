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
    [DisplayName("Người dùng")]
    public class User : BaseEntity
    {
        [DisplayName("Id người dùng")]
        [PrimaryKey]
        public Guid UserId { get; set; }
        [RequiredAttr]
        [DisplayName("Tên đăng nhập")]
        [LogAudit]
        [Duplicated]
        public string UserName { get; set; }
        [MaxLengthAttr(20)]
        [Duplicated]
        [DisplayName("Mã nhân viên")]
        [StartWith("UC-")]
        [IsCode]
        public string UserCode { get; set; }
        [RequiredAttr]
        [DisplayName("Mật khẩu")]
        public string Pass { get; set; }
        [MaxLengthAttr(100)]
        [DisplayName("Họ tên")]
        public string FullName { get; set; }
        [PhoneNumberAttr]
        [RequiredAttr]
        [Duplicated]
        [DisplayName("Số điện thoại")]
        [LogAudit]
        public string Phone { get; set; }
        [LogAudit]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        [EmailFormat]
        [DisplayName("Email")]
        [LogAudit]
        public string Email { get; set; }
        public int? Gender { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsEmployee { get; set; }
        public string Position { get; set; }
        public string Permission { get; set; }
        public int? UserStatus { get; set; }
        public string UserToken { get; set; }
	}
}
