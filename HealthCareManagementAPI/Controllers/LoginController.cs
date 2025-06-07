using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCareManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly HealthCareContext _context;

        public LoginController(HealthCareContext context)
        {
            _context = context;
        }

        public class LoginRequest
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { success = false, message = "Email and password are required." });
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Email == request.Email && a.Password == request.Password);

            if (admin == null)
            {
                return Ok(new { success = false, message = "Invalid email or password." });
            }

            return Ok(new
            {
                success = true,
                //adminId = admin.AdminId,
                //name = admin.Name,
                //email = admin.Email
            });
        }
    }
}
