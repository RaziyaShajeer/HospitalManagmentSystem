namespace HosptitalManagmentSystem.DTOs
{
	public class AppoinmentListViewDTO
	{
		public Guid AppointmentId { get; set; }
		public string? Name { get; set; }
		public string? DoctorName { get; set; }
		public DateTime AppointmentDate { get; set; }
		public string departmentName { get; set; }
		public string Phone { get; set; }
		public int TokenNumber { get; set; }

	}
}
