using FoodManagement.Core.Consts;
using FoodManagement.Core.General.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagement.Core.Consts
{
    public class EnumResource
    {
        #region ResultCode
        // thanh cong khi them moi, cap nhat, xoa du lieu
        public string Success = "FM-Success";
        // loi 500
        public string ErrorException = "FM-Exception";
        // loi du lieu trống
        public string NullError = "FM-NullError";
        // lỗi mã trùng
        public string Exist = "FM-Exist";
        // lỗi mã không tồn tại
        public string NotExist = "FM-NotExist";
        // loi max length
        public string MaxLength = "FM-MaxLength";
        //loi du lieu khong hop le: pageNumber<0, ma khach hang/ ma nhan vien khong bat dau bang KH-/NV-
        public string DataInvalid = "FM-DataInvalid";
        // loi email khong dung dinh dang
        public string EmailInValid = "FM-EmailInValid";
        // loi id khong đúng định dạng
        public string IdInvalid = "FM-IdInvalid";
        // ngày không được nhỏ hơn hiện tại
        public string DateInvalid = "FM-DateInvalid";
        // dữ liệu đã có phát sinh
        public string Incurred = "FM-Incurred";
        // đăng nhập thất bại
        public string LoginFail = "FM-LoginFail";
        #endregion
    }

    #region EntityState
    /// <summary>
    /// trang thai cua entity
    /// </summary>
    public enum EntityState
    {
        Get = 0,
        Add = 1,
        Update = 2,
        Delete = 3
    }
    #endregion
    #region GenderName
    public enum NameGender
    {
        Male = 1,
        Female = 0,
        Other = 2
    }
    public enum UserStatus
    {
        // Còn hoạt động
        Active = 1,
        // Bị hủy tài khoản
        Cancel = 0,
        // Dừng hoạt động
        StopActive = 2,
    }
    #endregion
    #region CategoryStatus
    /// <summary>
    /// Trạng thái của danh mục, thực phẩm, comment
    /// </summary>
    public enum CategoryStatus
    {
        // Còn hoạt động/hiển thị
        Active = 1,
        // Dừng hoạt động/không hiển thị
        StopActive = 0,
    }
    #endregion
    #region DiscountConditionOperation
    /// <summary>
    /// So sánh điều kiện
    /// </summary>
    public enum DiscountConditionOperation
    {
        // >=
        More = 0,
        // <=
        Less = 1,
        // Range
        Range = 2,
        // =
        Equal = 3,
    }
    #endregion
    #region OrderStatus
    //0 - Processing, 2 - Accept, 1 - Reject, 3 - WaitingToTake, 4 - Delivering, 5 - Complete
    public enum OrderStatus
    {
        // Đang xử lý
        Processing = 0,
        // Từ chối
        Reject = 1,
        // Chấp nhận
        Accept = 2,
        // Chờ lấy hàng
        WaitingToTake = 3,
        // Đang giao hàng
        Delivering = 4,
        // Hoàn thành
        Complete = 5,
    }
    #endregion
}