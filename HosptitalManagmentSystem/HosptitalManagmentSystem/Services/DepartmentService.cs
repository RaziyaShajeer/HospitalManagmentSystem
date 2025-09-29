using AutoMapper;
using HosptitalManagmentSystem.DTOs;
using HosptitalManagmentSystem.Interface;
using HosptitalManagmentSystem.Models;
using System.Collections.Generic;

namespace HosptitalManagmentSystem.Services
{
	public class DepartmentService:IDepartmentService
	{
		IMapper _mapper;
		IDepartmentRepository _departmentRepository;
		public DepartmentService(IMapper mapper,IDepartmentRepository departmentRepository)
		{
			_mapper = mapper;
			_departmentRepository = departmentRepository;
		}
		public async Task AddDepartMent(Department departmentDTO)
		{
			var department=_mapper.Map<Department>(departmentDTO);
			await _departmentRepository.AddDepartMent(department);
			

		}
		public List<Department> GetAllDepartment()
		{
		var department=_departmentRepository.GetAllDepartment();
			//var listDepartmentDTO=_mapper.Map< List < DepartmentLIstDTO>>(department);
			return department;
		}
		public Department GetDepartment(Guid id)
		{
			return _departmentRepository.GetDepartment(id);

		}
		
	}
}
