using HosptitalManagmentSystem.Models;

namespace HosptitalManagmentSystem.Interface
{
	public interface IDoctorRepository
	{
		public Task<Doctor> GetDoctorById(Guid id);
		public Task<Doctor> AdDoctor(Doctor doctor);
		public Task<List<Doctor>> GetDoctorsByDepartment(Guid departmentId);
		public  Task<List<Doctor>> GetDoctors();	
	}
}
