using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EuromilhoesAPI.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EuromilhoesAPI.Controllers
{
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly ApiContext _context;

        public ResultsController(ApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves latest prize draw.
        /// </summary>
        /// <returns>The latest prize draw.</returns>
        /// <response code="404">The prize draw does not exist.</response>
        /// <response code="200">The prize draw was retrieved with success.</response>
        [HttpGet("last")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLastResultAsync(CancellationToken cancellationToken)
        {
            try
            {
                var prizeDraw = await _context.PrizeDraws.OrderByDescending(x => x.Id).FirstOrDefaultAsync(cancellationToken);

                if (prizeDraw == null)
                {
                    return NotFound();
                }

                return Ok(new Result(prizeDraw));
            }
            catch(TaskCanceledException)
            {
            }

            return BadRequest();
        }


        /// <summary>
        /// Retrieves prize draw from specified date.
        /// </summary>
        /// <param name="date">The date of the prize draw in dd-MM-yyyy format.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The prize draw from the specified date.</returns>
        /// <response code="404">The prize draw does not exist.</response>
        /// <response code="200">The prize draw was retrieved with success.</response>
        [HttpGet("{date}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSpecificResultAsync(string date, CancellationToken cancellationToken)
        {
            try
            {
                var prizeDraw = await _context.PrizeDraws.FirstOrDefaultAsync(x => x.Date == date, cancellationToken);

                if (prizeDraw == null)
                {
                    return NotFound();
                }

                return Ok(new Result(prizeDraw));
            }
            catch(TaskCanceledException)
            {
            }

            return BadRequest();
        }


        /// <summary>
        /// Retrieves prize draws from specified year.
        /// </summary>
        /// <param name="year">The year you want to get results for.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A list with the year's prize draws.</returns>
        /// <response code="404">There's no entries for specified year.</response>
        /// <response code="200">The prize draws were retrieved with success.</response>
        [HttpGet("all/{year}")]
        [ProducesResponseType(typeof(IEnumerable<Result>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetYearResultsAsync(string year, CancellationToken cancellationToken)
        {
            try
            {
                var prizeDraws = await _context.PrizeDraws.Where(x => x.Date.Contains(year)).ToListAsync(cancellationToken);

                if (!prizeDraws.Any())
                {
                    return NotFound();
                }

                return Ok(prizeDraws.Select(prizeDraw => new Result(prizeDraw)).ToList());
            }
            catch(TaskCanceledException)
            {
            }

            return BadRequest();
        }
    }
}