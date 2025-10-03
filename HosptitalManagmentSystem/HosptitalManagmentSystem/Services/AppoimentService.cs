using HosptitalManagmentSystem.DTOs;
using HosptitalManagmentSystem.Interface;
using HosptitalManagmentSystem.Models;
using HosptitalManagmentSystem.Repository;
using System.Runtime.InteropServices;

namespace HosptitalManagmentSystem.Services
{
	public class AppoimentService:IAppoimentService
	{
		IAppoimentRepository _appoimentRepository;
		public AppoimentService(IAppoimentRepository appoimentRepository)
		{
			_appoimentRepository = appoimentRepository;
		}
		public async Task MakeAppoiment(Appointment appoiment)
		{
			await _appoimentRepository.MakeAppoiment(appoiment);
		}
		public async Task<Appointment> GetAppoinmentById(Guid Id)
		{
			return await _appoimentRepository.GetAppoinmentById(Id);
		}
		public async Task<List<Appointment>> GetAppoinmentList()
		{
			return await _appoimentRepository.GetAppoinmentList();
		}
		public async Task ChangeStatusToAppointed(Guid id)
		{
			 await _appoimentRepository.ChangeStatusToAppointed(id);
		}
		public async Task<bool> DeleteAppoinment(Guid id)
		{
		 return await _appoimentRepository.DeleteAppoinment(id);	
		}
	}
}
