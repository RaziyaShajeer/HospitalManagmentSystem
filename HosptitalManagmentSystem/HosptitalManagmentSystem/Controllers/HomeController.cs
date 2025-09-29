using System.Diagnostics;
using HosptitalManagmentSystem.Interface;
using HosptitalManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace HosptitalManagmentSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IDepartmentService _departmentService;
        public HomeController(ILogger<HomeController> logger,IDepartmentService departmentService)
        {
            _logger = logger;
            _departmentService = departmentService;
        }
	

		public IActionResult Index()
        
        {
			var departments = _departmentService.GetAllDepartment();
			return View(departments);
		}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
