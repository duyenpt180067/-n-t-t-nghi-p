using Dapper;
using FoodManagement.Core.Consts;
using FoodManagement.Core.Entities.AdditionalElement;
using FoodManagement.Core.Entities.FMUser;
using FoodManagement.Core.General.Entities;
//using MySqlConnector;
using FoodManagement.Core.Interfaces.Repository;
using MySqlConnector;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static FoodManagement.Core.General.Entities.BaseEntity;

namespace FoodManagement.Repository
{
    /// <summary>
    /// tuong tac voi databse
    /// Create: PTDuyen(28/07/2021)
    /// </summary>
    public class DBContext<TEntity>:IBaseRepository<TEntity>, IDisposable where TEntity : BaseEntity
    {
        #region Fields
        protected IDbConnection DBConnection;
        protected string _connectionString;
        #endregion
        public DBContext()
        {
            //thông tin ket noi Db
            _connectionString = ""
                                + "Host = localhost;"
                                + "Port = 3306;"
                                + "Database = fast_food_management;"
                                + "User Id = root;" + "Password = khsKTX52";
            //Khoi tao ket noi
            DBConnection = new MySqlConnection(_connectionString);
        }

        #region Methods
        /// <summary>
        /// lay tat ca cac object
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <returns>Danh sach object</returns>
        public List<TEntity> GetAllObj()
        {
            //lay ten class entity
            var className = typeof(TEntity).Name;
            var sqlCommand = $"SELECT * FROM {className}";
            var entities = DBConnection.Query<TEntity>(sqlCommand, commandType: CommandType.Text).ToList();
            return entities;
        }


        /// <summary>
        /// lay du lieu object theo id
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="entityId">Guid</param>
        /// <returns>object</returns>
        public TEntity GetObjById(Guid entityId)
        {
            DynamicParameters parameter = new DynamicParameters();
            //lay ten class entity
            var className = typeof(TEntity).Name;
            parameter.Add("@PropertyValue", entityId);
            var sqlCommand = $"SELECT * FROM {className} WHERE {className}Id = @PropertyValue LIMIT 1;";
            var entity = DBConnection.QueryFirstOrDefault<TEntity>(sqlCommand, param: parameter, commandType: CommandType.Text);
            return entity;
        }

