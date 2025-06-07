using HealthCareManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HealthCareManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly HealthCareContext _context;

        public StaffController(HealthCareContext context)
        {
            _context = context;
        }

        // POST: api/Staff
        [HttpPost]
        public async Task<IActionResult> AddStaff([FromBody] Staff staff)
        {
            if (staff == null || string.IsNullOrWhiteSpace(staff.Name) || string.IsNullOrWhiteSpace(staff.Email) || string.IsNullOrWhiteSpace(staff.Role))
            {
                return BadRequest(new { message = "Name, Email, and Role are required." });
            }

            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStaffById), new { id = staff.StaffId }, staff);
        }

        // PUT: api/Staff/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int id, [FromBody] Staff staff)
        {
            if (id != staff.StaffId)
            {
                return BadRequest(new { message = "Staff ID mismatch." });
            }

            var existingStaff = await _context.Staffs.FindAsync(id);
            if (existingStaff == null)
            {
                return NotFound(new { message = "Staff not found." });
            }

            existingStaff.Name = staff.Name;
            existingStaff.Email = staff.Email;
            existingStaff.Role = staff.Role;

            _context.Staffs.Update(existingStaff);
            await _context.SaveChangesAsync();

            return Ok(existingStaff);
        }

        // Optional: GET by id for CreatedAtAction
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffById(int id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }

        // GET: api/Staff/stafflist
        [HttpGet("stafflist")]
        public async Task<ActionResult<IEnumerable<Staff>>> GetStaffList()
        {
            var staffList = await _context.Staffs.ToListAsync();
            return Ok(staffList);
        }
    }
}