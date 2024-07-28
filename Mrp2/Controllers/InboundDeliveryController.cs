using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mrp2.Data;
using Mrp2.Models;

namespace Mrp2.Controllers
{
    public class InboundDeliveryController : Controller
    {

        private readonly DataContextEF _context;


        public InboundDeliveryController(DataContextEF context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            PopulateRawMaterials();
            ViewBag.Suppliers = new SelectList(_context.Suppliers, "Id", "Name");
            return View("Create");

        }

        private void PopulateRawMaterials()
        {
            var rawMaterials = _context.RawMaterial.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = $"{r.Id} - {r.Name} - unit: {r.Unit}"
            }).ToList();

            ViewBag.RawMaterials = new SelectList(rawMaterials, "Value", "Text");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InboundDeliveryViewModel model)
        {

            try
            {
                foreach (var item in model.RawMaterials)
                {
                    var rawMaterial = _context.RawMaterial.Find(item.RawMaterialId);
                    if (rawMaterial != null)
                    {
                        rawMaterial.Quantity += item.Quantity;
                        _context.Update(rawMaterial);
                    }
                }
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Quantities updated successfully!";
                return RedirectToAction(nameof(Create));

            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }



        }











    }
}
