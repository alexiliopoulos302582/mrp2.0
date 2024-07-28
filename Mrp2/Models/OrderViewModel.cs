using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mrp2.Models
{
    public class OrderViewModel
    {


        public int Id { get; set; }

        public string Name { get; set; }
        public string Po { get; set; }
        public string Notes { get; set; }
        public decimal RawMaterialCost { get; set; }
        public decimal RecipeCost { get; set; }
        public decimal Quantity { get; set; }
        public decimal OrderTotalCost { get; set; }
        public int? RecipeId { get; set; }
     
        public List<int> RawMaterialsIds { get; set; }=new List<int>();
        public List<decimal> Quantities { get; set; }=new List<decimal>();
        public List<decimal> UnitCosts { get; set; }=new List<decimal>();

 

        public IEnumerable<SelectListItem>? Recipes { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem>? RawMaterials { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> RecipeRawMaterials { get; set; }



    }
}
