using HealthCareManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HealthCareManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftController : ControllerBase
    {
        private readonly HealthCareContext _context;

        public ShiftController(HealthCareContext context)
        {
            _context = context;
        }

        public class AssignShiftRequest
        {
            public int StaffId { get; set; }
            public int ShiftId { get; set; }
            public string ShiftTime { get; set; } = string.Empty; //  "Morning", "Evening", "Night"
            public DateTime Date { get; set; }
        }

        // POST: api/Shift/assign
        [HttpPost("assign")]
        public async Task<IActionResult> AssignShift([FromBody] AssignShiftRequest request)
        {
            if (request.StaffId <= 0 || request.ShiftId <= 0 || string.IsNullOrWhiteSpace(request.ShiftTime) || request.Date == default)
            {
                return BadRequest(new { message = "StaffId, ShiftId, ShiftTime, and Date are required." });
            }

            var staffExists = await _context.Staffs.AnyAsync(s => s.StaffId == request.StaffId);
            if (!staffExists)
            {
                return NotFound(new { message = "Staff not found." });
            }

            // Create and save the shift assignment
            var shiftAssign = new ShiftAssign
            {
                StaffId = request.StaffId,
                ShiftId = request.ShiftId,
                ShiftTime = request.ShiftTime,
                Date = request.Date
            };

            _context.ShiftAssigns.Add(shiftAssign);
            await _context.SaveChangesAsync();

            return Ok(shiftAssign);
        }

        // PUT: api/Shift/assign/{id}
        [HttpPut("assign/{id}")]
        public async Task<IActionResult> UpdateShiftAssign(int id, [FromBody] AssignShiftRequest request)
        {
            if (request.StaffId <= 0 || request.ShiftId <= 0 || string.IsNullOrWhiteSpace(request.ShiftTime) || request.Date == default)
            {
                return BadRequest(new { message = "StaffId, ShiftId, ShiftTime, and Date are required." });
            }

            var shiftAssign = await _context.ShiftAssigns.FindAsync(id);
            if (shiftAssign == null)
            {
                return NotFound(new { message = "Shift assignment not found." });
            }

            var staffExists = await _context.Staffs.AnyAsync(s => s.StaffId == request.StaffId);
            if (!staffExists)
            {
                return NotFound(new { message = "Staff not found." });
            }

            shiftAssign.StaffId = request.StaffId;
            shiftAssign.ShiftId = request.ShiftId;
            shiftAssign.ShiftTime = request.ShiftTime;
            shiftAssign.Date = request.Date;

            _context.ShiftAssigns.Update(shiftAssign);
            await _context.SaveChangesAsync();

            return Ok(shiftAssign);
        }
    }
}