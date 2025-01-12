using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITB22003Application;
using ITB2203Application.Models;

namespace ITB2203Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouriersController : ControllerBase
    {
        private readonly DataContext _context;

        public CouriersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Couriers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Courier>>> GetCouriers()
        {
            return await _context.Couriers.ToListAsync();
        }

        // GET: api/Couriers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Courier>> GetCourier(int id)
        {
            var courier = await _context.Couriers.FindAsync(id);

            if (courier == null)
            {
                return NotFound();
            }

            return courier;
        }

        // PUT: api/Couriers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourier(int id, Courier courier)
        {
            if (id != courier.Id)
            {
                return BadRequest();
            }

            _context.Entry(courier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourierExists(id))
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

        // POST: api/Couriers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Courier>> PostCourier(Courier courier)
        {
            if(!ModelState.IsValid){
                return BadRequest();
            }
            _context.Couriers.Add(courier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourier", new { id = courier.Id }, courier);
        }

        // DELETE: api/Couriers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourier(int id)
        {
            var courier = await _context.Couriers.FindAsync(id);
            if (courier == null)
            {
                return NotFound();
            }

            _context.Couriers.Remove(courier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourierExists(int id)
        {
            return _context.Couriers.Any(e => e.Id == id);
        }

         [HttpGet("{id}/resource")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
             //leia kõiki Ordereid kust courier võttis tellimusi. 
            //grupeerida samasugudeid id ühe listi ja ühe querisse panna id-sid ja summa kordi
            //tagastab restoran, kus OrderBy esimene Orderite hulgast ? 
            var courier = await _context.Couriers.FindAsync(id);
            if(courier == null){
                return NotFound();
            }
            var orders = await _context.Orders.Where(x => x.CourierId == id).ToListAsync();
            if(orders.Count == 0){
                return NotFound();
            }
            var orderIds = orders.Select(x => x.Id).ToList();
            var orderRestaurantIds = await _context.Orders.Where(x=> orderIds.Contains(x.Id)).Select(x=>x.restaurantId).ToListAsync();
            var restaurantId = orderRestaurantIds.GroupBy(x => x).OrderByDescending(x => x.Count()).FirstOrDefault()?.Key;
            var restaurant = await _context.Restaurants.Where(x=> x.Id == restaurantId).FirstOrDefaultAsync();
            if(restaurant == null){
                return NotFound();
            }
            return restaurant;
        }


    }
}
