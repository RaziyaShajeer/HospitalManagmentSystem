using HosptitalManagmentSystem.Models;

namespace HosptitalManagmentSystem.Interface
{
	public interface IDepartmentRepository
	{
		public List<Department> GetAllDepartment();
		public Task AddDepartMent(Department department);
		public Department GetDepartment(Guid id);
		//public Task<List<Department>> GetAlldepartment();
	}
}
