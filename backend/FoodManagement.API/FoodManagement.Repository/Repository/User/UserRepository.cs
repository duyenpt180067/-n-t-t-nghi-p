using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text.Json;
using Dapper;
using FoodManagement.Core.Entities.FMUser;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMUser;

namespace FoodManagement.Repository.Repository.FMUser
{
    public class UserRepository:DBContext<User>,IUserRepository
    {
        /// <summary>
        /// Mã hóa mật khẩu
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public string GetMD5(string pass)
        {
            string str_md5 = "";
            byte[] arrPass = System.Text.Encoding.UTF8.GetBytes(pass);

            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            arrPass = my_md5.ComputeHash(arrPass);

            foreach (byte bt in arrPass)
            {
                str_md5 += bt.ToString("X2");
            }

            return str_md5;
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="useName"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public User Login(string useName, string pass)
        {
            DynamicParameters parameters = new DynamicParameters();
            string ecPass = GetMD5(pass);
            parameters.Add($"$UserName", useName);
            parameters.Add($"$Pass", ecPass);
            var result = DBConnection.QueryFirstOrDefault<User>($"Proc_User_Login", param: parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        /// <summary>
        /// them moi duw lieu cua object
        /// Create: PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="user">Object</param>
        /// <returns>so luong hang bi sua doi</returns>
        public override string AddObj(User user, User UserCreate)
        {
            var rowEffects = 0;
            DBConnection.Open();
            using (var transaction = DBConnection.BeginTransaction())
            {
                try
                {
                    user.Pass = GetMD5(user.Pass);
                    DBConnection.Execute($"Proc_User_Post", param: user, transaction: transaction, commandType: CommandType.StoredProcedure);
                    rowEffects += 1;
                    transaction.Commit();
                    if (rowEffects > 0)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add($"$UserName", user.UserName);
                        parameters.Add($"$Pass", user.Pass);
                        var result = DBConnection.QueryFirstOrDefault<User>($"Proc_User_Login", param: parameters, commandType: CommandType.StoredProcedure);
                        return JsonSerializer.Serialize<User>(result); ;
                    }
                    else return rowEffects.ToString();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    rowEffects = 0;
                    return e.Message;
                }

            };
        }

        /// <summary>
        /// sua du lieu cua object
        /// create:PTDuyen(18/08/2021)
        /// </summary>
        /// <param name="user">object</param>
        /// <returns>so luong ban ghi bi sua</returns>
        public string UpdateUser(User user)
        {
            var rowEffects = 0;
            DBConnection.Open();
            using (var transaction = DBConnection.BeginTransaction())
            {
                try
                {
                    user.Pass = user.Pass;
                    DynamicParameters parameter = new DynamicParameters();
                    var listProp = user.GetType().GetProperties();
                    //dynamic objectPut = new ExpandoObject();
                    foreach (var item in listProp)
                    {
                        var name = "$" + item.Name;
                        parameter.Add(name, item.GetValue(user));
                    }
                    DBConnection.Execute($"Proc_Update_User", param: parameter, transaction: transaction, commandType: CommandType.StoredProcedure);
                    rowEffects += 1;
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
        /// <param name="listUserId"></param>
        /// <returns>so ban ghi bi xoa</returns>
        public string DeleteMultiUser(List<Guid> listUserId)
        {
            var rowEffects = 0;
            DBConnection.Open();
            using (var transaction = DBConnection.BeginTransaction())
            {
                try
                {
                    foreach (var entityId in listUserId)
                    {
                        //thêm tham số 
                        DynamicParameters parameter = new DynamicParameters();
                        parameter.Add($"$Id", entityId);
                        //thực hiện xóa
                        DBConnection.Execute($"Proc_Delete_User_By_Id", param: parameter, transaction: transaction, commandType: CommandType.StoredProcedure);
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
    }
}
