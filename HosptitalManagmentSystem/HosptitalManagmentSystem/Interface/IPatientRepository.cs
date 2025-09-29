using HosptitalManagmentSystem.DTOs;
using HosptitalManagmentSystem.Models;
using HosptitalManagmentSystem.Repository;

namespace HosptitalManagmentSystem.Interface
{
	public interface IPatientRepository
	{
	public Task<Patient>  RegisterPatients(Patient patient);
		public Task<Patient> GetPatientById(int id);
		public Task<Patient> GetPatientByPhone(string phone);
		public Task EditPatient(Patient patient);
		public Task<List<Patient>> ViewAllPAtients();
	}
}
