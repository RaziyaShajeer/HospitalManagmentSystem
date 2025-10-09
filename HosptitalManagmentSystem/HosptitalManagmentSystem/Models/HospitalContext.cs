using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;

namespace HosptitalManagmentSystem.Models
{
	public class HospitalContext:DbContext
	{


		public HospitalContext(DbContextOptions<HospitalContext> options)
			: base(options)
		{
		}
		public virtual DbSet<Patient> Patients { get; set; }
		public virtual DbSet<Department> Departments { get; set; }
		public virtual DbSet<Doctor> Doctors { get; set; }	
		public virtual DbSet<Appointment> Appointments { get; set; }
		public virtual DbSet<User>  Users { get; set; }


	}
}
