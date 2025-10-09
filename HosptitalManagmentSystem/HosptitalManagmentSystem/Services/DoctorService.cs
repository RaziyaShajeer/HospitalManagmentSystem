using AutoMapper;
using HosptitalManagmentSystem.DTOs;
using HosptitalManagmentSystem.Interface;
using HosptitalManagmentSystem.Models;

namespace HosptitalManagmentSystem.Services
{
	public class DoctorService:IDoctorService
	{
		IDoctorRepository _doctorRepository;
		IMapper _mapper;
		public DoctorService(IDoctorRepository doctorRepository,IMapper mapper)
		{
			_doctorRepository = doctorRepository;
			_mapper = mapper;	
		}
		public async  Task<Doctor> AddDoctor(Doctor doctor)
		{
			try
			{
				
				return await _doctorRepository.AdDoctor(doctor);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			
		}
		public async  Task<List<Doctor>> GetDoctorsByDepartment(Guid departmentId)
		{
			return await _doctorRepository.GetDoctorsByDepartment(departmentId);
		}
		public Task<Doctor> GetDoctorById(Guid id)
		{
			return _doctorRepository.GetDoctorById(id);
		}
		public async  Task<List<DoctorListDto>> GetDoctors()
		{
			var doctors=await _doctorRepository.GetDoctors();
			var doctorList=_mapper.Map<List<DoctorListDto>>(doctors);
			return doctorList;

		}
	}
}
