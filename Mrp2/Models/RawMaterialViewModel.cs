using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mrp2.Models
{
    public class RawMaterialViewModel
    {

        public RawMaterial RawMaterial { get; set; }

        public IEnumerable<SelectListItem> Units { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Suppliers{ get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }

        public int SelectedUnitId {  get; set; }
        public int SelectedCategoryId {  get; set; }
        public int SelectedSupplierId {  get; set; }
        public int SelectedStatusId {  get; set; }




    }
}
