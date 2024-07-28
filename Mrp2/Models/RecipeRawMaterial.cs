namespace Mrp2.Models
{
    public class RecipeRawMaterial
    {

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public int RawMaterialId { get; set; }
        public RawMaterial RawMaterial { get; set; }

        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }



    }
}
