using HealthCareManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly HealthCareContext _context;

        public AttendanceController(HealthCareContext context)
        {
            _context = context;
        }

           // POST: api/Attendance
            [HttpPost]
            public async Task<IActionResult> MarkAttendance([FromBody] Attendance attendance)
            {
                if (attendance == null || attendance.StaffId <= 0 || attendance.Date == default)
                {
                    return BadRequest(new { message = "StaffId and Date are required." });
                }
    
                _context.Attendances.Add(attendance);
                await _context.SaveChangesAsync();
    
                return CreatedAtAction(nameof(GetAttendanceById), new { id = attendance.AttendanceId }, attendance);
            }
    
            // Optional: GET by id for CreatedAtAction
            [HttpGet("{id}")]
            public async Task<IActionResult> GetAttendanceById(int id)
            {
                var attendance = await _context.Attendances.FindAsync(id);
                if (attendance == null)
                {
                    return NotFound(new { message = "Attendance not found." });
                }
                return Ok(attendance);
        }
    }
}
