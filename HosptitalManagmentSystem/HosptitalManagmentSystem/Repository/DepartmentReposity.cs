using HosptitalManagmentSystem.Interface;
using HosptitalManagmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HosptitalManagmentSystem.Repository
{
	public class DepartmentReposity:IDepartmentRepository
	{
		HospitalContext _context { get; set; }

		public DepartmentReposity(HospitalContext context)
		{
			_context = context;
		}
		public async Task AddDepartMent(Department department)
		{
			try
			{
				await _context.Departments.AddAsync(department);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			
		}
		public List<Department> GetAllDepartment()
		{
			var departments = _context.Departments.ToList();
			return departments;	
		}
		public Department GetDepartment(Guid id)
		{
			var department = _context.Departments.SingleOrDefault(x => x.DepartmentId == id);
			return department;
		}
		//public async Task<List<Department>> GetAlldepartment()
		//{
		//	return await _context.Departments.ToListAsync();
		//}
	}
}
