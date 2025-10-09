using HosptitalManagmentSystem.Interface;
using HosptitalManagmentSystem.Models;

namespace HosptitalManagmentSystem.Services
{
	public class LoginService:IloginService
	{
		ILoginRepository loginRepository;
		public LoginService(ILoginRepository _loginRepostory)
		{
			loginRepository = _loginRepostory;
		}
		public async  Task<User> Login(string Email, string Password)
		{
			return await loginRepository.Login(Email, Password);	
		}
	}
}
