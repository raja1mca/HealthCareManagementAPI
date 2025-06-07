namespace HealthCareManagementAPI.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // e.g., Nurse, Technician, etc.
    }
}
