using AutoMapper;
using HosptitalManagmentSystem.DTOs;
using HosptitalManagmentSystem.Interface;
using HosptitalManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HosptitalManagmentSystem.Controllers
{
	public class AdminController : Controller
	{
		IDepartmentService _departmentService;
		private readonly IWebHostEnvironment _env;
		IMapper _mapper;
		public AdminController(IDepartmentService departmentService,IMapper mapper, IWebHostEnvironment env)
		{
			_departmentService = departmentService;	
			_mapper = mapper;
			_env = env;
		}

		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public ActionResult AddDepartment()
		
		{
			return View();


		}
		public IActionResult Details(Guid id)
		{
			var dept = _departmentService.GetDepartment(id);
			if (dept == null) return NotFound();

			return View(dept); // will render Views/Department/Details.cshtml
		}
		[HttpPost]
		public async  Task<ActionResult> AddDepartment(DepartmentDTO departmentDTO)
		{
			var fileName = $"{Guid.NewGuid()}{Path.GetExtension(departmentDTO.DepartmentImage.FileName)}";
			var imagesPath = Path.Combine(_env.WebRootPath, "uploads");
			// Ensure directory exists
			
	
			if (!Directory.Exists(imagesPath))
				Directory.CreateDirectory(imagesPath);
			// Combine full file path
			var department = _mapper.Map<Department>(departmentDTO);
			var filePath = Path.Combine(imagesPath, fileName);
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await departmentDTO.DepartmentImage.CopyToAsync(stream);
			}
			department.DepartmentImagePath = $"uploads/{fileName}";
			
		
			await _departmentService.AddDepartMent(department);
			return View();

		}
	}
}
