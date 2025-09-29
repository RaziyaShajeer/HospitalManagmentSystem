using System.ComponentModel.DataAnnotations;

namespace HosptitalManagmentSystem.Models
{
	public class Patient
	{
		public int Id { get; set; }
		public string CustomId { get; set; }
		[Required] 
		public string? Name { get; set; }
		public string? Gender { get; set; }
		public DateTime? DOB { get; set; }
		public string? Phone { get; set; }
		public string? Email { get; set; }
		public string? Location { get; set; }
		public string? Address { get; set; }
	}
}
