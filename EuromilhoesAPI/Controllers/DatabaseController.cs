using System.Linq;
using System.Threading.Tasks;
using EuromilhoesAPI.Authentication;
using Microsoft.AspNetCore.Mvc;
using EuromilhoesAPI.Context;
using EuromilhoesAPI.Context.Models;

namespace EuromilhoesAPI.Controllers
{
    [TypeFilter(typeof(RequiresDbAccessAttribute))]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly DatabaseAccess _dbAccess;

        public DatabaseController(ApiContext context, DatabaseAccess dbAccess)
        {
            _context = context;
            _dbAccess = dbAccess;
        }

        // GET: api/Database/5
        [HttpGet]
        public async Task<IActionResult> GetPrizeDraw(int id, string token)
        {
            //if (!_dbAccess.HasAccess(token))
            //    return StatusCode(403);

            var prizeDraw = await _context.PrizeDraws.FindAsync(id);

            if (prizeDraw == null)
            {
                return NotFound();
            }

            return Ok(prizeDraw);
        }

        // POST: api/Database
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostPrizeDraw(PrizeDraw prizeDraw, string token)
        {
            //if (!_dbAccess.HasAccess(token))
            //    return StatusCode(403);

            if (PrizeDrawExists(prizeDraw))
            {
                return StatusCode(409, $"Prize draw for {prizeDraw.Date} already exists.");
            }

            await _context.PrizeDraws.AddAsync(prizeDraw);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrizeDraw", new { id = prizeDraw.Id }, prizeDraw);
        }

        // DELETE: api/Database/5
        [HttpDelete]
        public async Task<IActionResult> DeletePrizeDraw(int id, string token)
        {
            //if (!_dbAccess.HasAccess(token))
            //    return StatusCode(403);

            var prizeDraw = await _context.PrizeDraws.FindAsync(id);
            if (prizeDraw == null)
            {
                return NotFound();
            }

            _context.PrizeDraws.Remove(prizeDraw);
            await _context.SaveChangesAsync();

            return NoContent();
        }
          
        private bool PrizeDrawExists(PrizeDraw prizeDraw)
        {
            return _context.PrizeDraws.Any(pd => pd.Date == prizeDraw.Date);
        }
    }
}
