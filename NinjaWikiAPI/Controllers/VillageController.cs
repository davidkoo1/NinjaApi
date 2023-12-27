using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NinjaWikiAPI.Dto;
using NinjaWikiAPI.Interface;
using NinjaWikiAPI.Models;
using NinjaWikiAPI.Repository;

namespace NinjaWikiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillageController : Controller
    {
        private readonly IVillageRepository _villageRepository;
        private readonly IMapper _mapper;

        public VillageController(IVillageRepository villageRepository, IMapper mapper)
        {
            _villageRepository = villageRepository;
            _mapper = mapper;
        }

        [HttpGet("GetVillages")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<VillageDto>))]
        public async Task<IActionResult> GetVillages()
        {
            try
            {
                var villages = _mapper.Map<List<VillageDto>>(await _villageRepository.GetVillages());

                if (villages == null)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                return Ok(villages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{villageId}")]
        [ProducesResponseType(200, Type = typeof(VillageDto))]
        public async Task<IActionResult> GetVillage(int villageId)
        {
            try
            {
                if (!_villageRepository.VillageExists(villageId))
                    return NotFound();

                var village = _mapper.Map<VillageDto>(await _villageRepository.GetVillageById(villageId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(village);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("GetVillageByNinjaId/{ninjaId}")] //{ninjaId}/{rankId?} [FromQuery]newElementId
        [ProducesResponseType(200, Type = typeof(VillageDto))]
        public async Task<IActionResult> GetNinjaVillage(int ninjaId)
        {
            try
            {
                /*NInja
                 if (!_villageRepository.VillageExists(rankId))
                     return NotFound();
                */
                var village = _mapper.Map<VillageDto>(await _villageRepository.GetVillageByNinja(ninjaId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(village);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BaseResponsed))]
        public async Task<IActionResult> CreateVillage([FromBody] VillageDto villageCreate)
        {
            try
            {
                if (villageCreate == null)
                    return BadRequest(ModelState);

                var villages = await _villageRepository.GetVillages();
                var village = villages.FirstOrDefault(r => r.Name == villageCreate.Name);
                if (village != null)
                {
                    ModelState.AddModelError("", "Village alredy exists");
                    return StatusCode(422, ModelState);
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var villageMap = _mapper.Map<Village>(villageCreate);

                if (!_villageRepository.Insert(villageMap))
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


        [HttpPut("{villageId}")]
        [ProducesResponseType(200, Type = typeof(BaseResponsed))]
        public IActionResult UpdateVillage(int villageId, [FromBody] VillageDto updateVillage)
        {
            try
            {
                if (updateVillage == null)
                    return Ok(new BaseResponsed { errorCode = 2, errorMessage = "VillageNull", errorName = "Error" });
                if (villageId != updateVillage.Id)
                    return Ok(new BaseResponsed { errorCode = 1, errorMessage = "CompareVillageID", errorName = "Error" });
                if (!_villageRepository.VillageExists(villageId))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var villageMap = _mapper.Map<Village>(updateVillage);

                if (!_villageRepository.Update(villageMap))
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



        [HttpDelete("{villageId}")]
        /*[ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]*/
        public async Task<IActionResult> DeleteVillage(int villageId)
        {
            if (!_villageRepository.VillageExists(villageId))
                return NotFound();

            var villageToDelete = await _villageRepository.GetVillageById(villageId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_villageRepository.Delete(villageToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();

        }

    }
}
