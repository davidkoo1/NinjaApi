using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NinjaWikiAPI.Dto;
using NinjaWikiAPI.Interface;
using NinjaWikiAPI.Models;

namespace NinjaWikiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NinjaController : Controller
    {
        private readonly INinjaRepository _ninjaRepository;
        private readonly IClanRepository _clanRepository;
        private readonly IRankRepository _rankRepository;
        private readonly IVillageRepository _villageRepository;
        private readonly IMapper _mapper;

        public NinjaController(IMapper mapper, 
                               INinjaRepository ninjaRepository, 
                               IClanRepository clanRepository, 
                               IRankRepository rankRepository,
                               IVillageRepository villageRepository)
        {
            _mapper = mapper;
            _ninjaRepository = ninjaRepository;
            _clanRepository = clanRepository;
            _rankRepository = rankRepository;
            _villageRepository = villageRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<NinjaInfoDto>))]
        public async Task<IActionResult> GetNinjas(int limit = 100, int offset = 0)
        {
            try
            {
                limit = Math.Min(Math.Abs(limit), 500);

                var ninjas = _mapper.Map<List<NinjaInfoDto>>(await _ninjaRepository.GetNinjas(limit, offset));

                if (ninjas == null)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                return Ok(ninjas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{ninjaId}")]
        [ProducesResponseType(200, Type = typeof(NinjaInfoDto))]
        public async Task<IActionResult> GetNinja(int ninjaId)
        {
            try
            {
                if (!_ninjaRepository.NinjaExists(ninjaId))
                    return NotFound();

                var ninja = _mapper.Map<NinjaInfoDto>(await _ninjaRepository.GetNinjaById(ninjaId));
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(ninja);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }


        }


        [HttpPost("clanId={clanId}&rankId={rankId}&villageId={villageId}")]
        [ProducesResponseType(200, Type = typeof(BaseResponsed))]
        public async Task<IActionResult> CreateNinja(int clanId,int rankId, int villageId, [FromBody] NinjaDto ninjaCreate)
        {
            try
            {
                if (ninjaCreate == null)
                    return BadRequest(ModelState);

                var ninjas = await _ninjaRepository.GetNinjas(); //?!
                var ninja = ninjas.FirstOrDefault(c => c.Name.Trim().ToLower() == ninjaCreate.Name.TrimEnd().ToLower());
                if (ninja != null)
                {
                    ModelState.AddModelError("", "Ninja alredy exists");
                    return StatusCode(422, ModelState);
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var ninjaMap = _mapper.Map<Ninja>(ninjaCreate);

                if (!_clanRepository.ClanExists(clanId) && !_rankRepository.RankExists(rankId) && !_villageRepository.VillageExists(villageId))
                    return NotFound();

                ninjaMap.Clan = await _clanRepository.GetClanById(clanId);
                ninjaMap.Rank = await _rankRepository.GetRankById(rankId);
                ninjaMap.Village = await _villageRepository.GetVillageById(villageId);

                if (!_ninjaRepository.Insert(ninjaMap))
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



        [HttpPut("{ninjaId}")]
        [ProducesResponseType(200, Type = typeof(BaseResponsed))]
        public async Task<IActionResult> UpdateNinja(int ninjaId, [FromBody] NinjaUpdateDto updateNinja) 
        {
            try
            {
                if (updateNinja == null)
                    return Ok(new BaseResponsed { errorCode = 2, errorMessage = "NinjaNull", errorName = "Error" });
                if (ninjaId != updateNinja.Id)
                    return Ok(new BaseResponsed { errorCode = 1, errorMessage = "CompareNinjaID", errorName = "Error" });
                if (!_ninjaRepository.NinjaExists(ninjaId))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var ninjaMap = _mapper.Map<Ninja>(updateNinja);
                ninjaMap.Clan = await _clanRepository.GetClanById(updateNinja.ClanId);
                ninjaMap.Rank = await _rankRepository.GetRankById(updateNinja.RankId);
                ninjaMap.Village = await _villageRepository.GetVillageById(updateNinja.VillageId);
                if (!_ninjaRepository.Update(ninjaMap))
                {
                    return Ok(new BaseResponsed { errorCode = -1, errorMessage = "Something went wrong updating category", errorName = "Error" });
                }

                return Ok(new BaseResponsed { errorCode = 0, errorName = "noError" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponsed { errorCode = -1, errorMessage = ex.Message, errorName = "CatchError" });
            }

        }


        [HttpPatch("{ninjaId}")]
        [ProducesResponseType(200, Type = typeof(BaseResponsed))]
        public IActionResult PatchNinja(int ninjaId, [FromBody] NinjaDto patchNinja)
        {
            try
            {
                if (patchNinja == null)
                    return Ok(new BaseResponsed { errorCode = 2, errorMessage = "NinjaNull", errorName = "Error" });
                if (ninjaId != patchNinja.Id)
                    return Ok(new BaseResponsed { errorCode = 1, errorMessage = "CompareNinjaID", errorName = "Error" });
                if (!_ninjaRepository.NinjaExists(ninjaId))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var ninjaMap = _mapper.Map<Ninja>(patchNinja);

                if (!_ninjaRepository.Update(ninjaMap))
                {
                    return Ok(new BaseResponsed { errorCode = -1, errorMessage = "Something went wrong updating category", errorName = "Error" });
                }

                return Ok(new BaseResponsed { errorCode = 0, errorName = "noError" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponsed { errorCode = -1, errorMessage = ex.Message, errorName = "CatchError" });
            }

        }


        [HttpDelete("{ninjaId}")]
        /*[ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]*/
        public async Task<IActionResult> DeleteClan(int ninjaId)
        {
            if (!_ninjaRepository.NinjaExists(ninjaId))
                return NotFound();

            var ninjaToDelete = await _ninjaRepository.GetNinjaById(ninjaId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_ninjaRepository.Delete(ninjaToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();

        }

    }
}
