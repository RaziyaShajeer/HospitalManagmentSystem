namespace HosptitalManagmentSystem.DTOs
{
	public class DepartmentDTO
	{
		
		public string Name { get; set; }         // Example: "Cardiology", "Neurology"

		// Optional: If you want to track description
		public string? Description { get; set; }
		public IFormFile DepartmentImage { get; set; }
		public string DepartmentImagePath { get; set; }

		// Optional: If linking with Doctor

	}
}
