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
    public class SkillController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISkillRepository _skillRepository;
        private readonly IRankRepository _rankRepository;
        private readonly ICategoryRepository _categoryRepository;

        public SkillController(IMapper mapper, ISkillRepository skillRepository, IRankRepository rankRepository, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _skillRepository = skillRepository;
            _rankRepository = rankRepository;
            _categoryRepository = categoryRepository;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SkillDto>))]
        public async Task<IActionResult> GetSkills()
        {
            try
            {
                var skills = _mapper.Map<List<SkillDto>>(await _skillRepository.GetSkills());

                if (skills == null)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                return Ok(skills);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{skillId}")]
        [ProducesResponseType(200, Type = typeof(SkillDto))]
        public async Task<IActionResult> GetSkill(int skillId)
        {
            try
            {
                if (!_skillRepository.SkillExists(skillId))
                    return NotFound();

                var skill = _mapper.Map<SkillDto>(await _skillRepository.GetSkillById(skillId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(skill);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpPost("rankId={rankId}&categoryId={categoryId}")]
        [ProducesResponseType(200, Type = typeof(BaseResponsed))]
        public async Task<IActionResult> CreateSkill(int rankId, int categoryId, [FromBody] SkillDto skillCreate)
        {
            try
            {
                if (skillCreate == null)
                    return BadRequest(ModelState);

                var skills = await _skillRepository.GetSkills();
                var skill = skills.FirstOrDefault(s => s.Name.Trim().ToLower() == skillCreate.Name.TrimEnd().ToLower());
                if (skill != null)
                {
                    ModelState.AddModelError("", "Skill alredy exists");
                    return StatusCode(422, ModelState);
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                
                var skillMap = _mapper.Map<Skill>(skillCreate);
                skillMap.Rank = await _rankRepository.GetRankById(rankId);
                skillMap.Category = await _categoryRepository.GetCategoryById(categoryId);
                if (!_skillRepository.Insert(skillMap))
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


        [HttpPut("{skillId}")]
        [ProducesResponseType(200, Type = typeof(BaseResponsed))]
        public async Task<IActionResult> UpdateSkill(int skillId, [FromBody] SkillUpdateDto updateSkill)
        {
            try
            {
                if (updateSkill == null)
                    return Ok(new BaseResponsed { errorCode = 2, errorMessage = "SkillNull", errorName = "Error" });
                if (skillId != updateSkill.Id)
                    return Ok(new BaseResponsed { errorCode = 1, errorMessage = "CompareSkillID", errorName = "Error" });
                if (!_skillRepository.SkillExists(skillId))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var skillMap = _mapper.Map<Skill>(updateSkill);
                skillMap.Category = await _categoryRepository.GetCategoryById(updateSkill.CategoryId);
                skillMap.Rank = await _rankRepository.GetRankById(updateSkill.RankId);

                if (!_skillRepository.Update(skillMap))
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


        [HttpDelete("{skillId}")]
        public async Task<IActionResult> DeleteSkill(int skillId)
        {
            if (!_skillRepository.SkillExists(skillId))
                return NotFound();

            var skillToDelete = await _skillRepository.GetSkillById(skillId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_skillRepository.Delete(skillToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();

        }


    }
}
