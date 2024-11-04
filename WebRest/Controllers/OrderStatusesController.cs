using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using WebRestEF.EF.Data;
using WebRestEF.EF.Models;
using WebRest.Interfaces;
namespace WebRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusesController : ControllerBase, iController<OrderStatus>
    {
        private readonly WebRestOracleContext _context;

        public OrderStatusesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrderStatuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatus>>> Get()
        {
            return await _context.OrderStatuses.ToListAsync();
        }

        // GET: api/OrderStatuses/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<OrderStatus>> Get(string id)
        {
            var OrderStatus = await _context.OrderStatuses.FindAsync(id);

            if (OrderStatus == null)
            {
                return NotFound();
            }

            return OrderStatus;
        }

        // PUT: api/OrderStatuses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, OrderStatus OrderStatus)
        {
            if (id != OrderStatus.OrderStatusId)
            {
                return BadRequest();
            }
            _context.OrderStatuses.Update(OrderStatus);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStatusExists(id))
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

        // POST: api/OrderStatuses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderStatus>> Post(OrderStatus OrderStatus)
        {
            _context.OrderStatuses.Add(OrderStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderStatus", new { id = OrderStatus.OrderStatusId }, OrderStatus);
        }

        // DELETE: api/OrderStatuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var OrderStatus = await _context.OrderStatuses.FindAsync(id);
            if (OrderStatus == null)
            {
                return NotFound();
            }

            _context.OrderStatuses.Remove(OrderStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderStatusExists(string id)
        {
            return _context.OrderStatuses.Any(e => e.OrderStatusId == id);
        }
    }
}
