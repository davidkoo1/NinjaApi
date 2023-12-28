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
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public async Task<IActionResult> Getcategories()
        {
            try
            {
                var categories = _mapper.Map<List<CategoryDto>>(await _categoryRepository.GetCategories());

                if (categories == null)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            try
            {
                if (!_categoryRepository.CategoryExists(categoryId))
                    return NotFound();

                var category = _mapper.Map<CategoryDto>(await _categoryRepository.GetCategoryById(categoryId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("GetSkillsBy/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SkillUpdateDto>))]
        public async Task<IActionResult> GetSkills(int categoryId)
        {
            try
            {
                if (!_categoryRepository.CategoryExists(categoryId))
                    return NotFound();

                var skills = _mapper.Map<List<SkillUpdateDto>>(await _categoryRepository.GetSkillsByCategory(categoryId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(skills);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("GetCategoryBy/{skillId}")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        public async Task<IActionResult> GetCategoryBySkill(int skillId)
        {
            try
            {

                var category = _mapper.Map<CategoryDto>(await _categoryRepository.GetCategoryBySkill(skillId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BaseResponsed))]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            try
            {
                if (categoryCreate == null)
                    return BadRequest(ModelState);

                var categories = await _categoryRepository.GetCategories();
                var category = categories.FirstOrDefault(c => c.Name.Trim().ToLower() == categoryCreate.Name.TrimEnd().ToLower());
                if (category != null)
                {
                    ModelState.AddModelError("", "Category alredy exists");
                    return StatusCode(422, ModelState);
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var categoryMap = _mapper.Map<Category>(categoryCreate);

                if (!_categoryRepository.Insert(categoryMap))
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



        [HttpPut("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(BaseResponsed))]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto updateCategory)
        {
            try
            {
                if (updateCategory == null)
                    return Ok(new BaseResponsed { errorCode = 2, errorMessage = "CategoryNull", errorName = "Error" });
                if (categoryId != updateCategory.Id)
                    return Ok(new BaseResponsed { errorCode = 1, errorMessage = "CompareCategoryID", errorName = "Error" });
                if (!_categoryRepository.CategoryExists(categoryId))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var categoryMap = _mapper.Map<Category>(updateCategory);

                if (!_categoryRepository.Update(categoryMap))
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


        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var categoryToDelete = await _categoryRepository.GetCategoryById(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.Delete(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();

        }

    }
}
