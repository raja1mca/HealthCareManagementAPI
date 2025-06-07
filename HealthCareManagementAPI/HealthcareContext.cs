using HealthCareManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

    public class HealthCareContext : DbContext
    {
        public HealthCareContext(DbContextOptions<HealthCareContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<ShiftAssign> ShiftAssigns { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasKey(a => a.AdminId);
            modelBuilder.Entity<Staff>().HasKey(s => s.StaffId);
            modelBuilder.Entity<ShiftAssign>().HasKey(sa => sa.ShiftAssignId);
            modelBuilder.Entity<Attendance>().HasKey(a => a.AttendanceId);

            modelBuilder.Entity<ShiftAssign>()
                .HasOne<Staff>()
                .WithMany()
                .HasForeignKey(sa => sa.StaffId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Attendance>()
                .HasOne<Staff>()
                .WithMany()
                .HasForeignKey(a => a.StaffId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
