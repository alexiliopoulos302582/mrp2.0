using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mrp2.Data;
using Mrp2.Models;

namespace Mrp2.Controllers
{
    public class OrdersController : Controller
    {
        private readonly DataContextEF _context;

        public OrdersController(DataContextEF context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(string searchString)
        {

            /* var orders = await _context.Order.Include(
             x => x.OrderRawMaterials).ThenInclude(x => x.RawMaterial).Include(x => x.Recipe).ToListAsync();*/

            var orders = from x in _context.Order.Include(x => x.OrderRawMaterials)
                         .ThenInclude(x => x.RawMaterial).Include(x => x.Recipe)
                         select x;



            if (!string.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(o => o.Name.Contains(searchString)); // Adjust property as needed
            }

            return View(await orders.ToListAsync());
        }








        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var order = await _context.Order
        .Include(o => o.Recipe)
            .ThenInclude(r => r.recipeRawMaterials)
                .ThenInclude(rrm => rrm.RawMaterial)
        .Include(o => o.OrderRawMaterials)
            .ThenInclude(o => o.RawMaterial)
        .FirstOrDefaultAsync(o => o.Id == id);

            if (id == null)
            {
                return NotFound();
            }

            var reciperawmaterials = await _context.recipeRawMaterials.Where(x => x.RecipeId == order.RecipeId)
                .Include(x => x.RawMaterial).ToListAsync();

            var orderViewModel = new OrderViewModel

            {
                Id = order.Id,
                Name = order.Name,
                Po = order.Po,
                Notes = order.Notes,
                RecipeId = order.RecipeId,
                RawMaterialCost = order.RawMaterialCost,
                RecipeCost = order.RecipeCost,
                Quantity = order.Quantity,
                OrderTotalCost = order.OrderTotalCost,
                RawMaterialsIds = order.OrderRawMaterials.Select(orm => orm.RawMaterialId).ToList(),
                Quantities = order.OrderRawMaterials.Select(orm => orm.Quantity).ToList(),
                UnitCosts = order.OrderRawMaterials.Select(orm => orm.UnitCost).ToList(),
                Recipes = order.Recipe != null ? new List<SelectListItem>

            {
                new SelectListItem
                {
                    Text = order.Recipe.RecipeName,
                    Value = order.Recipe.Id.ToString()
                   }
                   } : new List<SelectListItem>(),


                RawMaterials = order.OrderRawMaterials.Select(orm => new SelectListItem
                {
                    Text = orm.RawMaterial.Name,
                    Value = orm.RawMaterialId.ToString()
                }).ToList(),
                RecipeRawMaterials = reciperawmaterials.Select(z => new SelectListItem
                {
                    Text = $"{z.RawMaterial.Name} - Quantity: {z.Quantity}",
                    Value = z.RawMaterialId.ToString()
                }).ToList()


            };

            return View(orderViewModel);
        }





        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            var model = new OrderViewModel
            {
                Recipes = await _context.Recipe.Select(r =>
                new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = $"{r.Id} - {r.RecipeName} - Cost: {r.TotalCost} "

                }).ToListAsync(),
                RawMaterials = await _context.RawMaterial.Select(rm => new SelectListItem
                {
                    Value = rm.Id.ToString(),
                    Text = $"{rm.Id} - {rm.Name} -  {rm.Unit} - Cost: {rm.UnitCost} "
                }).ToListAsync()
            };
            return View(model);
        }



        [HttpPost]
        [Route("Orders/Create")]
        public async Task<IActionResult> Create(OrderViewModel model)
        /*List<int> RawMaterialIDs, List<decimal> Quantites*/

        {
            try
            {
                var order = new Order
                {

                    Name = model.Name,
                    Po = model.Po,
                    Notes = model.Notes,
                    Created = DateTime.Now,
                    RecipeId = model.RecipeId,
                    RecipeCost = model.RecipeCost,
                    RawMaterialCost = model.RawMaterialCost,
                    Quantity = model.Quantity,
                    OrderTotalCost = model.OrderTotalCost,
                    OrderRawMaterials = model.RawMaterialsIds.Select((rawMaterialId, index) => new OrderRawMaterial
                    {
                        RawMaterialId = rawMaterialId,
                        Quantity = model.Quantities[index],
                        UnitCost = model.UnitCosts[index],
                    }).ToList(),
                };

                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,
                    "An error occurred while creating the order. Please try again.");
                // Repopulate dropdown lists
                model.Recipes = await _context.Recipe.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.RecipeName
                }).ToListAsync();
                model.RawMaterials = await _context.RawMaterial.Select(rm => new SelectListItem
                {
                    Value = rm.Id.ToString(),
                    Text = rm.Name
                }).ToListAsync();
                return View(model);

            }








        }








        [HttpGet]
        public IActionResult GetRecipeCost(int recipeId)
        {
            var totalcost = _context.Recipe.Where(r => r.Id == recipeId)
                .Select(r => r.TotalCost).FirstOrDefault();
            return Json(new { totalcost });
        }


        [HttpGet]
        public IActionResult GetUnitCost(int rawMaterialId)
        {
            var unitCost = _context.RawMaterial
                                .Where(r => r.Id == rawMaterialId)
                                .Select(r => r.UnitCost)
                                .FirstOrDefault();

            return Json(new { unitCost });
        }








        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Po,Notes,Created")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }
            try
            {
                var existingOrder = await _context.Order.FindAsync(id);
                if (existingOrder == null)
                {
                    return NotFound();
                }

                // Update only the fields that are editable
                existingOrder.Name = order.Name;
                existingOrder.Po = order.Po;
                existingOrder.Notes = order.Notes;
                existingOrder.Created = order.Created; // Assuming this field is editable, which might not be typical

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw new Exception();
            }
            return RedirectToAction(nameof(Index));
        }




        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReserveMaterials(int id)
        {
            var order = await _context.Order
                    .Include(o => o.Recipe)
                    .ThenInclude(o => o.recipeRawMaterials)
                    .ThenInclude(o => o.RawMaterial)
                    .Include(o => o.OrderRawMaterials)
                    .ThenInclude(o => o.RawMaterial).FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();

            }

            if (order.OrderCompleted)
            {
                // Optionally, you can return an error message or redirect to an error page
                return RedirectToAction("OrderCompleted"); // Or an appropriate view
            }


            if (order.MaterialsReserved)
            {

                return NotFound();


            }


            var rawMaterialsToUpdate = order.Recipe?.recipeRawMaterials.Select(rrm => rrm.RawMaterial)
                .Union(order.OrderRawMaterials.Select(orm => orm.RawMaterial))
                .ToList();

            if (rawMaterialsToUpdate != null)
            {
                // Order quantity used for recipe raw materials
                var orderQuantity = order.Quantity;

                foreach (var rawMaterial in rawMaterialsToUpdate)
                {
                    var orderRawMaterial = order.OrderRawMaterials.FirstOrDefault(orm => orm.RawMaterialId == rawMaterial.Id);



                    if (orderRawMaterial != null)
                    {
                        rawMaterial.BoundQTY += orderRawMaterial.Quantity;
                    }

                    var recipeRawMaterial = order.Recipe?.recipeRawMaterials
                     .FirstOrDefault(rrm => rrm.RawMaterialId == rawMaterial.Id);

                    if (recipeRawMaterial != null)
                    {
                        rawMaterial.BoundQTY += recipeRawMaterial.Quantity * orderQuantity;

                    }
                }
                order.MaterialsReserved = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Details", new { id = id });

        }


        public IActionResult MaterialsAlreadyReserved()
        {
            return View();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnMaterials(int id)
        {
            var order = await _context.Order
                .Include(o => o.Recipe)
                .ThenInclude(o => o.recipeRawMaterials)
                .ThenInclude(o => o.RawMaterial)
                .Include(o => o.OrderRawMaterials)
                .ThenInclude(o => o.RawMaterial)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }
            if (order.OrderCompleted)
            {

                /*return View("OrderCompleted");*/
                return NotFound();
            }

            if (!order.MaterialsReserved)
            {
                return NotFound();
            }

            var rawMaterialsToUpdate = order.Recipe?.recipeRawMaterials
                .Select(rrm => rrm.RawMaterial)
                .Union(order.OrderRawMaterials.Select(orm => orm.RawMaterial))
                .ToList();

            if (rawMaterialsToUpdate != null)
            {
                var orderQuantity = order.Quantity;


                foreach (var rawMaterial in rawMaterialsToUpdate)
                {
                    var orderRawMaterial = order.OrderRawMaterials
                        .FirstOrDefault(orm => orm.RawMaterialId == rawMaterial.Id);
                    var recipeRawMaterial = order.Recipe?.recipeRawMaterials
                        .FirstOrDefault(rrm => rrm.RawMaterialId == rawMaterial.Id);

                    if (orderRawMaterial != null)
                    {
                        rawMaterial.BoundQTY -= orderRawMaterial.Quantity;
                        if (rawMaterial.BoundQTY < 0)
                        {
                            rawMaterial.BoundQTY = 0;
                        }
                    }

                    if (recipeRawMaterial != null)
                    {
                        rawMaterial.BoundQTY -= recipeRawMaterial.Quantity * orderQuantity;
                        if (rawMaterial.BoundQTY < 0)
                        {
                            rawMaterial.BoundQTY = 0;
                        }
                    }
                }
                order.MaterialsReserved = false;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id = id });
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderCompleted(int id)
        {



            var order = await _context.Order
                    .Include(o => o.Recipe)
                    .ThenInclude(o => o.recipeRawMaterials)
                    .ThenInclude(o => o.RawMaterial)
                    .Include(o => o.OrderRawMaterials)
                    .ThenInclude(o => o.RawMaterial).FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();

            }

            if (order.OrderCompleted)
            {

                return NotFound();


            }

            var rawMaterialsToUpdate = order.Recipe?.recipeRawMaterials.Select(rrm => rrm.RawMaterial)
                .Union(order.OrderRawMaterials.Select(orm => orm.RawMaterial))
                .ToList();

            if (rawMaterialsToUpdate != null)
            {
                var orderQuantity = order.Quantity;
                foreach (var rawMaterial in rawMaterialsToUpdate)
                {
                    var orderRawMaterial = order.OrderRawMaterials.FirstOrDefault(orm => orm.RawMaterialId == rawMaterial.Id);
                    var recipeRawMaterial = order.Recipe?.recipeRawMaterials
                        .FirstOrDefault(rrm => rrm.RawMaterialId == rawMaterial.Id);

                    if (orderRawMaterial != null)
                    {
                        if (order.MaterialsReserved)
                        {

                            rawMaterial.BoundQTY -= orderRawMaterial.Quantity;
                        }
                        rawMaterial.Quantity -= orderRawMaterial.Quantity;
                    }

                    if (recipeRawMaterial != null)
                    {
                        if (order.MaterialsReserved)
                        {

                            rawMaterial.BoundQTY -= recipeRawMaterial.Quantity * orderQuantity;
                        }
                        rawMaterial.Quantity -= recipeRawMaterial.Quantity * orderQuantity;

                    }
                }
                order.OrderCompleted = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Details", new { id = id });


        }



        [HttpGet]
        public async Task<IActionResult> ManualReturnMaterials(int id)
        {
            var order = await _context.Order
                .Include(o => o.Recipe)
                .ThenInclude(o => o.recipeRawMaterials)
                .ThenInclude(o => o.RawMaterial)
                .Include(o => o.OrderRawMaterials)
                .ThenInclude(o => o.RawMaterial)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null || order.OrderCompleted)
            {
                return View("MaterialsAlreadyReserved", "Materials are not reserved for this order.");
                return NotFound();
            }

            var rawMaterials = order.Recipe?.recipeRawMaterials
                .Select(rrm => new
                {
                    rrm.RawMaterialId,
                    rrm.RawMaterial.Name,
                    BoundQTY = rrm.RawMaterial.BoundQTY
                })
                .Union(order.OrderRawMaterials.Select(orm => new
                {
                    orm.RawMaterialId,
                    orm.RawMaterial.Name,
                    BoundQTY = orm.RawMaterial.BoundQTY
                }))
                .ToList();



            return View(new
            {
                OrderId = id,
                RawMaterials = rawMaterials
            });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManualReturnMaterials(int orderId, List<RawMaterialReturnViewModel> rawMaterials)
        {
            var order = await _context.Order
                .Include(o => o.Recipe)
                .ThenInclude(o => o.recipeRawMaterials)
                .ThenInclude(o => o.RawMaterial)
                .Include(o => o.OrderRawMaterials)
                .ThenInclude(o => o.RawMaterial)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null || order.OrderCompleted)
            {

                return NotFound();
            }

            foreach (var item in rawMaterials)
            {
                var rawMaterial = await _context.RawMaterial.FindAsync(item.RawMaterialId);
                if (rawMaterial != null)
                {

                    var orderRawMaterial = order.OrderRawMaterials.FirstOrDefault(x => x.RawMaterialId == rawMaterial.Id);
                    var recipeRawMaterial = order.Recipe?.recipeRawMaterials.FirstOrDefault(x => x.RawMaterialId == rawMaterial.Id);

                    /*var quantityToReturn = item.QuantityToReturn;*/
                    if (orderRawMaterial != null)
                    {
                        rawMaterial.BoundQTY -= orderRawMaterial.Quantity;
                        /*rawMaterial.Quantity -= quantityToReturn;*/
                    }
                    if (recipeRawMaterial != null)
                        {
                            rawMaterial.BoundQTY -= recipeRawMaterial.Quantity * order.Quantity;
                        }

                        // Ensure BoundQTY does not go below zero
                        if (rawMaterial.BoundQTY < 0)
                        {
                            rawMaterial.BoundQTY = 0;
                        }



                        // Subtract the user-input quantity from the stock
                        var quantityToReturn = item.QuantityToReturn;
                        if (quantityToReturn > 0)
                        {
                            rawMaterial.Quantity -= quantityToReturn;



                            _context.RawMaterial.Update(rawMaterial);
                        }
                    }
                }

                order.OrderCompleted = true;
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = orderId });
            }



        }
    }
 




