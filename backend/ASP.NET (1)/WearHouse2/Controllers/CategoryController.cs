using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WearHouse.Data;
using WearHouse.Models;
using WearHouse.Models.Requests;
using WearHouse.Models.Results;
using WearHouse2.Models.Requests;
using WearHouse2.Models.Results;

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
        public async Task<ActionResult<IEnumerable<GetCategoryResult>>> GetAll()
        {
            var category = await _context.MsCategory
                .OrderBy(x => x.CategoryId)
                .Select(x => new GetCategoryResult()
                {
                    CategoryId = x.CategoryId,
                    Name = x.Name,
                })
                .ToListAsync();

            var response = new SuccessApiPayloadResponse<IEnumerable<GetCategoryResult>>()
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Payload = category
            };

            return Ok(response);
        }

        // GET: api/<CategoryController>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<GetCategoryResult>>> GetById(int id)
        {
            var category = await _context.MsCategory
                .Where(x => x.CategoryId == id)
                .Select(x => new GetCategoryResult()
                {
                    CategoryId = x.CategoryId,
                    Name = x.Name,
                })
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound(new ErrorApiResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Message = "Category not found!"
                });
            }

            var response = new SuccessApiPayloadResponse<GetCategoryResult>()
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Message = "Successfully",
                Payload = category
            };

            return Ok(response);
        }

        // POST: api/<CategoryController>
        [HttpPost("add")]
        public async Task<IActionResult> PostForAdd([FromBody] CreateCategoryRequest createCategoryRequest)
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

            var response = new SuccessApiMessageResponse()
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Message = "Successfully added!",
            };

            return Ok(response);
        }

        // PUT: api/<CategoryController>/5
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCategoryRequest updateCategoryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.MsCategory.FirstOrDefaultAsync(x => x.CategoryId == id);

            if (category == null)
            {
                return NotFound(new ErrorApiResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Message = "Category not found!"
                });
            }

            var checkCategoryName = _context.MsCategory
                .Where(x => x.Name == updateCategoryRequest.Name).Count();

            if (checkCategoryName > 0)
            {
                return BadRequest(new ErrorApiResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    RequestMethod = HttpContext.Request.Method,
                    Message = "Category name already exists!"
                });
            }

            category.Name = updateCategoryRequest.Name;

            await _context.SaveChangesAsync();

            var response = new SuccessApiMessageResponse()
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Message = "Successfully edited!",
            };

            return Ok(response);
        }

        // DELETE
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.MsCategory.FirstOrDefaultAsync(x => x.CategoryId == id);

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

            var response = new SuccessApiMessageResponse()
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Message = "Successfully deleted!",
            };

            return Ok(response);
        }


    }
}
