using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NinjaWikiAPI.Data;
using NinjaWikiAPI.Interface;
using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ClanController : Controller
    {
        private readonly ILogger<ClanController> _logger;
        private readonly IClanRepository _clanRepository;

        public ClanController(ILogger<ClanController> logger, IClanRepository clanRepository)
        {
            _logger = logger;
            _clanRepository = clanRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Clan))]
        public async Task<IActionResult> GetClans()
        {
            try
            {
                _logger.LogInformation("Getting clans...");


                var clans = await _clanRepository.GetClans();

                if (clans == null)
                {
                    _logger.LogWarning("No clans found.");
                    return NotFound();
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _logger.LogInformation("Returning clans.");

                return Ok(clans);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting clans.");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }

}
