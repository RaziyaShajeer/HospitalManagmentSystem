using HosptitalManagmentSystem.Enums;
using HosptitalManagmentSystem.Interface;
using HosptitalManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace HosptitalManagmentSystem.Controllers
{
	public class LoginController : Controller
	{
		IloginService _loginService;
		public LoginController(IloginService loginService)
		{
			_loginService = loginService;
		}
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
		public async Task<IActionResult> Login(string email, string password)
		{
			var result=await _loginService.Login(email, password);
			if(result!=null)
			{
				RedirectToAction("Index");
				HttpContext.Session.SetString("UserId", result.Id.ToString());
				HttpContext.Session.SetString("UserRole", result.Role.ToString());
				if (result.Role == Role.Admin)
				{
					return RedirectToAction("Index", "Home");
				}
				else if (result.Role == Role.Doctor)
				{
					return RedirectToAction("getAppinmentsofDoctor", "Doctor");

				}
				else
				{
					TempData["MESSAGE"] = "Not Permission";
					return View();
				}


				
			}
			else
			{
				TempData["MESSAGE"] = "Invalid Login";
				return View();
			}
		
		}
	}
}
