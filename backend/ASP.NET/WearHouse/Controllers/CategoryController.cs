using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WearHouse.Data;
using WearHouse.Models;
using WearHouse.Models.Requests;
using WearHouse.Models.Results;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WearHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCategoryResult>>> Get()
        {
            var category = await _context.MsCategory
                .OrderBy(x => x.CategoryId)
                .Select(x => new GetCategoryResult()
                {
                    CategoryId = x.CategoryId,
                    Name = x.Name,
                })
                .ToListAsync();

            var response = new SuccessApiResponse<IEnumerable<GetCategoryResult>>()
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Payload = category
            };

            return Ok(response);
        }

        // POST: api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryRequest createCategoryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkCategory = _context.MsCategory
                .Where(x => x.Name == createCategoryRequest.Name).Count();

            if (checkCategory > 0)
            {
                return BadRequest(new ErrorApiResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    RequestMethod = HttpContext.Request.Method,
                    Message = "Category name already exists!"
                });
            }

            var category = new Category
            {
                Name = createCategoryRequest.Name,
            };

            _context.MsCategory.Add(category);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // PUT: api/<CategoryController>/5
        [HttpPut]
        public async Task<IActionResult> Put(string name, [FromBody] UpdateCategoryRequest updateCategoryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.MsCategory.FirstOrDefaultAsync(x => x.Name == name);

            if (category == null)
            {
                return NotFound(new ErrorApiResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Message = "Category not found!"
                });
            }

            category.Name = updateCategoryRequest.Name;

            await _context.SaveChangesAsync(); 

            return Ok();
        }

        // DELETE
        [HttpDelete]
        public async Task<IActionResult> Delete(string name)
        {
            var category = await _context.MsCategory.FirstOrDefaultAsync(x => x.Name == name);

            if (category == null)
            {
                return NotFound(new ErrorApiResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Message = "Category not found!"
                });
            }

            _context.MsCategory.Remove(category);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
