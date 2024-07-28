using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mrp2.Models
{
    public class RecipeViewModel
    {
        public int Id { get; set; }
        public string RecipeName { get; set; }
        public decimal TotalCost { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Quantity { get; set; }
        public List<RecipeRawMaterialViewModel> RawMaterials { get; set; } = new List<RecipeRawMaterialViewModel>();
       

        public IEnumerable<SelectListItem> RawMaterialOptions { get; set; }


    }
}
