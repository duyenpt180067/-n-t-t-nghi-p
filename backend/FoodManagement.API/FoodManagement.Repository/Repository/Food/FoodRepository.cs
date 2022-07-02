using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FoodManagement.Core.Entities.AdditionalElement;
using FoodManagement.Core.Entities.BussinessItem;
using FoodManagement.Core.Entities.FMFood;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMFood;
using Newtonsoft.Json;

namespace FoodManagement.Repository.Repository.FMFood
{
    public class FoodRepository : DBContext<Food>, IFoodRepository
    {
        public List<Food> GetPopularCategories()
        {
            var entities = DBConnection.Query<Food>($"Proc_Food_PopularDish", commandType: CommandType.StoredProcedure).ToList();
            return entities;
        }

        public FoodMerge GetFoodMerge(string FoodCode)
        {
            FoodMerge FoodMerge = new FoodMerge();
            DynamicParameters parameters1 = new DynamicParameters();
            parameters1.Add($"$FoodCode", FoodCode);
            FoodMerge.Food = DBConnection.QueryFirstOrDefault<Food>($"Proc_Food_GetByCode", param: parameters1, commandType: CommandType.StoredProcedure);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"$FoodId", FoodMerge.Food.FoodId);
            FoodMerge.FoodDetails = DBConnection.Query<FoodDetail>($"Proc_FoodDetail_GetByFoodId", param: parameters, commandType: CommandType.StoredProcedure).ToList();
            DynamicParameters parameters3 = new DynamicParameters();
            parameters3.Add($"FoodCode", FoodMerge.Food.FoodCode);
            parameters3.Add($"ListUserId", null);
            parameters3.Add($"OrderId", null);
            parameters3.Add($"CommentStatus", null);
            parameters3.Add($"CommentStar", null);
            FoodMerge.Comments = DBConnection.Query<Comment>($"Proc_Comment_GetFilterPaging", param: parameters3, commandType: CommandType.StoredProcedure).ToList();
            DynamicParameters parameters2 = new DynamicParameters();
            if(FoodMerge.Food != null && FoodMerge.Food.ListTopping != null && FoodMerge.Food.ListTopping != "")
            {
                parameters2.Add($"ListToppingId", FoodMerge.Food.ListTopping);
                FoodMerge.Toppings = DBConnection.Query<Topping>($"Proc_Topping_GetByListToppingId", param: parameters2, commandType: CommandType.StoredProcedure).ToList();
            }
            return FoodMerge;
        }

        public List<Food> GetPopularFood(Guid CategoryId)
        {
            DynamicParameters parameters1 = new DynamicParameters();
            parameters1.Add($"CategoryId", CategoryId);
            var entities = DBConnection.Query<Food>($"Proc_Food_PopularDish", param: parameters1, commandType: CommandType.StoredProcedure).ToList();
            return entities;
        }

        public string AddFood(FoodMerge FoodMerge)
        {
            var rowEffects = 0;
            DBConnection.Open();
            using (var transaction = DBConnection.BeginTransaction())
            {
                try
                {
                    var FoodDetail = FoodMerge.FoodDetails;
                    FoodMerge.Food.FoodId = Guid.NewGuid();
                    rowEffects = DBConnection.Execute($"Proc_Food_Post", param: FoodMerge.Food, transaction: transaction, commandType: CommandType.StoredProcedure);
                    DynamicParameters paramDetail = new DynamicParameters();
                    foreach (var entity in FoodMerge.FoodDetails)
                    {
                        entity.FoodDetailId = Guid.NewGuid();
                        entity.FoodId = FoodMerge.Food.FoodId;
                        rowEffects += DBConnection.Execute($"Proc_FoodDetail_Post", param: entity, transaction: transaction, commandType: CommandType.StoredProcedure);
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

            };
        }

        public string UpdateFood(FoodMerge FoodMerge)
        {
            var rowEffects = 0;
            DBConnection.Open();
            using (var transaction = DBConnection.BeginTransaction())
            {
                try
                {
                    // tách từng phần trong Food merge
                    var listDetail = FoodMerge.FoodDetails;
                    // xóa Fooddetailt của Food này
                    //DynamicParameters pid = new DynamicParameters();
                    //pid.Add($"$FoodId", FoodMerge.Food.FoodId);
                    //DBConnection.ExecuteScalar<int>($"Proc_FoodDetail_Delete", param: pid, transaction: transaction, commandType: CommandType.StoredProcedure);
                    // đặt lại tên biến để truyền dữ liệu
                    var listProp = FoodMerge.Food.GetType().GetProperties();
                    DynamicParameters proUpdate = new DynamicParameters();
                    foreach (var item in listProp)
                    {
                        var name = "$" + item.Name;
                        proUpdate.Add(name, item.GetValue(FoodMerge.Food));
                    }
                    // cập nhật Food
                    rowEffects = DBConnection.Execute($"Proc_Food_Update", param: proUpdate, transaction: transaction, commandType: CommandType.StoredProcedure);
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add($"$FoodId", FoodMerge.Food.FoodId);
                    var foodDetails = DBConnection.Query<FoodDetail>($"Proc_FoodDetail_GetByFoodId", param: parameters, transaction: transaction, commandType: CommandType.StoredProcedure).ToList();
                    if(foodDetails == null)
                    {
                        foreach (var entity in listDetail)
                        {
                            entity.FoodDetailId = Guid.NewGuid();
                            entity.FoodId = Guid.Parse(FoodMerge.Food.FoodId.ToString());
                            rowEffects += DBConnection.Execute($"Proc_FoodDetail_Post", param: entity, transaction: transaction, commandType: CommandType.StoredProcedure);
                        }
                    }
                    else
                    {
                        // thêm lại FoodDetail
                        DynamicParameters paramDetail = new DynamicParameters();
                        foreach (var entity in listDetail)
                        {
                            var cnt = 0;
                            for (int i = 0; i < foodDetails.Count; i++)
                            {
                                if (entity.SizeCode == foodDetails[i].SizeCode)
                                {
                                    foodDetails[i].Amount = entity.Amount;
                                    rowEffects += DBConnection.Execute($"Proc_FoodDetail_Update", param: foodDetails[i], transaction: transaction, commandType: CommandType.StoredProcedure);
                                    break;
                                }
                                else
                                {
                                    cnt += 1;
                                }
                            }
                            if (cnt == foodDetails.Count)
                            {
                                entity.FoodDetailId = Guid.NewGuid();
                                entity.FoodId = Guid.Parse(FoodMerge.Food.FoodId.ToString());
                                rowEffects += DBConnection.Execute($"Proc_FoodDetail_Post", param: entity, transaction: transaction, commandType: CommandType.StoredProcedure);
                            }
                        }
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

            };
        }
    }
}
