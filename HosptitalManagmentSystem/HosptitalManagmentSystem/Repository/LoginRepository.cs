using HosptitalManagmentSystem.Interface;
using HosptitalManagmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HosptitalManagmentSystem.Repository
{
	public class LoginRepository:ILoginRepository
	{
		HospitalContext _context;
		public LoginRepository(HospitalContext context)
		{
			_context = context;
		}
		public async Task<User> Login(string Email, string Password)
		{
			var user=await _context.Users.Where(e=>e.Email==e.Email &&e.Password==Password).FirstOrDefaultAsync();
			if (user != null)
			{
				return user;
			}
			else
			{
				return user;	
			}

		}
	}
}
