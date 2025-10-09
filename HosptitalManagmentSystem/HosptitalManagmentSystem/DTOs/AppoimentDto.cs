using System.ComponentModel.DataAnnotations;

namespace HosptitalManagmentSystem.DTOs
{
	public class AppoimentDto
	{
		public Guid AppointmentId { get; set; }
		public int PatientId { get; set; }
		public string CustomId { get; set; }
		[Required(ErrorMessage = "Booking date is required")]
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }
		//public List<DoctorListforApoiment> doctors { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public Guid SelectedDoctorId { get; set; }
		public string? Location { get; set; }

		public Guid DepartmentId { get; set; } // <-- for dropdown binding
	}
}
