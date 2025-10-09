using Microsoft.AspNetCore.Mvc.Rendering;

namespace HosptitalManagmentSystem.DTOs
{
	public class EditAppoinmentDTO
	{
		public Guid AppointmentId { get; set; }
		public int PatientID { get; set; }
		public DateTime AppointmentDate { get; set; }
		public int TokenNumber { get; set; } // e.g., 1,2,3,... until limit
		public string PatientName { get; set; }
		public string CustomId { get; set; }
		public string? Location { get; set; }
		public string Phone { get; set; }
		public Guid DoctorId { get; set; }
		public Guid DepartmentId { get; set; } // <-- for dropdown binding
		public IEnumerable<SelectListItem> Doctors { get; set; }

	}
}
