using AutoMapper;
using HosptitalManagmentSystem.DTOs;
using HosptitalManagmentSystem.Interface;
using HosptitalManagmentSystem.Models;

namespace HosptitalManagmentSystem.Services
{
	public class PatientService:IPatientService
	{
		private IPatientRepository _patientRepository;
		private IMapper _mapper;
		public PatientService(IPatientRepository patientRepository,IMapper mapper)
		{
			_patientRepository = patientRepository;
			_mapper = mapper;
		}
		public async  Task<Patient> RegisterPatients(PatientDTO patientDTO)
		{
			var patient = _mapper.Map < Patient > (patientDTO);
		var registerdpatient=await	_patientRepository.RegisterPatients(patient);
			return registerdpatient;

		
		}
		public async Task<Patient> GetPatientById(int id)
		{
			return await _patientRepository.GetPatientById(id);
		}
		public async Task<Patient> GetPatientByPhone(string phone)
		{
			return await _patientRepository.GetPatientByPhone(phone);
		}
		public async Task EditPatient(Patient patient)
		{
			await _patientRepository.EditPatient(patient);
		}
		public async Task<List<Patient>> ViewAllPAtients()
		{
			return await _patientRepository.ViewAllPAtients();
		}
	}
}
