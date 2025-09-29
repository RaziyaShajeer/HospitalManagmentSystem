namespace HosptitalManagmentSystem.DTOs
{
	public class DoctorListDto
	{
		public Guid Id { get; set; }
		public string? DoctorName { get; set; }
		public string? Specialty { get; set; }
		public string Qualification { get; set; }
		public string? Phone { get; set; }
		public string? Email { get; set; }
		public string Name { get; set; }
		public string? DepartmentImagePath { get; set; }
		public string DoctorTiming { get; set; }
		
		public int TokenLimit { get; set; }
	}

}
