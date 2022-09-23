using Empresa_X_API.Data;
using Empresa_X_API.Repository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Empresa_X_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly AppDbContext _db;


        public ClientController(AppDbContext db) 
        { 
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                var lista = await _db.clients.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientsByID(int id) 
        {
            try { 
                var lista = await _db.clients.FirstOrDefaultAsync(x => x.Id == id);

                return Ok(lista);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
           }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientByID(int id)
        {
            try
            {
                //var direction = await _db.directions.Where(x => x.clientId == id).ToListAsync();
                var client = await _db.clients.SingleOrDefaultAsync(x => x.Id == id);
                _db.Remove(client);
                await _db.SaveChangesAsync();

                return Ok(new JsonResult("Cliente Eliminado"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteClientByIDClient/{id}")]
        public async Task<IActionResult> DeleteClientByIDClient(int id)
        {
            try
            {
                var client = await _db.clients.SingleOrDefaultAsync(x => x.Id == id);
                var directions = await _db.directions.Where(x => x.clientId == client.Id).ToListAsync();

                _db.RemoveRange(directions);
                await _db.SaveChangesAsync();
                _db.Remove(client);
                await _db.SaveChangesAsync();

                return Ok(new JsonResult("Cliente Eliminado"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClientById([FromRoute] int id, [FromBody] Clients clients)
        {
            try
            {
                var client = await _db.clients.FindAsync(id);
                if (client != null)
                {
                    client.cedula = clients.cedula;
                    client.fName = clients.fName;
                    client.lName = clients.lName;
                    client.cellphoneNumber = clients.cellphoneNumber;

                    _db.Update(client);
                    await _db.SaveChangesAsync();
                }

                return Ok(new JsonResult("Cliente Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] Clients clients)
        {
            try
            {
                await _db.AddAsync(clients);
                await _db.SaveChangesAsync();

                return Ok(new JsonResult("Cliente Created"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    
    
}
