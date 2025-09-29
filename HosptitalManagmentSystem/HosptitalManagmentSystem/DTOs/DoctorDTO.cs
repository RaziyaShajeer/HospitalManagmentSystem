using HosptitalManagmentSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HosptitalManagmentSystem.DTOs
{
	public class DoctorDTO
	{
		[Required]
		public string? DoctorName { get; set; }
		public string? Specialty { get; set; }
		public string Qualification { get; set; }
		public string? Phone { get; set; }
		public string? Email { get; set; }

		[Required(ErrorMessage = "Please select a department")]
		public Guid? DepartmentID { get; set; }
		public IFormFile DoctorImage { get; set; }
		//public string DoctorTiming { get; set; }
		
		public int TokenLimit { get; set; }
		//public string DoctorImagePath { get; set; }






	}
}
