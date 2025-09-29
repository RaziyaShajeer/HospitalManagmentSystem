using HosptitalManagmentSystem.DTOs;
using HosptitalManagmentSystem.Models;

namespace HosptitalManagmentSystem.Interface
{
	public interface IPatientService
	{
		public Task<Patient> RegisterPatients(PatientDTO patientDTO);
		public Task<Patient> GetPatientById(int id);
		public Task<Patient> GetPatientByPhone(string phone);
		public Task EditPatient(Patient patient);
		public Task<List<Patient>> ViewAllPAtients();
	}
}
