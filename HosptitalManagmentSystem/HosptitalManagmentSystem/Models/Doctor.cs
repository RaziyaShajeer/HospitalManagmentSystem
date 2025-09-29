using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HosptitalManagmentSystem.Models
{
	public class Doctor
	{


		public Guid Id { get; set; } =Guid.NewGuid();

		[Required] 
		public string? DoctorName { get; set; }
		public string? Specialty { get; set; }
		public string? Phone { get; set; }
		public string Qualification { get; set; }
		public string? Email { get; set; }
		[ForeignKey("Department")]
		public Guid? DepartmentID { get; set; }
	  public string DoctorTiming { get; set; }
		
		public int TokenLimit { get; set; }	
		public virtual Department Department { get; set; }
		public string? DepartmentImagePath { get; set; }
		// Keeps track of appointments (date-wise)
		public List<Appointment> Appointments { get; set; } = new List<Appointment>();


	}
}
