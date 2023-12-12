using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClanNinjasDto>))]
        public async Task<IActionResult> GetClans()
        {
            try
            {
                _logger.LogInformation("Getting clans...");


                var clans = _mapper.Map<List<ClanNinjasDto>>(await _clanRepository.GetClans());

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
        [ProducesResponseType(200, Type = typeof(ClanNinjasDto))]
        public async Task<IActionResult> GetClan(int clanId)
        {
            try
            {
                if (!_clanRepository.ClanExists(clanId))
                    return NotFound();

                var clan = _mapper.Map<ClanNinjasDto>(await _clanRepository.GetClanById(clanId));


                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(clan);
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponsed { errorCode = -1, errorMessage = ex.Message, errorName = "Error" });
            }


        }


        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BaseResponsed))]
        public async Task<IActionResult> CreateClan([FromBody] ClanDto clanCreate)
        {
            try
            {
                if (clanCreate == null)
                    return BadRequest(ModelState);

                var clans = await _clanRepository.GetClans();
                var clan = clans.FirstOrDefault(c => c.Name.Trim().ToLower() == clanCreate.Name.TrimEnd().ToLower());
                if (clan != null)
                {
                    ModelState.AddModelError("", "Clan alredy exists");
                    return StatusCode(422, ModelState);
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var clanMap = _mapper.Map<Clan>(clanCreate);

                if (!_clanRepository.Insert(clanMap))
                {
                    ModelState.AddModelError("", "Something went wrong while savin");
                    return StatusCode(500, ModelState);
                }

                return Ok(new BaseResponsed { errorCode = 0, errorMessage = "Successfully created", errorName = "Ok" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponsed { errorCode = -1, errorMessage = ex.Message, errorName = "Error" });
            }
           
        }


        [HttpPut("{clanId}")]
        [ProducesResponseType(200, Type = typeof(BaseResponsed))]
        public IActionResult UpdateClan(int clanId, [FromBody] ClanDto updateClan)
        {
            if (updateClan == null)
                return Ok(new BaseResponsed { errorCode = 2, errorMessage = "ClanNull", errorName = "Error" });
            if (clanId != updateClan.Id)
                return Ok(new BaseResponsed { errorCode = 1, errorMessage = "CompareClanID", errorName = "Error" });
            if (!_clanRepository.ClanExists(clanId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clanMap = _mapper.Map<Clan>(updateClan);

            if (!_clanRepository.Update(clanMap))
            {
                return Ok(new BaseResponsed { errorCode = -1, errorMessage = "Something went wrong updating category", errorName = "Error" });
            }

            return Ok(new BaseResponsed { errorCode = 0, errorName = "noError" });
        }


        [HttpDelete("{clanId}")]
        /*[ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]*/
        public async Task<IActionResult> DeleteClan(int clanId)
        {
            if (!_clanRepository.ClanExists(clanId))
                return NotFound();

            var clanToDelete = await _clanRepository.GetClanById(clanId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_clanRepository.Delete(clanToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();

        }


    }

}
