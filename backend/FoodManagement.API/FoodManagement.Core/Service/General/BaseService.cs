using FoodManagement.Core.Consts;
using FoodManagement.Core.Entities.FMOrder;
using FoodManagement.Core.Entities.FMUser;
using FoodManagement.Core.General.Entities;
using FoodManagement.Core.Interfaces.Repository;
using FoodManagement.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static FoodManagement.Core.General.Entities.BaseEntity;

namespace FoodManagement.Core.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity:BaseEntity
    {
        #region Fields
        protected ServiceResult serviceResult;
        protected EnumResource _const;
        IBaseRepository<TEntity> _baseRepository;
        public List<String> mgsArrayError;
        #endregion
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            serviceResult = new ServiceResult();
            _const = new EnumResource();
            mgsArrayError = new List<string>();
        }

        #region Methods

        /// <summary>
        /// Them moi du lieu
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual ServiceResult AddObj(TEntity entity)
        {
            var user = _baseRepository.GetUserByUserName(entity.UserAction);
            if (user == null)
            {
                serviceResult.Success = false;
                //serviceResult.DevMgs = "";
                serviceResult.UserMgs = string.Format(Properties.Resources.DataNotExist, entity.UserAction);
                serviceResult.ResultCode = _const.NotExist;
                serviceResult.Data = serviceResult.UserMgs;
                return serviceResult;
            }
            entity.EntityState = EntityState.Add;
            var isValidate = Validate(entity);
            // nếu validate bị sai
            if(isValidate == false)
            {
                serviceResult.Success = false;
                return serviceResult;
            } 
            // validate đúng
            else
            {
                // số lượng bản ghi bị ảnh hưởng
                int rowEffects;
                //kết quả trả về của việc thêm mới dữ liệu vào db
                var res = _baseRepository.AddObj(entity, user);
                // nếu kết quả trả về không convert sang được số thì trả về exception
                if (Int32.TryParse(res, out rowEffects) == false && typeof(TEntity).Name != typeof(User).Name)
                {
                    serviceResult.ResultCode = _const.ErrorException;
                    serviceResult.UserMgs = Properties.Resources.ExceptionError;
                    serviceResult.Success = false;
                    serviceResult.Data = res;
                    return serviceResult;
                }
                else if (typeof(TEntity).Name == typeof(User).Name)
                {
                    serviceResult.ResultCode = _const.Success;
                    serviceResult.UserMgs = Properties.Resources.PostSuccess;
                    serviceResult.Success = true;
                    serviceResult.Data = JsonSerializer.Deserialize<User>(res); ;
                    return serviceResult;
                }
                //nếu convert sang được số thì trả về số lượng bản ghi bị tác động
                else
                {
                    serviceResult.ResultCode = _const.Success;
                    serviceResult.UserMgs = Properties.Resources.PostSuccess;
                    serviceResult.Success = true;
                    serviceResult.Data = rowEffects;
                    return serviceResult;
                }
            }
            //return serviceResult;
        }

        public virtual ServiceResult AddMultiObj(List<TEntity> listEntity)
        {
            // số lượng bản ghi bị ảnh hưởng
            int rowEffects;
            //kết quả trả về của việc thêm mới dữ liệu vào db
            var res = _baseRepository.AddMultiObj(listEntity);
            // nếu kết quả trả về không convert sang được số thì trả về exception
            if (Int32.TryParse(res, out rowEffects) == false)
            {
                serviceResult.ResultCode = _const.ErrorException;
                serviceResult.UserMgs = Properties.Resources.ExceptionError;
                serviceResult.Success = false;
                serviceResult.Data = res;
                return serviceResult;
            }
            //nếu convert sang được số thì trả về số lượng bản ghi bị tác động
            else
            {
                serviceResult.ResultCode = _const.Success;
                serviceResult.UserMgs = Properties.Resources.PostSuccess;
                serviceResult.Success = true;
                serviceResult.Data = res;
                return serviceResult;
            }
        }

        /// <summary>
        /// xoa du lieu
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public virtual ServiceResult DeleteObj(string entityId, string userName)
        {
            var user = _baseRepository.GetUserByUserName(userName);
            if (user == null)
            {
                serviceResult.Success = false;
                //serviceResult.DevMgs = "";
                serviceResult.UserMgs = string.Format(Properties.Resources.DataNotExist, userName);
                serviceResult.ResultCode = _const.NotExist;
                serviceResult.Data = serviceResult.UserMgs;
                return serviceResult;
            }
            Guid guid;
            //kiểm tra dữ liệu đầu vào
            //nếu dữ liệu đầu vào chuyển sang dạng Guid được  <=> dữ liệu đầu vào hợp lệ
            if (Guid.TryParse(entityId, out guid))
            {
                // kiểm tra xem đối tượng có id là guid có trong db không
                var entityCheck = _baseRepository.GetObjById(guid);
                //nếu không trả về không có dữ liệu
                if (entityCheck == null)
                {
                    serviceResult.Success = false;
                    serviceResult.UserMgs = string.Format(Properties.Resources.DataNotExist, entityId);
                    serviceResult.ResultCode = _const.NotExist;
                    return serviceResult;
                }
                // nếu có
                else
                {
                    //gán state của đối tượng đó là delete
                    entityCheck.EntityState = EntityState.Delete;
                    // res là kết quả trả về của việc delete dữ liệu
                    var res = _baseRepository.DeleteObj(guid, user, entityCheck);
                    int rowEffects;
                    // kiểm tra xem res có convert sang kiểu int được không
                    // (res trả về so luong ban ghi bị tac dong hoac exception loi 500)
                    //nếu không thì trả về exception
                    if (Int32.TryParse(res, out rowEffects) == false)
                    {
                        serviceResult.ResultCode = _const.ErrorException;
                        serviceResult.UserMgs = Properties.Resources.ExceptionError;
                        serviceResult.Success = false;
                        serviceResult.Data = res;
                        return serviceResult;
                    }
                    //nếu có thì trả về số lượng bản ghi bị tác động
                    else
                    {
                        serviceResult.ResultCode = _const.Success;
                        serviceResult.UserMgs = Properties.Resources.DeleteSuccess;
                        serviceResult.Success = true;
                        serviceResult.Data = rowEffects;
                        return serviceResult;
                    }
                }
            }
            // dữ liệu không đúng định dạng
            else
            {
                serviceResult.Success = false;
                serviceResult.UserMgs = Properties.Resources.DataInValid;
                serviceResult.ResultCode = _const.DataInvalid;
                return serviceResult;
            }
            //return serviceResult;
        }

        /// <summary>
        /// xoa nhieu ban gi theo mang id
        /// Create: PTDuyen(03/08/2021)
        /// </summary>
        /// <param name="listEntityId">mang cac id truyen vao</param>
        /// <returns>so ban ghi bi xoa</returns>
        public ServiceResult DeleteMultiObj(List<String> listEntityId, string userName)
        {
            var user = _baseRepository.GetUserByUserName(userName);
            if(user == null) {
                serviceResult.Success = false;
                //serviceResult.DevMgs = "";
                serviceResult.UserMgs = string.Format(Properties.Resources.DataNotExist, userName);
                serviceResult.ResultCode = _const.NotExist;
                serviceResult.Data = serviceResult.UserMgs;
                return serviceResult;
            }
            List<object> listMgs = new List<object>();
            // listId chứa các id dạng Guid
            List<Guid> listId = new List<Guid>();
            // xét mỗi Id trong listEntityId được truyền vào
            foreach (var entityId in listEntityId)
            {
                Guid guid;
                // xét xem entityId có chuyển sang dạng Guid được không
                //nếu được <=> dữ liệu đầu vào hợp lệ
                if (Guid.TryParse(entityId, out guid))
                {
                    listId.Add(guid);
                    var entityCheck = _baseRepository.GetObjById(guid);
                    // kiểm tra lấy đối tượng theo id bằng null không
                    // nếu = null trả về luôn lỗi dữ liệu không hợp lệ
                    if (entityCheck == null)
                    {
                        serviceResult.Success = false;
                        //serviceResult.DevMgs = "";
                        serviceResult.UserMgs = Properties.Resources.DataInValid;
                        serviceResult.ResultCode = _const.NotExist;
                        listMgs.Add(serviceResult.UserMgs);
                        serviceResult.Data = listMgs;
                        return serviceResult;
                    }
                    // nếu khác null thì gán state của đối tượng là delete đi đến id tiếp theo của listEntityId
                    else
                    {
                        entityCheck.EntityState = EntityState.Delete;
                        serviceResult.Success = true;
                        continue;
                    }
                }
                // nếu không chuyển sang Guid được thì trả về dữ liệu không hợp lệ
                else
                {
                    serviceResult.Success = false;
                    //serviceResult.DevMgs = "";
                    serviceResult.UserMgs = Properties.Resources.DataInValid;
                    serviceResult.ResultCode = _const.NotExist;
                    listMgs.Add(serviceResult.UserMgs);
                    serviceResult.Data = listMgs;
                    return serviceResult;
                }
            }
            // success = false thì trả về luôn lỗi  <=> trogn listEntityId có dữ liệu không hợp lệ
            if (serviceResult.Success == false)
            {
                return serviceResult;
            }
            // nếu success= true <=> trogn listEntityId có tất cả dữ liệu hợp lệ
            else
            {
                // res là kết quả trả về của việc xóa nhiều bản ghi
                var res = _baseRepository.DeleteMultiObj(listId, user);
                int rowEffects;
                //kiểm tra res có chuyển về kiểu int được không
                //nêu không thì trả về exception
                if (Int32.TryParse(res, out rowEffects) == false)
                {
                    serviceResult.ResultCode = _const.ErrorException;
                    serviceResult.UserMgs = Properties.Resources.ExceptionError;
                    serviceResult.Success = false;
                    serviceResult.Data = res;
                }
                // nếu có thì trả về số lượng ản ghi bị tác động
                else
                {
                    serviceResult.ResultCode = _const.Success;
                    serviceResult.UserMgs = Properties.Resources.DeleteSuccess;
                    serviceResult.Success = true;
                    serviceResult.Data = rowEffects;
                }
                return serviceResult;

            }
        }

        /// <summary>
        /// cap nhat du lieu
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual ServiceResult UpdateObj(TEntity entity)
        {
            var user = _baseRepository.GetUserByUserName(entity.UserAction);
            if (user == null)
            {
                serviceResult.Success = false;
                //serviceResult.DevMgs = "";
                serviceResult.UserMgs = string.Format(Properties.Resources.DataNotExist, entity.UserAction);
                serviceResult.ResultCode = _const.NotExist;
                serviceResult.Data = serviceResult.UserMgs;
                return serviceResult;
            }
            var properties = entity.GetType().GetProperties();
            Guid valueId = new Guid();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(entity);
                if (property.IsDefined(typeof(PrimaryKey), false))
                {
                    try
                    {
                        valueId = Guid.Parse(property.GetValue(entity).ToString());
                    }
                    catch (Exception)
                    {

                        valueId = new Guid();
                    }
                    break;
                }
            }
            // kiểm tra xem đối tượng có id là guid có trong db không
            var entityCheck = _baseRepository.GetObjById(valueId);
            //nếu không trả về không có dữ liệu
            if (entityCheck == null)
            {
                serviceResult.Success = false;
                serviceResult.UserMgs = string.Format(Properties.Resources.DataNotExist, "");
                serviceResult.ResultCode = _const.NotExist;
                return serviceResult;
            }
            // nếu có
            else
            {
                // gán state của entity = update
                entity.EntityState = EntityState.Update;
                var isValidate = Validate(entity);
                // dữ liệu khoogn hợp lệ
                if (isValidate == false)
                {
                    serviceResult.Success = false;
                    return serviceResult;
                }
                // dữ liệu hợp lệ
                else
                {
                    // res là kết quả của việc update dữ liệu
                    var res = _baseRepository.UpdateObj(entity, user, entityCheck);
                    int rowEffects;
                    // nếu res không convert sang int được thì trả về exception
                    if (Int32.TryParse(res, out rowEffects) == false)
                    {
                        serviceResult.ResultCode = _const.ErrorException;
                        serviceResult.UserMgs = Properties.Resources.ExceptionError;
                        serviceResult.Success = false;
                        serviceResult.Data = res;
                        return serviceResult;
                    }
                    // nếu được thì trả về số lượng bản ghi bị tác động
                    else
                    {
                        serviceResult.ResultCode = _const.Success;
                        serviceResult.UserMgs = Properties.Resources.PutSuccess;
                        serviceResult.Success = true;
                        serviceResult.Data = rowEffects;
                        return serviceResult;
                    }
                }
            }
            //return serviceResult;
        }
        /// <summary>
        /// kiem tra du lieu hop le hay khong
        /// Create: PTDuyen(17/08/2021)
        /// </summary>
        /// <param name="entity">du lieu can kiem tra</param>
        /// <returns>true: hop le / false: khong hop le</returns>
        protected bool Validate(TEntity entity)
        {
            var isValidate = true;
            //Lay properties cua entity
            var properties = entity.GetType().GetProperties();
            foreach(var property in properties)
            {
                var propertyValue = property.GetValue(entity);
                // nếu là cập nhật kiểm tra đã có trong hệ thống ko
                if (entity.EntityState == EntityState.Update)
                {
                    if (property.IsDefined(typeof(PrimaryKey), false))
                    {
                        if (property.IsDefined(typeof(InheritId), false))
                        {
                            continue;
                        }
                        else
                        {
                            var displayName = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().Single().DisplayName;
                            Guid entityId;
                            // neu primary chuyen duoc sang kieu Guid
                            if (Guid.TryParse(propertyValue.ToString(), out entityId))
                            {
                                // kiem tra primary co ton tai trong he thong khong
                                var entityById = _baseRepository.GetObjById(entityId);
                                if (entityById == null)
                                {
                                    // thông báo nhân viên không tồn tại
                                    this.InvalidData(_const.NullError, serviceResult, mgsArrayError, Properties.Resources.EmployeeNotExist);
                                    isValidate = false;
                                }
                            }
                            // neu khong chuyen duoc
                            else
                            {
                                serviceResult.UserMgs = string.Format(Properties.Resources.IdInvalid, displayName);
                                serviceResult.ResultCode = _const.IdInvalid;
                                serviceResult.Data = Properties.Resources.DataInValid;
                                isValidate = false;
                                return isValidate;
                            }
                        }
                    }
                }
                //kiem tra co property required khong
                if (property.IsDefined(typeof(RequiredAttr), false) && typeof(TEntity).Name != typeof(Order).Name && typeof(TEntity).Name != typeof(User).Name)
                {
                    //check bat buoc nhap
                    if(propertyValue == null)
                    {
                        var displayName = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().Single().DisplayName;
                        this.InvalidData(_const.NullError, serviceResult, mgsArrayError, string.Format(Properties.Resources.DataNullError, displayName,displayName));
                        isValidate = false;
                    }

                }
                //kiem tra co property bi trung khong
                if(property.IsDefined(typeof(Duplicated), true) && typeof(TEntity).Name != typeof(Order).Name)
                {
                    var propDuplicated = _baseRepository.GetObjByProperty(entity, property);
                    if(propDuplicated != null)
                    {
                        var displayName = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().Single().DisplayName;
                        var propValue = displayName + " <" + propertyValue +">";
                        this.InvalidData( _const.Exist, serviceResult, mgsArrayError, string.Format(Properties.Resources.DataExist, propValue, displayName));
                        isValidate = false;
                    }
                }
                // check dinh dang ma code
                if(property.IsDefined(typeof(StartWith), true) && typeof(TEntity).Name != typeof(Order).Name && typeof(TEntity).Name != typeof(User).Name)
                {
                    var displayName = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().Single().DisplayName;
                    var val = (property.GetCustomAttributes(typeof(StartWith), true)[0] as StartWith).Value;
                    if (propertyValue.ToString().StartsWith(val))
                    {
                        continue;
                    }
                    else
                    {
                        var mgs = string.Format(Properties.Resources.CodeInvalid, displayName, val);
                        this.InvalidData(_const.MaxLength, serviceResult, mgsArrayError, mgs);
                        isValidate = false;
                    }
                }
                //check maxlength
                if(property.IsDefined(typeof(MaxLengthAttr), false))
                {
                    if(propertyValue != null)
                    {
                        // lấy display name của thuôc tính
                        var displayName = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().Single().DisplayName;
                        // lấy giá trị max length cho phép
                        var attrMaxLength = property.GetCustomAttributes(typeof(MaxLengthAttr), true)[0];
                        var length = (attrMaxLength as MaxLengthAttr).Value;
                        // nếu vượt quá đọ dài cho phép
                        if (propertyValue.ToString().Trim().Length > length)
                        {
                            var mgs = string.Format(Properties.Resources.DataMaxLengthError, displayName, length);
                            this.InvalidData(_const.MaxLength, serviceResult, mgsArrayError, mgs);
                            isValidate = false;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                // check email
                if(property.IsDefined(typeof(EmailFormat), false))
                {
                    //lấy displayName của property
                    var displayName = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().Single().DisplayName;
                    try
                    {
                        // nếu propertyValue là null hoặc rỗng hoặc có định dạng là mail
                        if((propertyValue != null) && (propertyValue.ToString() != ""))
                        {
                            MailAddress m = new MailAddress(propertyValue.ToString());
                            continue;
                        }
                        else continue;
                    }
                    // nếu không có định dạng mail
                    catch (FormatException)
                    {
                        var mgs = string.Format(Properties.Resources.EmailInvalid,displayName);
                        this.InvalidData(_const.EmailInValid, serviceResult, mgsArrayError, mgs);
                        isValidate = false;
                    }
                }
            }
            serviceResult.Data = mgsArrayError;
            if(mgsArrayError.Count >= 2)
            {
                serviceResult.ResultCode = _const.DataInvalid;
            }
            if(isValidate == true)
            {
                isValidate = ValidateEntity(entity);
            }
            return isValidate;
        }
        /// <summary>
        /// check validate của từng entity extend từ base
        /// Create:  PTDuyen(17/08/2021)
        /// mặc định kết quả trả về là true
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool ValidateEntity(TEntity entity) {
            return true;
        }

        protected void InvalidData(string code, ServiceResult serviceResult, List<string> mgsArrayError, string mgs)
        {
            serviceResult.Success = false;
            serviceResult.UserMgs = Properties.Resources.DataInValid;
            serviceResult.ResultCode = code;
            mgsArrayError.Add(mgs);
        }
        #endregion
    }
}
