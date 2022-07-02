using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using FoodManagement.Core.Entities.AdditionalElement;
using FoodManagement.Core.Entities.BussinessItem;
using FoodManagement.Core.Entities.FMOrder;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMOrder;
using FoodManagement.Repository;
using Newtonsoft.Json;

namespace OrderManagement.Repository.Repository.FMOrder
{
    public class OrderRepository : DBContext<Order>, IOrderRepository
    {
        public OrderMerge GetOrderMerge(string OrderCode)
        {
            OrderMerge OrderMerge = new OrderMerge();
            DynamicParameters parameters1 = new DynamicParameters();
            parameters1.Add($"$OrderCode", OrderCode);
            OrderMerge.Order = DBConnection.QueryFirstOrDefault<Order>($"Proc_Order_GetByCode", param: parameters1, commandType: CommandType.StoredProcedure);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"OrderId", OrderMerge.Order.OrderId);
            OrderMerge.OrderDetails = DBConnection.Query<OrderDetail>($"Proc_OrderDetail_GetByOrder", param: parameters, commandType: CommandType.StoredProcedure).ToList();
            if (OrderMerge.OrderDetails != null && OrderMerge.OrderDetails.Count > 0)
            {
                foreach (var orderDetail in OrderMerge.OrderDetails)
                {
                    //OrderDetail detail = JsonConvert.DeserializeObject<OrderDetail>(orderDetail.JsonData);
                    //var properties = orderDetail.GetType().GetProperties();
                    //foreach (var item in properties)
                    //{
                    //    var val = item.GetValue(detail, null);
                    //    item.SetValue(orderDetail, item.GetValue(detail, null));
                    //}
                    DynamicParameters parameters2 = new DynamicParameters();
                    parameters2.Add($"ListToppingId", orderDetail.ListTopping);
                    orderDetail.Toppings = DBConnection.Query<Topping>($"Proc_Topping_GetByListToppingId", param: parameters2, commandType: CommandType.StoredProcedure).ToList();
                    DynamicParameters parameters3 = new DynamicParameters();
                    parameters3.Add($"CommentStar", null);
                    parameters3.Add($"CommentStatus", null);
                    parameters3.Add($"ListUserId", null);
                    parameters3.Add($"FoodCode", orderDetail.FoodCode);
                    parameters3.Add($"OrderId", orderDetail.OrderId);
                    orderDetail.Comment = DBConnection.QueryFirstOrDefault<Comment>($"Proc_Comment_GetFilterPaging", param: parameters3, commandType: CommandType.StoredProcedure);
                }
            }
            return OrderMerge;
        }

