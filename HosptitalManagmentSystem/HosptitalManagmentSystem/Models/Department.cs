namespace HosptitalManagmentSystem.Models
{
	public class Department
	{
		public Guid DepartmentId { get; set; }   // More descriptive than just Id
		public string Name { get; set; }         // Example: "Cardiology", "Neurology"

		// Optional: If you want to track description
		public string? Description { get; set; }
		public string? DepartmentImagePath { get; set; }
		//public List<Appointment> Appointments { get; set; } = new List<Appointment>();

		// Optional: If linking with Doctor

	}
}
