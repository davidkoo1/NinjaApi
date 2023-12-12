using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NinjaWikiAPI.Dto;
using NinjaWikiAPI.Interface;
using NinjaWikiAPI.Models;
using NinjaWikiAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace NinjaWikiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NinjaController : Controller
    {
        private readonly INinjaRepository _ninjaRepository;
        private readonly IClanRepository _clanRepository;
        private readonly IMapper _mapper;

        public NinjaController(INinjaRepository ninjaRepository, IMapper mapper, IClanRepository clanRepository)
        {
            _ninjaRepository = ninjaRepository;
            _mapper = mapper;
            _clanRepository = clanRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<NinjaDto>))]
        public async Task<IActionResult> GetNinjas()
        {
            try
            {

                var ninjas = _mapper.Map<List<NinjaDto>>(await _ninjaRepository.GetNinjas());

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
        [ProducesResponseType(200, Type = typeof(NinjaDto))]
        public async Task<IActionResult> GetNinja(int ninjaId)
        {
            try
            {
                if (!_ninjaRepository.NinjaExists(ninjaId))
                    return NotFound();

                var ninja = _mapper.Map<NinjaDto>(await _ninjaRepository.GetNinjaById(ninjaId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(ninja);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }


        }


        [HttpPost("{clanId}")]
        [ProducesResponseType(200, Type = typeof(BaseResponsed))]
        public async Task<IActionResult> CreateNinja(int clanId, [FromBody] NinjaDto ninjaCreate)
        {
            try
            {
                if (ninjaCreate == null)
                    return BadRequest(ModelState);

                var ninjas = await _ninjaRepository.GetNinjas();
                var ninja = ninjas.FirstOrDefault(c => c.Name.Trim().ToLower() == ninjaCreate.Name.TrimEnd().ToLower());
                if (ninja != null)
                {
                    ModelState.AddModelError("", "Ninja alredy exists");
                    return StatusCode(422, ModelState);
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var ninjaMap = _mapper.Map<Ninja>(ninjaCreate);

                if (!_clanRepository.ClanExists(clanId))
                    return NotFound();

                ninjaMap.Clan = await _clanRepository.GetClanById(clanId);

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
        public IActionResult UpdateNinja(int ninjaId, [FromBody] NinjaDto updateNinja)
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
