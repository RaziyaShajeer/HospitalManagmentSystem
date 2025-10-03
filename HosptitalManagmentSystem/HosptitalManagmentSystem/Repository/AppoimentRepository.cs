using HosptitalManagmentSystem.DTOs;
using HosptitalManagmentSystem.Enums;
using HosptitalManagmentSystem.Interface;
using HosptitalManagmentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HosptitalManagmentSystem.Repository
{
	public class AppoimentRepository : IAppoimentRepository
	{
		HospitalContext _context;
		public AppoimentRepository(HospitalContext context)
		{
			_context = context;
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

			return await _context.Appointments.Where(e => e.status == AppoinmentStatus.Pending).Include(e => e.Doctor).ThenInclude(e => e.Department).Include(e => e.Patient).ToListAsync();
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
	}
}

