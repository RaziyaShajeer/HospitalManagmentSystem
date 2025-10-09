using HosptitalManagmentSystem.DTOs;
using HosptitalManagmentSystem.Enums;
using HosptitalManagmentSystem.Interface;
using HosptitalManagmentSystem.Models;
using HosptitalManagmentSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Numerics;

namespace HosptitalManagmentSystem.Repository
{
	public class AppoimentRepository : IAppoimentRepository
	{
		HospitalContext _context;
		IDoctorService _doctorService;
		public AppoimentRepository(HospitalContext context,IDoctorService doctorService)
		{
			_context = context;
			_doctorService = doctorService;
		}
		public async Task MakeAppoiment(Appointment appoimentDto)
		{
			appoimentDto.status = AppoinmentStatus.Pending;
			await _context.Appointments.AddAsync(appoimentDto);
			await _context.SaveChangesAsync();

		}
		public async Task<Appointment> GetAppoinmentById(Guid Id)
		{
			var apoinment = await _context.Appointments.Where(e => e.AppointmentId == Id).Include(e => e.Doctor).ThenInclude(e => e.Department).Include(e => e.Patient).FirstOrDefaultAsync();
			return apoinment;
		}
		public async Task<List<Appointment>> GetAppoinmentList()
		{

			return  _context.Appointments
	  .Include(a => a.Patient)
	  .Include(a => a.Doctor)
	  .AsNoTracking() // 🔑 avoids stale tracking cache
	  .ToList();

		}
		public async Task ChangeStatusToAppointed(Guid id)
		{
			var appoimentToChangeStatus = await _context.Appointments.Where(e => e.AppointmentId == id).FirstOrDefaultAsync();
			if (appoimentToChangeStatus != null)
			{
				appoimentToChangeStatus.status = AppoinmentStatus.Consulted;

				await _context.SaveChangesAsync();
			}
		}
		public async Task<bool> DeleteAppoinment(Guid id)
		{
			var appoinment = await _context.Appointments.Where(e => e.AppointmentId == id).FirstOrDefaultAsync();
			if (appoinment != null)
			{
				_context.Appointments.Remove(appoinment);
				await _context.SaveChangesAsync();
				return true;
			}
			else
			{
				return false;
			}
		}
		public async Task EditAppoinment(Appointment appointment)
		{
			var AppoinmenttoUpdate = await _context.Appointments.Where(e => e.AppointmentId == appointment.AppointmentId).FirstOrDefaultAsync();
			if(AppoinmenttoUpdate!=null)
			{
				
				AppoinmenttoUpdate.PatientID =
	(appointment.PatientID == 0 || appointment.PatientID.ToString() == "")
	? AppoinmenttoUpdate.PatientID : appointment.PatientID;
				var doctor = await _doctorService.GetDoctorById(appointment.DoctorId);

				var todaysAppointments = doctor.Appointments.Where(a => a.AppointmentDate.Date == appointment.AppointmentDate)

	.ToList();
				if (todaysAppointments.Count >= doctor.TokenLimit)
				{
					throw new Exception("No more tokens available for this date.");

				}
				int nextToken = todaysAppointments.Count + 1;
				AppoinmenttoUpdate.TokenNumber = nextToken;
				AppoinmenttoUpdate.AppointmentDate = appointment.AppointmentDate.ToString() == " " ? AppoinmenttoUpdate.AppointmentDate : appointment.AppointmentDate;
				AppoinmenttoUpdate.DoctorId = appointment.DoctorId.ToString()=="" ? AppoinmenttoUpdate.DoctorId:appointment.DoctorId;	
				AppoinmenttoUpdate.status = appointment.status.ToString()==" " ? AppoinmenttoUpdate.status:appointment.status;	
			 _context.Appointments.Update(AppoinmenttoUpdate);
				await _context.SaveChangesAsync();	

			}
			

		}
		public async Task<List<Appointment>> GetAppoinmentsofDoctor(Guid id)
		{
			var Appoinments = await _context.Appointments.Where(e => e.DoctorId == id).Include(e=>e.Patient).ToListAsync();
			
				return Appoinments;
			
		}
	}
}

