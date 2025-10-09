using HosptitalManagmentSystem.Models;

namespace HosptitalManagmentSystem.Interface
{
	public interface IloginService
	{
		public Task<User> Login(string Email, string Password);
	}
}
