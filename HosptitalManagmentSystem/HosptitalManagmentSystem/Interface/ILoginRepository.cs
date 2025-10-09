using HosptitalManagmentSystem.Models;

namespace HosptitalManagmentSystem.Interface
{
	public interface ILoginRepository
	{
		public Task<User> Login(string Email, string Password);
	}
}
