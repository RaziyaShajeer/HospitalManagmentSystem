using HosptitalManagmentSystem.Interface;
using HosptitalManagmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HosptitalManagmentSystem.Repository
{
	public class Doctorrepostory:IDoctorRepository
	{
		public readonly HospitalContext _context ;
		public Doctorrepostory(HospitalContext context)
		{
			_context = context;
		}
		public async Task<Doctor> AdDoctor(Doctor doctor)
		{
			try
			{
				doctor.Password = (doctor.DoctorName?.Replace(" ", "").Length >= 4
	? doctor.DoctorName.Replace(" ", "").Substring(0, 4)
	: doctor.DoctorName?.Replace(" ", "")) + doctor.Phone.Replace(" ", "").Substring(0, 4);
				_context.Doctors.AddAsync(doctor);
				await _context.SaveChangesAsync();
				User user = new User();
				user.Id = doctor.Id;
				user.Email = doctor.Email;
				user.Password = doctor.Password;
				user.Role = Enums.Role.Doctor;
				await _context.Users.AddAsync(user);
				await _context.SaveChangesAsync();
				return doctor;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			


		}
		public async Task<Doctor> GetDoctorById(Guid id)
		{
			try
			{
				return await _context.Doctors.Where(x => x.Id == id).Include(x => x.Department).Include(e => e.Appointments).FirstAsync();
			}
		 catch(Exception ex)
			{
				throw;
			}

		}
		public async  Task<List<Doctor>> GetDoctorsByDepartment(Guid departmentId)
		{
			var doctors =await  _context.Doctors.Where(x => x.DepartmentID == departmentId).ToListAsync();
			return doctors;
		}
		public  async Task<List<Doctor>> GetDoctors()
		{
			var doctors=await _context.Doctors.Include(x=>x.Department).ToListAsync();
			return doctors;
		}
	}
}
