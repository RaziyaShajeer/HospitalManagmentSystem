using HosptitalManagmentSystem.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HosptitalManagmentSystem.Models
{
	public class Appointment
	{
		public Guid AppointmentId { get; set; }

		public DateTime AppointmentDate { get; set; }
		public int TokenNumber { get; set; } // e.g., 1,2,3,... until limit
		[ForeignKey("Patient")]
		public int PatientID { get; set; }


		[ForeignKey("Doctor")]
		public Guid DoctorId { get; set; }
		public virtual Doctor Doctor { get; set; }
		public virtual Patient Patient { get; set; }
		public AppoinmentStatus status { get; set; }
	}
}
