using HosptitalManagmentSystem.DTOs;
using HosptitalManagmentSystem.Models;
using System.Runtime.CompilerServices;

namespace HosptitalManagmentSystem.Interface
{
	public interface IDoctorService
	{
		public  Task<Doctor> AddDoctor(Doctor Doctordto);
		public Task<Doctor> GetDoctorById(Guid id);
		public Task<List<DoctorListDto>> GetDoctors();
		public Task<List<Doctor>> GetDoctorsByDepartment(Guid departmentId);
	
	}
}
