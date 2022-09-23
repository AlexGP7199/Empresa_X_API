using Empresa_X_API.Data;
using Empresa_X_API.Repository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Empresa_X_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectionsController : ControllerBase
    {
        private readonly AppDbContext _db;


        public DirectionsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDirections()
        {
            try
            {
                var lista = await _db.directions.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDirecctionById(int id)
        {
            try {

                var lista = await _db.directions.FirstOrDefaultAsync(x => x.Id == id);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("DirecctionsByClientId/{id}")]
        public async Task<IActionResult> GetDirecctionsByClientId(int id)
        {
            try
            {

                var lista = await _db.directions.Where(x => x.clientId == id).ToListAsync();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirectionByID (int id)
        {
            try
            {
                var direction = await _db.directions.SingleOrDefaultAsync(x => x.Id == id);
                _db.Remove(direction);
                await _db.SaveChangesAsync();

                return Ok(new JsonResult("Direccion Eliminada"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDirectionById([FromRoute] int id, [FromBody] Directions directions)
        {
            try {
                var direction = await _db.directions.FindAsync(id);
                if (direction != null)
                {
                    direction.clientId = directions.clientId;
                    direction.direction = directions.direction;

                    _db.Update(direction);
                    await _db.SaveChangesAsync();
                }

                return Ok(new JsonResult("Direccion Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateNewDirection([FromBody] Directions directions)
        {
            try
            {
                await _db.AddAsync(directions);
                await _db.SaveChangesAsync();

                return Ok(new JsonResult("Direction Created"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }


    
}
