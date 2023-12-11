using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NinjaWikiAPI.Data;
using NinjaWikiAPI.Dto;
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
        private readonly IMapper _mapper;

        public ClanController(ILogger<ClanController> logger, IClanRepository clanRepository, IMapper mapper)
        {
            _logger = logger;
            _clanRepository = clanRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClanDto>))]
        public async Task<IActionResult> GetClans()
        {
            try
            {
                _logger.LogInformation("Getting clans...");


                var clans = _mapper.Map<List<ClanDto>>(await _clanRepository.GetClans());

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
       
        [HttpGet("{clanId}")]
        [ProducesResponseType(200, Type = typeof(ClanDto))]
        //[ProducesResponseType(400)]
        public async Task<IActionResult> GetClan(int clanId)
        {
            try
            {
                if (!_clanRepository.ClanExists(clanId))
                    return NotFound();

                var clan = _mapper.Map<ClanDto>(await _clanRepository.GetClanById(clanId));

                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(clan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting clan.");
                return StatusCode(500, "Internal Server Error");
            }


        }
    }

}
