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
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/UserController>/5
        [HttpGet]
        public async Task<ActionResult<GetUserResult>> Get(string email, string password)
        {
            var checkEmail = await _context.MsUser
                .Where(x => x.Email == email)
                .Select(x => new GetUserResult()
                {
                    Email = x.Email,
                    Password = x.Password
                })
                .FirstOrDefaultAsync();

            if (checkEmail == null)
            {
                return NotFound(new ErrorApiResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Message = "Email not found!"
                });
            }

            var checkPassword = await _context.MsUser
                .Where(x => x.Email == checkEmail.Email)
                .Where(x => x.Password == password)
                .Select(x => new GetUserResult()
                {
                    UserId = x.UserId,
                    Name = x.Name,
                })
                .FirstOrDefaultAsync();

            if (checkPassword == null)
            {
                return NotFound(new ErrorApiResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    RequestMethod = HttpContext.Request.Method,
                    Message = "Invalid password!"
                });
            }

            var response = new SuccessApiResponse<GetUserResult>()
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Payload = checkPassword
            };

            return Ok(response);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserRequest createUserRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkUser = _context.MsUser
                .Where(x => x.Email == createUserRequest.Email).Count();

            if (checkUser > 0)
            {
                return BadRequest(new ErrorApiResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    RequestMethod = HttpContext.Request.Method,
                    Message = "User email already exists!"
                });
            }

            var userCreate = new User
            {
                Name = createUserRequest.Name,
                Email = createUserRequest.Email,
                Password = createUserRequest.Password,
            };

            _context.MsUser.Add(userCreate);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
