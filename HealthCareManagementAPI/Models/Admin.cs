﻿namespace HealthCareManagementAPI.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Consider using a secure hash for passwords

    }
}