        public string AddOrder(OrderMerge OrderMerge)
        {
            var rowEffects = 0;
            DBConnection.Open();
            using (var transaction = DBConnection.BeginTransaction())
            {
                try
                {
                    var OrderDetail = OrderMerge.OrderDetails;
                    OrderMerge.Order.OrderId = Guid.NewGuid();
                    var sqlCommand1 = $"SELECT (MAX(CAST(SUBSTR(OrderCode, 4, LENGTH(OrderCode) - 3) AS UNSIGNED) + 1)) FROM `Order`";
                    var newcode = DBConnection.QueryFirstOrDefault<String>(sqlCommand1, transaction: transaction, commandType: CommandType.Text);
                    if (newcode != null)
                    {
                        newcode = newcode.Length >= 3 ? newcode : (newcode.Length == 2 ? "0" + newcode : "00" + newcode);
                        newcode = "OC-" + newcode;
                    }
                    else
                    {
                        newcode = "OC-" + "001";
                    }
                    OrderMerge.Order.OrderCode = newcode;
                    //OrderMerge.Order.OrderCode = GetNewCode();
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@PropertyValue", OrderMerge.Order.UserName);
                    var sqlCommand = $"SELECT UserId FROM `User` WHERE UserName = @PropertyValue LIMIT 1;";
                    var userId = DBConnection.QueryFirstOrDefault<Guid>(sqlCommand, param: parameter, transaction: transaction, commandType: CommandType.Text);
                    if(userId != new Guid())
                    {
                        OrderMerge.Order.UserId = userId;
                    }
                    else
                    {
                        transaction.Commit();
                        return FoodManagement.Core.Properties.Resources.LoginLimit;
                    }
                    rowEffects = DBConnection.Execute($"Proc_Order_Post", param: OrderMerge.Order, transaction: transaction, commandType: CommandType.StoredProcedure);
                    DynamicParameters paramDetail = new DynamicParameters();
                    var listCartDetailId = new List<String>();
                    foreach (var entity in OrderMerge.OrderDetails)
                    {
                        listCartDetailId.Add(entity.CartDetailId.ToString());
                        entity.OrderDetailId = Guid.NewGuid();
                        entity.OrderId = OrderMerge.Order.OrderId;
                        entity.JsonData = JsonConvert.SerializeObject(entity);
                        var orderAdd = JsonConvert.DeserializeObject<OrderDetailNotComment>(entity.JsonData);
                        rowEffects += DBConnection.Execute($"Proc_OrderDetail_Post", param: orderAdd, transaction: transaction, commandType: CommandType.StoredProcedure);
                    }
                    var strListCartId = string.Join(",", listCartDetailId);
                    DynamicParameters parameterCart = new DynamicParameters();
                    parameterCart.Add($"ListCartDetailId", strListCartId);
                    var rowEffected = DBConnection.Execute($"Proc_CartDetail_Delete", param: parameterCart, transaction: transaction, commandType: CommandType.StoredProcedure);
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

        public string EmployeeUpdateOrder(int OrderStatus, Guid OrderId, string OrderReason)
        {
            var rowEffects = 0;
            DBConnection.Open();
            using (var transaction = DBConnection.BeginTransaction())
            {
                try
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("OrderStatus", OrderStatus);
                    parameter.Add("OrderId", OrderId);
                    parameter.Add("OrderReason", OrderReason);
                    //var sqlCommand = "Update `order` od set od.OrderStatus = @OrderStatus where od.OrderId = @OrderId;";
                    rowEffects = DBConnection.Execute("Proc_Order_UpdateStatus", param: parameter, transaction: transaction, commandType: CommandType.StoredProcedure);
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

        public string UpdateOrder(OrderMerge OrderMerge)
        {
            var rowEffects = 0;
            DBConnection.Open();
            using (var transaction = DBConnection.BeginTransaction())
            {
                try
                {
                    // tách từng phần trong Order merge
                    var listDetail = OrderMerge.OrderDetails;
                    // xóa Orderdetailt của Order này
                    DynamicParameters pid = new DynamicParameters();
                    pid.Add($"$OrderId", OrderMerge.Order.OrderId);
                    DBConnection.ExecuteScalar<int>($"Proc_OrderDetail_DeleteByOrderId", param: pid, transaction: transaction, commandType: CommandType.StoredProcedure);
                    // đặt lại tên biến để truyền dữ liệu
                    var listProp = OrderMerge.Order.GetType().GetProperties();
                    DynamicParameters proUpdate = new DynamicParameters();
                    foreach (var item in listProp)
                    {
                        var name = "$" + item.Name;
                        proUpdate.Add(name, item.GetValue(OrderMerge.Order));
                    }
                    // cập nhật Order
                    rowEffects = DBConnection.Execute($"Proc_Order_Update", param: proUpdate, transaction: transaction, commandType: CommandType.StoredProcedure);

                    // thêm lại OrderDetail
                    DynamicParameters paramDetail = new DynamicParameters();
                    foreach (var entity in listDetail)
                    {
                        entity.OrderDetailId = Guid.NewGuid();
                        entity.OrderId = Guid.Parse(OrderMerge.Order.OrderId.ToString());
                        rowEffects += DBConnection.Execute($"Proc_OrderDetail_Post", param: entity, transaction: transaction, commandType: CommandType.StoredProcedure);
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
