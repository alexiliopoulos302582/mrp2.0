namespace Mrp2.Models
{
    public class Order
    {

        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Po { get; set; } = "";
        public string Notes { get; set; } = "";

        public decimal RawMaterialCost { get; set; } = 0;
        public decimal RecipeCost { get; set; } = 0;
        public decimal Quantity { get; set; } = 0;
        public decimal OrderTotalCost { get; set; } = 0;

        public DateTime? Created { get; set; }
        public bool MaterialsReserved { get; set; } = false;
        public bool OrderCompleted { get; set; } = false;

        public int? RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public ICollection<OrderRawMaterial> OrderRawMaterials { get; set; }
        public List<int> RawMaterialIds { get; set; } = new List<int>();





    }






}
