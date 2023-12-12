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
    public class RankController : Controller
    {
        private readonly IRankRepository _rankRepository;
        private readonly IMapper _mapper;

        public RankController(IRankRepository rankRepository, IMapper mapper)
        {
            _rankRepository = rankRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RankDto>))]
        public async Task<IActionResult> GetRanks()
        {
            try
            {
                var ranks = _mapper.Map<List<RankDto>>(await _rankRepository.GetRanks());

                if (ranks == null)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                return Ok(ranks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        [HttpGet("{rankId}")]
        [ProducesResponseType(200, Type = typeof(RankDto))]
        public async Task<IActionResult> GetRank(int rankId)
        {
            try
            {
                if (!_rankRepository.RankExists(rankId))
                    return NotFound();

                var rank = _mapper.Map<RankDto>(await _rankRepository.GetRankById(rankId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(rank);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        [HttpGet("Ninja/{ninjaId}")] //{ninjaId}/{rankId?} [FromQuery]newElementId
        [ProducesResponseType(200, Type = typeof(RankDto))]
        public async Task<IActionResult> GetNinjaRank(int ninjaId)
        {
            try
            {
               /* if (!_rankRepository.RankExists(rankId))
                    return NotFound();
               */
                var rank = (await _rankRepository.GetRankByNinja(ninjaId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(rank);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BaseResponsed))]
        public async Task<IActionResult> CreateNinja([FromBody] RankDto rankCreate)
        {
            try
            {
                if (rankCreate == null)
                    return BadRequest(ModelState);

                var ranks = await _rankRepository.GetRanks();
                var rank = ranks.FirstOrDefault(r => r.Symbol == rankCreate.Symbol);
                if (rank != null)
                {
                    ModelState.AddModelError("", "Rank alredy exists");
                    return StatusCode(422, ModelState);
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var rankMap = _mapper.Map<Rank>(rankCreate);

                if (!_rankRepository.Insert(rankMap))
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


        [HttpPut("{rankId}")]
        [ProducesResponseType(200, Type = typeof(BaseResponsed))]
        public IActionResult UpdateRank(int rankId, [FromBody] RankDto updateRank)
        {
            try
            {
                if (updateRank == null)
                    return Ok(new BaseResponsed { errorCode = 2, errorMessage = "RankNull", errorName = "Error" });
                if (rankId != updateRank.Id)
                    return Ok(new BaseResponsed { errorCode = 1, errorMessage = "CompareRankID", errorName = "Error" });
                if (!_rankRepository.RankExists(rankId))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var rankMap = _mapper.Map<Rank>(updateRank);

                if (!_rankRepository.Update(rankMap))
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


        [HttpDelete("{rankId}")]
        /*[ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]*/
        public async Task<IActionResult> DeleteClan(int rankId)
        {
            if (!_rankRepository.RankExists(rankId))
                return NotFound();

            var rankToDelete = await _rankRepository.GetRankById(rankId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_rankRepository.Delete(rankToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();

        }
    }
}