        /// <summary>
        /// them moi duw lieu cua object
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="entity">Object</param>
        /// <returns>so luong hang bi sua doi</returns>
        public virtual string AddObj(TEntity entity, User user)
        {
            DBConnection.Open();
            using (var transaction = DBConnection.BeginTransaction())
            {
                try
                {
                    //lay ten class entity
                    var className = typeof(TEntity).Name;
                    var displayName = typeof(TEntity)
                    .GetCustomAttributes(typeof(DisplayNameAttribute), true)
                    .FirstOrDefault() as DisplayNameAttribute;
                    var rowEffects = DBConnection.Execute($"Proc_{className}_Post", param: entity, transaction: transaction, commandType: CommandType.StoredProcedure);
                    AccessLog accessLog = new AccessLog
                    {
                        UserActionId = user.UserId,
                        UserAction = user.UserName,
                        Description = GenDescriptionAccessLog(entity, null, "Thêm mới"),
                        Subject = displayName.DisplayName.ToString(),
                        Action = "Thêm mới"
                    };
                    rowEffects += DBConnection.Execute($"Proc_AccessLog_Post", param: accessLog, transaction: transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                    return rowEffects.ToString();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return e.Message;
                }

            }; 
        }

        /// <summary>
        /// sua du lieu cua object
        /// create:PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="entity">object</param>
        /// <returns>so luong ban ghi bi sua</returns>
        public string UpdateObj(TEntity entity, User user, TEntity oldObj)
        {
            DBConnection.Open();
            using(var transaction = DBConnection.BeginTransaction())
            {
                try
                {
                    var displayName = typeof(TEntity)
                       .GetCustomAttributes(typeof(DisplayNameAttribute), true)
                       .FirstOrDefault() as DisplayNameAttribute;
                    DynamicParameters parameter = new DynamicParameters();
                    var listProp = entity.GetType().GetProperties();
                    //dynamic objectPut = new ExpandoObject();
                    foreach (var item in listProp)
                    {
                        if(item.Name != "Toppings" && item.Name != "ListFoodDetailSame" && item.Name != "ListOrgTopping")
                        {
                            var name = "$" + item.Name;
                            parameter.Add(name, item.GetValue(entity));
                        }
                    }
                    var className = typeof(TEntity).Name;
                    var rowEffects = DBConnection.Execute($"Proc_{className}_Update", param: parameter, transaction: transaction, commandType: CommandType.StoredProcedure);
                    AccessLog accessLog = new AccessLog
                    {
                        UserActionId = user.UserId,
                        UserAction = user.UserName,
                        Description = GenDescriptionAccessLog(entity, oldObj, "Cập nhật"),
                        Subject = displayName.DisplayName.ToString(),
                        Action = "Thêm mới"
                    };
                    rowEffects += DBConnection.Execute($"Proc_AccessLog_Post", param: accessLog, transaction: transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                    return rowEffects.ToString();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return e.Message;
                }
            }
        }
        /// <summary>
        /// xoa du lieu theo id
        /// </summary>
        /// <param name="entityId">Guid</param>
        /// Create: PTDuyen(18/08/2021)
        /// <returns>so ban ghi bi xoa</returns>
        public virtual string DeleteObj(Guid entityId, User user, TEntity entity)
        {
            var rowEffects = 0;
            DBConnection.Open();
            using(var transaction = DBConnection.BeginTransaction())
            {
                try
                {
                    var displayName = typeof(TEntity)
                    .GetCustomAttributes(typeof(DisplayNameAttribute), true)
                    .FirstOrDefault() as DisplayNameAttribute;
                    DynamicParameters parameter = new DynamicParameters();
                    //lay ten class entity
                    var className = typeof(TEntity).Name.ToLower();
                    parameter.Add($"p_id", entityId);
                    DBConnection.Execute($"Proc_{className}_DeleteById", param: parameter, transaction: transaction, commandType: CommandType.StoredProcedure);
                    rowEffects += 1;
                    AccessLog accessLog = new AccessLog
                    {
                        UserActionId = user.UserId,
                        UserAction = user.UserName,
                        Description = GenDescriptionAccessLog(entity, null, "Xóa"),
                        Subject = displayName.DisplayName.ToString(),
                        Action = "Xóa"
                    };
                    rowEffects += DBConnection.Execute($"Proc_AccessLog_Post", param: accessLog, transaction: transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                    return rowEffects.ToString();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    rowEffects = 0;
                    return e.Message;
                }
            }
        }

        /// <summary>
        /// xoa nhieu ban ghi theo list id truyen vao
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="listEntityId"></param>
        /// <returns>so ban ghi bi xoa</returns>
        public string DeleteMultiObj(List<Guid> listEntityId, User user)
        {
            DBConnection.Open();
            using(var transaction = DBConnection.BeginTransaction())
            {
                try
                {
                    var displayName = typeof(TEntity)
                      .GetCustomAttributes(typeof(DisplayNameAttribute), true)
                      .FirstOrDefault() as DisplayNameAttribute;
                    var rowEffects = 0;
                    DynamicParameters parameter = new DynamicParameters();
                    //lay ten class entity
                    var className = typeof(TEntity).Name;
                    string listEntityIdString = string.Join(",", listEntityId);
                    parameter.Add($"List{className}Id", listEntityIdString);
                    for (int i = 0; i < listEntityId.Count; i++)
                    {
                        var entity = GetObjById(listEntityId[i]);
                        AccessLog accessLog = new AccessLog
                        {
                            UserActionId = user.UserId,
                            UserAction = user.UserName,
                            Description = GenDescriptionAccessLog(entity, null, "Xóa"),
                            Subject = displayName.DisplayName.ToString(),
                            Action = "Xóa"
                        };
                        rowEffects += DBConnection.Execute($"Proc_AccessLog_Post", param: accessLog, transaction: transaction, commandType: CommandType.StoredProcedure);
                    }
                    rowEffects = DBConnection.Execute($"Proc_{className}_Delete", param: parameter, transaction: transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                    return rowEffects.ToString();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return e.Message;
                }
            }
        }

        /// <summary>
        /// them moi duw lieu cua object
        /// Create: PTDuyen(04/10/2021)
        /// </summary>
        /// <param name="entity">Object</param>
        /// <returns>so luong hang bi sua doi</returns>
        public string AddMultiObj(List<TEntity> listEntity)
        {
            var rowEffects = 0;
            DBConnection.Open();
            using (var transaction = DBConnection.BeginTransaction())
            {
                try
                {
                    //lay ten class entity
                    var className = typeof(TEntity).Name;
                    foreach (var entity in listEntity)
                    {
                        //thực hiện theem
                        DBConnection.Execute($"Proc_{className}_Post", param: entity, transaction: transaction, commandType: CommandType.StoredProcedure);
                        rowEffects += 1;
                    }
                    transaction.Commit();
                    return rowEffects.ToString();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    rowEffects = 0;
                    return e.Message;
                }
            }
        }

        /// <summary>
        /// xoa nhieu ban gi theo mang id
        /// Create: PTDuyen(03/08/2021)
        /// </summary>
        /// <param name="listEntityId">mang cac id truyen vao</param>
        /// <returns>so ban ghi bi xoa</returns>
        public TEntity GetObjByCode(string entityCode)
        {
            //lay ten class entity
            var className = typeof(TEntity).Name.ToLower();
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add($"{className}_code", entityCode);
            var entity = DBConnection.QueryFirstOrDefault<TEntity>($"Proc_Get_{className}_By_Code", param: parameter, commandType: CommandType.StoredProcedure);
            return entity;
        }

        public User GetUserByUserName(string userName)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"$UserName", userName);
            User user = DBConnection.QueryFirstOrDefault<User>($"Proc_User_By_UserName", param: parameters, commandType: CommandType.StoredProcedure);
            return user;
        }

        /// <summary>
        /// lay du lieu obj theo property nao do tru chinh entity truyen vao
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="entity">entity truyen vao</param>
        /// <param name="property">property truyen vao</param>
        /// <returns>entity</returns>
        public object GetObjByProperty(TEntity entity, PropertyInfo property)
        {
            var sqlCommand = string.Empty;
            var className = typeof(TEntity).Name;
            var entityId = entity.GetType().GetProperty($"{className}Id").GetValue(entity);
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@PropertyValue", property.GetValue(entity));
            if (entity.EntityState == EntityState.Add)
            {
                sqlCommand = $"SELECT * FROM {className} WHERE {property.Name} = @PropertyValue LIMIT 1;";
            }
            else if (entity.EntityState == EntityState.Update)
            {
                parameter.Add("@EntityId", entityId);
                sqlCommand = $"SELECT * FROM {className} WHERE {property.Name} = @PropertyValue AND {className}Id <> @EntityId LIMIT 1;";
            }
            else return null;
            var entityGet = DBConnection.QueryFirstOrDefault<TEntity>(sqlCommand, param: parameter, commandType: CommandType.Text);
            return entityGet;
        }

        /// <summary>
        /// lay du lieu obj theo property nao do tru chinh entity truyen vao
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="entity">entity truyen vao</param>
        /// <param name="property">property truyen vao</param>
        /// <returns>entity</returns>
        public object GetByCode(string code)
        {
            var sqlCommand = string.Empty;
            var className = typeof(TEntity).Name;
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@PropertyValue", code);
            sqlCommand = $"SELECT * FROM {className} WHERE {className}Code = @PropertyValue LIMIT 1;";
            var entityGet = DBConnection.QueryFirstOrDefault<TEntity>(sqlCommand, param: parameter, commandType: CommandType.Text);
            return entityGet;
        }
        /// <summary>
        /// Phan trang
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual Object GetFilterPaging(JObject param)
        {
            var TotalRecord = 0;
            var TotalPage = 0;
            var className = typeof(TEntity).Name;
            DynamicParameters parameter = new DynamicParameters();
            //var listProp = param.GetType().GetProperties();
            foreach (JProperty item in param.Properties())
            {
                Object value = item.Value.ToObject<Object>();
                parameter.Add(item.Name, value==null?null:value);
            }
            parameter.Add("TotalRecord", dbType: System.Data.DbType.Int32, direction: ParameterDirection.Output);
            parameter.Add("TotalPage", dbType: System.Data.DbType.Int32, direction: ParameterDirection.Output);
            var entities = DBConnection.Query<Object>($"Proc_{className}_GetFilterPaging", param: parameter, commandType: CommandType.StoredProcedure).ToList();
            if (parameter.Get<Object>("TotalRecord") != null)
            {
                TotalRecord = parameter.Get<int>("TotalRecord");
                TotalPage = parameter.Get<int>("TotalPage");
            }
            else
            {
                TotalRecord = entities.Count;
                TotalPage = 1;
            }
            var obj = new
            {
                TotalPage = TotalPage,
                TotalRecord = TotalRecord,
                Data = entities
            };
            return obj;
        }

        public virtual string GenDescriptionAccessLog(TEntity newObj, TEntity oldObj, string action) {
            var des = string.Empty;
            if (action == "Thêm mới")
            {
                var properties = newObj.GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (property.IsDefined(typeof(LogAudit), false))
                    {
                        var displayName = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().Single().DisplayName;
                        des += "\n+ " + displayName + ": " + property.GetValue(newObj);
                    }
                }
                des = des.Substring(1);
            }
            else if(action == "Cập nhật")
            {
                var properties = newObj.GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (property.IsDefined(typeof(LogAudit), false))
                    {
                        var oldVal = property.GetValue(oldObj).ToString();
                        var newVal = property.GetValue(newObj).ToString();
                        if (oldVal != newVal)
                        {
                            var displayName = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().Single().DisplayName;
                            des += "\n+ " + displayName + ": Từ <" + oldVal + "> thành <" + newVal + ">";
                        }
                    }
                }
                des = des.Substring(1);
            }
            else if (action == "Xóa")
            {
                var displayObjName = typeof(TEntity)
                    .GetCustomAttributes(typeof(DisplayNameAttribute), true)
                    .FirstOrDefault() as DisplayNameAttribute;
                DynamicParameters parameter = new DynamicParameters();
                var properties = newObj.GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (property.IsDefined(typeof(IsCode), false))
                    {
                        des += "Xóa " + displayObjName.DisplayName + "<" + property.GetValue(newObj) + ">";
                    }
                }
            }
            return des;
        }

