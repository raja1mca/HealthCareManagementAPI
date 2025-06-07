namespace HealthCareManagementAPI.Models
{
    public class ShiftAssign
    {
        public int ShiftAssignId { get; set; }
        public string ShiftTime { get; set; } // e.g., "Morning", "Evening", "Night"
        public int StaffId { get; set; }
        public int ShiftId { get; set; }
        public DateTime Date { get; set; }
    }
}
