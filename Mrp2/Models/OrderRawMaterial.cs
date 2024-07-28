namespace Mrp2.Models
{
    public class OrderRawMaterial
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int RawMaterialId { get; set; }
        public RawMaterial RawMaterial { get; set; }

 

        public decimal Quantity { get; set; } = 0;
        public decimal UnitCost { get; set; } = 0;


    }
}
