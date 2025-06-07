namespace HealthCareManagementAPI.Models
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public int StaffId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
    }
}