        public object GetLayoutConfig()
        {
            var className = typeof(TEntity).Name;
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("$LayoutName", className+"List");
            var entities = DBConnection.QueryFirstOrDefault<Object>($"Proc_LayoutConfig_GetLayout", param: parameter, commandType: CommandType.StoredProcedure);
            return entities;
        }

        /// <summary>
        /// dong ket noi khi ket noi dang mo ma khong duoc su dung
        /// create: PTDuyen(18/08/2021)
        /// </summary>
        public void Dispose()
        {
            if (DBConnection.State == ConnectionState.Open)
            {
                DBConnection.Close();
            }
        }

        public string GetNewCode()
        {
            var newcode = string.Empty;
            var className = typeof(TEntity).Name;
            var properties = typeof(TEntity).GetProperties();
            foreach (var property in properties)
            {
                if (property.IsDefined(typeof(IsCode), false))
                {
                    var attrStartWith = property.GetCustomAttributes(typeof(StartWith), true)[0];
                    var valStartWith = (attrStartWith as StartWith).Value;
                    var sqlCommand = $"SELECT (MAX(CAST(SUBSTR({property.Name}, {valStartWith.Length+1}, LENGTH({property.Name}) - {valStartWith.Length}) AS UNSIGNED) + 1)) FROM {className}";
                    newcode = DBConnection.QueryFirstOrDefault<string>(sqlCommand, commandType: CommandType.Text);
                    if(newcode != null)
                    {
                        newcode = newcode.Length >= 3 ? newcode : (newcode.Length == 2 ? "0" + newcode : "00" + newcode);
                        newcode = (attrStartWith as StartWith).Value + newcode;
                    }
                    else
                    {
                        newcode = valStartWith + "001";
                    }
                    return newcode;
                }
                else
                {
                    continue;
                }
            }
            return newcode;
        }
        #endregion
    }
}