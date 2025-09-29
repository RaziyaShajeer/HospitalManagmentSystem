namespace HosptitalManagmentSystem.DTOs
{
	public class AppoinmentViewDTo
	{
		public Guid AppointmentId { get; set; }
		public string Name { get; set; }	
		public string DoctorName { get; set; }
		public int TokenNumber { get; set; }
		public DateTime AppointmentDate { get; set; }

		public string DepartmentName { get; set; }
		public string Location { get; set; }
		public string phone { get; set; }
	}
}
