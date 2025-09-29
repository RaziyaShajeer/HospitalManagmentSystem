using HosptitalManagmentSystem.DTOs;
using HosptitalManagmentSystem.Models;

namespace HosptitalManagmentSystem.Interface
{
	public interface IDepartmentService
	{
		public Task AddDepartMent(Department departmentDTO);
		public List<Department> GetAllDepartment();
		public Department GetDepartment(Guid id);
	//	public Task<List<Department>> GetAlldepartment();
	}
}
