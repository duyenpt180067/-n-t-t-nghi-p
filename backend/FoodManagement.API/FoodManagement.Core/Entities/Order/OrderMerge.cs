using FoodManagement.Core.Entities.AdditionalElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagement.Core.Entities.FMOrder
{
    public class OrderMerge
    {
        public Order Order { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        //public List<Topping> Toppings { get; set; }
    }
}
