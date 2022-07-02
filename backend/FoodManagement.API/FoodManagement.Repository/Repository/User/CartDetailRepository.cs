using Dapper;
using FoodManagement.Core.Entities.AdditionalElement;
using FoodManagement.Core.Entities.FMFood;
using FoodManagement.Core.Entities.FMUser;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMFood;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMUser;
using FoodManagement.Repository.Repository.FMFood;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FoodManagement.Repository.Repository.FMUser
{
    public class CartDetailRepository:DBContext<CartDetail>, ICartDetailRepository
    {
        public CartDetail GetObjByFoodDetailId(Guid entityId)
        {
            DynamicParameters parameter = new DynamicParameters();
            //lay ten class entity
            var className = typeof(CartDetail).Name;
            parameter.Add("@PropertyValue", entityId);
            var sqlCommand = $"SELECT * FROM {className} WHERE FoodDetailId = @PropertyValue LIMIT 1;";
            var entity = DBConnection.QueryFirstOrDefault<CartDetail>(sqlCommand, param: parameter, commandType: CommandType.Text);
            return entity;
        }

        /*public override string AddObj(CartDetail entity)
        {
            CartDetail cartDetail = GetObjByFoodDetailId(Guid.Parse(entity.FoodDetailId.ToString()));
            if(cartDetail == null)
            {
                DBConnection.Open();
                using (var transaction = DBConnection.BeginTransaction())
                {
                    try
                    {
                        //lay ten class entity
                        var className = typeof(CartDetail).Name;
                        var rowEffects = DBConnection.Execute($"Proc_{className}_Post", param: entity, transaction: transaction, commandType: CommandType.StoredProcedure);
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
            else
            {
                var toppings = cartDetail.ListTopping.Split(',').Select(s => s.Trim()).ToList();
                var lstTopping = entity.ListTopping.Split(',').Select(s => s.Trim()).ToList();
                var exceptToppingInLstTopping = toppings.Except(lstTopping).ToList();
                List<dynamic> entityTopping = new List<dynamic>();
                if(toppings.Count > 0 && lstTopping.Count > 0)
                {
                    for (int i = 0; i <= toppings.Count-1; i++)
                    {
                        for (int j = 0; j <= lstTopping.Count - 1; j++)
                        {
                            //if(toppings[0].Quantity.ToObject<Int32>())
                            //if (toppings[i].ToppingId.ToObject<String>() == lstTopping[j].ToppingId.ToObject<String>())
                            //{
                            //    var et = new
                            //    {
                            //        ToppingId = toppings[i].ToppingId.ToObject<Guid>(),
                            //        Quantity = toppings[i].Quantity.ToObject<Int32>() + lstTopping[j].Quantity.ToObject<Int32>()
                            //    };
                            //    entityTopping.Add(JsonConvert.SerializeObject(et));
                            //    toppings.Remove(toppings[i]);
                            //    lstTopping.Remove(lstTopping[j]);
                            //    i = i - 1 >= 0 ? i-1 : 0;
                            //    j = j - 1;
                            //}
                            //else
                            //{
                            //    continue;
                            //}
                        }
                    }
                }
                var ListTopping = lstTopping.Concat(toppings).Concat(entityTopping).ToList();
                entity.ListTopping = string.Join(", ", ListTopping).Replace("\r\n", "");
                entity.CartDetailId = cartDetail.CartDetailId;
                entity.Quantity = entity.Quantity + cartDetail.Quantity;
                DBConnection.Open();
                using (var transaction = DBConnection.BeginTransaction())
                {
                    try
                    {
                        DynamicParameters parameter = new DynamicParameters();
                        var listProp = entity.GetType().GetProperties();
                        //dynamic objectPut = new ExpandoObject();
                        foreach (var item in listProp)
                        {
                            var name = "$" + item.Name;
                            parameter.Add(name, item.GetValue(entity));
                        }
                        var className = typeof(CartDetail).Name;
                        var rowEffects = DBConnection.Execute($"Proc_{className}_Update", param: parameter, transaction: transaction, commandType: CommandType.StoredProcedure);
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
        }*/

        public override object GetFilterPaging(JObject param)
        {
            var TotalRecord = 0;
            var TotalPage = 0;
            var className = typeof(CartDetail).Name;
            DynamicParameters parameter = new DynamicParameters();
            //var listProp = param.GetType().GetProperties();
            foreach (JProperty item in param.Properties())
            {
                Object value = item.Value.ToObject<Object>();
                parameter.Add(item.Name, value == null ? null : value);
            }
            parameter.Add("TotalRecord", dbType: System.Data.DbType.Int32, direction: ParameterDirection.Output);
            parameter.Add("TotalPage", dbType: System.Data.DbType.Int32, direction: ParameterDirection.Output);
            var entities = DBConnection.Query<CartDetail>($"Proc_{className}_GetFilterPaging", param: parameter, commandType: CommandType.StoredProcedure).ToList();
            if (className == typeof(CartDetail).Name && entities != null && entities.Count > 0)
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add($"$FoodId", entities[i].FoodId);
                    entities[i].ListFoodDetailSame = DBConnection.Query<FoodDetail>($"Proc_FoodDetail_GetByFoodId", param: parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            if (entities != null && entities.Count > 0)
            {
                foreach (var item in entities)
                {
                    var par = new DynamicParameters();
                    par.Add("ListToppingId", item.ListTopping);
                    item.Toppings = DBConnection.Query<Topping>($"Proc_Topping_GetByListToppingId", param: par, commandType: CommandType.StoredProcedure).ToList();
                    var par2 = new DynamicParameters();
                    par2.Add("$FoodId", item.FoodId);
                    item.ListOrgTopping = DBConnection.Query<Topping>($"Proc_Topping_GetByFood", param: par2, commandType: CommandType.StoredProcedure).ToList();
                }
            }
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
    }
}
