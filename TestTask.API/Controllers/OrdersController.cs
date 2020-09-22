using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly Data.DataContext _context;        

        public OrdersController(Data.DataContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Order>>> GetOrders()
        {
            var res = await _context.Orders.Include(s => s.OrderRows).ToListAsync();

            #region AddTestData
                if (res == null || res?.Count == 0)
                {
                    res = new List<Order>();
                    res.Add(new Order());
                    res[0].OrderRows.Add(new OrderRow
                    {
                        OrderRowID = Guid.NewGuid(),
                        Order = res[0],
                        OrderID = res[0].OrderID,
                        ProductName = "Product 1",
                        ProductPrice = (decimal)23.0,
                        ProductQuantity = 12
                    }); ;
                    res[0].OrderRows.Add(new OrderRow
                    {
                        OrderRowID = Guid.NewGuid(),
                        Order = res[0],
                        OrderID = res[0].OrderID,
                        ProductName = "Product 2",
                        ProductPrice = (decimal)25.5,
                        ProductQuantity = 16
                    });
                    
                    res.Add(new Order());
                    res[1].OrderRows.Add(new OrderRow
                    {
                        OrderRowID = Guid.NewGuid(),
                        Order = res[1],
                        OrderID = res[1].OrderID,
                        ProductName = "Product 2_1",
                        ProductPrice = (decimal)0.3,
                        ProductQuantity = 19
                    }); ;
                    res[1].OrderRows.Add(new OrderRow
                    {
                        OrderRowID = Guid.NewGuid(),
                        Order = res[1],
                        OrderID = res[1].OrderID,
                        ProductName = "Product 2_2",
                        ProductPrice = (decimal)125.5,
                        ProductQuantity = 3
                    });
                _context.Orders.AddRange(res);
                _context.SaveChanges();
                res = await _context.Orders.ToListAsync();
            }
            #endregion
                            

            return res;
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.Include(s => s.OrderRows).FirstOrDefaultAsync(s => s.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderID)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderID }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }
    }
}