/*

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> ManualReturnMaterials(int orderId, List<RawMaterialReturnViewModel> rawMaterials)
{
    var order = await _context.Order
        .Include(o => o.Recipe)
        .ThenInclude(o => o.recipeRawMaterials)
        .ThenInclude(o => o.RawMaterial)
        .Include(o => o.OrderRawMaterials)
        .ThenInclude(o => o.RawMaterial)
        .FirstOrDefaultAsync(o => o.Id == orderId);

    if (order == null || order.OrderCompleted)
    {

        return NotFound();
    }

    foreach (var item in rawMaterials)
    {
        var rawMaterial = await _context.RawMaterial.FindAsync(item.RawMaterialId);
        if (rawMaterial != null)
        {



            var quantityToReturn = item.QuantityToReturn;
            if (quantityToReturn > 0)
            {
                rawMaterial.BoundQTY -= quantityToReturn;
                rawMaterial.Quantity -= quantityToReturn;

                if (rawMaterial.BoundQTY < 0)
                {
                    rawMaterial.BoundQTY = 0;
                }

                _context.RawMaterial.Update(rawMaterial);
            }
        }
    }

    order.OrderCompleted = true;
    await _context.SaveChangesAsync();

    return RedirectToAction("Details", new { id = orderId });
}
*/