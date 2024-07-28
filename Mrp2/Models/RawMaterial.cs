namespace Mrp2.Models
{
    public class RawMaterial
    {



        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";

        public string Notes { get; set; } = "";
        public decimal Quantity { get; set; } = 0;
        public decimal BoundQTY { get; set; } = 0;
        public string Category { get; set; } = "";
        public DateTime? Expiration { get; set; }
        public int LeadTime { get; set; } = 0;
        public string Location { get; set; } = "";
        public decimal ReorderPoint { get; set; } = 0;
        public decimal SafetyStock { get; set; } = 0;
        public string Status { get; set; } = "";
        public string Supplier { get; set; } = "";
        public decimal UnitCost { get; set; } = 0;
        public decimal Eoq { get; set; } = 0;
        public string Unit { get; set; } = "";

        public ICollection<RecipeRawMaterial> RecipeRawMaterials { get; set; }
        public ICollection<OrderRawMaterial> OrderRawMaterials { get; set; }

        public RawMaterial()
        {
            
        }
    }
}
