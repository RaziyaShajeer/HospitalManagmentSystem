using HosptitalManagmentSystem.DTOs;
using HosptitalManagmentSystem.Interface;
using HosptitalManagmentSystem.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;

namespace HosptitalManagmentSystem.Repository
{
	public class PatientRepository : IPatientRepository
	{
		public readonly HospitalContext _context;

		public PatientRepository(HospitalContext context)
		{
			_context = context;
		}
		public async Task<Patient> RegisterPatients(Patient patient)
		{
			try
			{
				var exist = await _context.Patients.FirstOrDefaultAsync(x => x.Phone == patient.Phone);
				if (exist != null)
				{
					return exist;
				}
				else
				{

					var maxId = await _context.Patients
		 .Where(p => p.CustomId != null)
		 .Select(p => p.CustomId)
		 .ToListAsync();
					int nextNumber = 1;

					if (maxId.Any())
					{
						// Extract number part (after "HT")
						var numericParts = maxId
							.Select(id => int.TryParse(id.Substring(2), out var num) ? num : 0)
							.ToList();

						nextNumber = numericParts.Max() + 1;
					}
					patient.CustomId = $"HT{nextNumber:D4}"; // HT0001, HT0002, etc.

					// Save to database
					await _context.Patients.AddAsync(patient);
					await _context.SaveChangesAsync();

					return patient;
				}
			}
			catch(Exception ex)
					{
				throw ex;
				}
			}



		
		public async Task<List<Patient>> ViewAllPAtients()
		{
			var patients = await _context.Patients.ToListAsync();
			return patients;

		}
		public async  Task<Patient> GetPatientById(int id)
		{
			return await _context.Patients.FirstOrDefaultAsync(x => x.Id == id);
		}
		public async  Task<Patient> GetPatientByPhone(string phone)
		{
			var patients = await _context.Patients
		.Where(p => p.Phone.Contains(phone))
		.FirstOrDefaultAsync();
			return patients;
		}
		public async Task EditPatient(Patient patient)
		{
			var existingPatient = _context.Patients.Where(x => x.Id == patient.Id).FirstOrDefault();
			if (existingPatient != null)
			{
				existingPatient.Name = !string.IsNullOrEmpty(patient.Name)
	? patient.Name
	: existingPatient.Name;
				existingPatient.Gender= !string.IsNullOrEmpty(patient.Gender)
	? patient.Gender
	: existingPatient.Gender;
				existingPatient.Address= !string.IsNullOrEmpty(patient.Address)
	? patient.Address
	: existingPatient.Address;
			}

			_context.Patients.Update(existingPatient);
			await _context.SaveChangesAsync();

		}

	}
}
