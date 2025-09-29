using HosptitalManagmentSystem.DTOs;
using HosptitalManagmentSystem.Models;

namespace HosptitalManagmentSystem.Interface
{
	public interface IAppoimentService
	{
		public Task MakeAppoiment(Appointment appoimentDto);
		public Task<Appointment> GetAppoinmentById(Guid Id);
		public Task<List<Appointment>> GetAppoinmentList();
	}
}
