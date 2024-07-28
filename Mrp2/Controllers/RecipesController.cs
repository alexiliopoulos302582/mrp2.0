using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mrp2.Data;
using Mrp2.Models;

namespace Mrp2.Controllers
{
    public class RecipesController : Controller
    {
        private readonly DataContextEF _context;

        public RecipesController(DataContextEF context)
        {
            _context = context;
        }

        // GET: Recipes
        public async Task<IActionResult> Index(string searchString)
        {
            /* var recipes = await _context.Recipe
          .Include(r => r.recipeRawMaterials)
          .ThenInclude(rr => rr.RawMaterial)
          .ToListAsync();*/

            var recipesQuery = _context.Recipe
        .Include(r => r.recipeRawMaterials)
        .ThenInclude(rr => rr.RawMaterial)
        .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                recipesQuery = recipesQuery.Where(r => r.RecipeName.Contains(searchString));
            }
            var recipes = await recipesQuery.ToListAsync();

            var recipeViewModels = recipes.Select(r => new RecipeViewModel
            {
                Id = r.Id,
                RecipeName = r.RecipeName,
                TotalCost = r.TotalCost,
                Quantity = r.Quantity,
                RawMaterials = r.recipeRawMaterials.Select(rr => new RecipeRawMaterialViewModel
                {
                    RawMaterialId = rr.RawMaterialId,
                    RawMaterialName = rr.RawMaterial.Name, // Assuming `Name` is a property of `RawMaterial`
                    Quantity = rr.Quantity,
                    Cost = rr.Cost
                }).ToList()
            }).ToList();

            return View(recipeViewModels);
        }





        // GET: Recipes/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe.Include(x=>x.recipeRawMaterials).ThenInclude(x=>x.RawMaterial)

                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            var viewmodel = new RecipeViewModel
            {
                Id = recipe.Id,
                RecipeName = recipe.RecipeName,
                TotalCost = recipe.TotalCost,
                Quantity = recipe.Quantity,
                RawMaterials = recipe.recipeRawMaterials.Select(x => new RecipeRawMaterialViewModel
                {
                    RawMaterialId = x.RawMaterialId,
                    RawMaterialName = x.RawMaterial.Name,
                    Quantity = x.Quantity,
                    Cost = x.Cost,
                }).ToList()



            };

            return View(viewmodel);
        }



         

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var rawMaterials = await _context.RawMaterial
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.Id} - {x.Name} - (unit cost: {x.UnitCost}) "
                }).ToListAsync();

            var viewModel = new RecipeViewModel
            {
                RawMaterialOptions = rawMaterials,
                RawMaterials = new List<RecipeRawMaterialViewModel> { new RecipeRawMaterialViewModel() } // Initialize with one empty item
            };

            return View(viewModel);
        }



        [HttpGet]
        public IActionResult GetUnitCost(int rawMaterialId)
        {
            var unitCost = _context.RawMaterial.Where(r => r.Id == rawMaterialId)
                .Select(r => r.UnitCost).FirstOrDefault();
            return Json(new { unitCost });
        }


        








         

        [HttpPost]
        public async Task<IActionResult> Create(RecipeViewModel model)
        {

            try
            {
                var recipe = new Recipe
                {
                    RecipeName = model.RecipeName,
                    TotalCost = model.TotalCost,
                    Quantity = model.Quantity,
                };
                _context.Add(recipe); 
                await _context.SaveChangesAsync();

                foreach(var item in model.RawMaterials)
                {
                    var reciperawMaterial = new RecipeRawMaterial
                    {
                        RecipeId = recipe.Id,
                        RawMaterialId = item.RawMaterialId,
                        Quantity = item.Quantity,
                        Cost = item.Cost,
                    };
                    _context.Add(reciperawMaterial);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {
                model.RawMaterialOptions = await _context.RawMaterial
                   .Select(rm => new SelectListItem
                   {
                       Value = rm.Id.ToString(),
                       Text = rm.Name
                   })
                   .ToListAsync();
                return RedirectToAction(nameof(Index));
              
            }

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RecipeName,TotalCost,Quantity")] Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.Id))
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
            return View(recipe);
        }




        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }




        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipe.Remove(recipe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipe.Any(e => e.Id == id);
        }
    }
}
