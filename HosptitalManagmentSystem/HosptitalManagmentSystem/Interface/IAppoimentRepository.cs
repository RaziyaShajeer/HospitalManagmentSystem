using HosptitalManagmentSystem.DTOs;
using HosptitalManagmentSystem.Models;

namespace HosptitalManagmentSystem.Interface
{
	public interface IAppoimentRepository
	{
		public	Task MakeAppoiment(Appointment appoiment);
		public Task<Appointment> GetAppoinmentById(Guid Id);
		public Task<List<Appointment>> GetAppoinmentList();
		public Task ChangeStatusToAppointed(Guid id);
	}
}
