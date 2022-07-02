using FoodManagement.Core.Entities.AdditionalElement;
using FoodManagement.Core.Entities.BussinessItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagement.Core.Entities.FMFood
{
    public class FoodMerge
    {
        public Food Food { get; set; }
        public List<Comment> Comments { get; set; }
        public List<FoodDetail> FoodDetails { get; set; }
        public List<Topping> Toppings { get; set; }
    }
}
