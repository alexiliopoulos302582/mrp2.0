using Microsoft.AspNetCore.Mvc;
using Mrp2.Data;

namespace Mrp2.Controllers
{
    public class InventoryController : Controller
    {
        private readonly DataContextEF _context;


        public InventoryController(DataContextEF context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var rawMaterials = _context.RawMaterial.ToList();
            return View("Inventory", rawMaterials);
        }


        [HttpGet]
        public IActionResult GetLeadTime(int rawMaterialId) 
        {
            var leadTime = _context.RawMaterial
                .Where(rm=>rm.Id==rawMaterialId)
                .Select(rm=>rm.LeadTime).FirstOrDefault();

            return Json(new { leadTime });

        }

        public IActionResult updateEOQROP(int rawMaterialId,decimal eoq,
            decimal reorderpoint,decimal safetystock) 
        {

            try
            {
                var rawMaterial = _context.RawMaterial.Find(rawMaterialId);
                if(rawMaterial == null)
                {
                    return NotFound();
                }

                rawMaterial.Eoq = eoq;
                rawMaterial.ReorderPoint = reorderpoint;
                rawMaterial.SafetyStock = safetystock;
                _context.SaveChanges();
                return Ok();


            }catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }




        }














    }
}
