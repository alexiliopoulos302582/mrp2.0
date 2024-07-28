using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mrp2.Data;
using Mrp2.Models;

namespace Mrp2.Controllers
{

    [Authorize]
    public class RawMaterialsController : Controller
    {
        private readonly DataContextEF _context;

        public RawMaterialsController(DataContextEF context)
        {
            _context = context;
        }



        // GET: RawMaterials
        public async Task<IActionResult> Index(string searchString)
        {
            var rawMaterials = from m in _context.RawMaterial select m;

            if(!string.IsNullOrEmpty(searchString))

            {
                rawMaterials = rawMaterials.Where(x=>x.Name.Contains(searchString));

            }

            return View(await  rawMaterials.ToListAsync());
        }




        // GET: RawMaterials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rawMaterial = await _context.RawMaterial
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rawMaterial == null)
            {
                return NotFound();
            }

            return View(rawMaterial);
        }





        // GET: RawMaterials/Create
        public async Task<IActionResult> Create()
        {
            var units = await _context.Units.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = $"{x.Id} - {x.Name}"
            }).ToListAsync();

            var suppliers = await _context.Suppliers.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = x.Name
            }).ToListAsync();

            var status = await _context.Status.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = x.Name
            }).ToListAsync();

            var category = await _context.Category.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = x.Name
            }).ToListAsync();




            var model = new RawMaterialViewModel
            {
                RawMaterial = new RawMaterial(),
                Units = units, Suppliers = suppliers,Categories=category,Statuses=status
            };


            return View(model);
        }




         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( RawMaterial rawMaterial)
        {
            
            
                _context.Add(rawMaterial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }





        // GET: RawMaterials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var rawMaterial = await _context.RawMaterial.FindAsync(id);
            if (rawMaterial == null)
            {
                return NotFound();
            }

            var units = await _context.Units.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = $"{x.Id} - {x.Name}"
            }).ToListAsync();

            var suppliers = await _context.Suppliers.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = x.Name
            }).ToListAsync();

            var status = await _context.Status.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = x.Name
            }).ToListAsync();

            var category = await _context.Category.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = x.Name
            }).ToListAsync();

            var viewModel = new RawMaterialViewModel
            {
                RawMaterial = rawMaterial,
                Units = units,
                Suppliers = suppliers,
                Statuses = status,
                Categories = category
            };


            return View(viewModel);
        }


         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Notes,Quantity,BoundQTY,Category,Expiration,LeadTime,Location,ReorderPoint,SafetyStock,Status,Supplier,UnitCost,Eoq,Unit")] RawMaterial rawMaterial)
        {
            if (id != rawMaterial.Id)
            {
                return NotFound();
            }

          
          
                try
                {
                    _context.Update(rawMaterial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RawMaterialExists(rawMaterial.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
          
          
        }




        // GET: RawMaterials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rawMaterial = await _context.RawMaterial
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rawMaterial == null)
            {
                return NotFound();
            }

            return View(rawMaterial);
        }




        // POST: RawMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rawMaterial = await _context.RawMaterial.FindAsync(id);
            if (rawMaterial != null)
            {
                _context.RawMaterial.Remove(rawMaterial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RawMaterialExists(int id)
        {
            return _context.RawMaterial.Any(e => e.Id == id);
        }
    }
}
