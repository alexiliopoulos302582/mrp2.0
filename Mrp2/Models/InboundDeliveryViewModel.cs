namespace Mrp2.Models
{
    public class InboundDeliveryViewModel
    {


        public int SupplierId { get; set; }
        public List<InboundRawMaterialViewModel> RawMaterials { get; set; } = new List<InboundRawMaterialViewModel>();






    }
}
