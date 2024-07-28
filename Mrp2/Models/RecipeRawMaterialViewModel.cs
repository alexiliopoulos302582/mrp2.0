namespace Mrp2.Models
{
    public class RecipeRawMaterialViewModel
    {
        public int RawMaterialId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public string RawMaterialName { get; set; } = "";
    }
}
