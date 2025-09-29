using Microsoft.AspNetCore.Mvc;

namespace HosptitalManagmentSystem.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult login()
		{
			return View();
		}
		[HttpPost]
		public IActionResult login(string email, string password)
		{
			
			return View();
		}
	}
}
