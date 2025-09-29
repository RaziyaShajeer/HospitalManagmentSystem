using AutoMapper;
using HosptitalManagmentSystem.DTOs;
using HosptitalManagmentSystem.Interface;
using HosptitalManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HosptitalManagmentSystem.Controllers
{
	public class DoctorController : Controller
	{
		private readonly IDepartmentService _departmentService;
		IMapper _mapper;
		IDoctorService _doctorService;
		private readonly IWebHostEnvironment _env;
		public DoctorController(IWebHostEnvironment env, IDepartmentService departmentService, IMapper mapper, IDoctorService doctorService)
		{
			_departmentService = departmentService;
			_mapper = mapper;
			_doctorService = doctorService;
			_env = env;

		}

		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult AddDoctors()
		{
			var departments = _departmentService.GetAllDepartment();
			var departmentLIstDto = _mapper.Map<List<DepartmentLIstDTO>>(departments);
			ViewBag.Departments = new SelectList(departmentLIstDto, "DepartmentId", "Name");
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> ViewAllDoctors()
		{
			var doctor_list = await _doctorService.GetDoctors();
			var groupedDoctors = doctor_list.GroupBy(x => x.Name).Select(x => new DoctorGroupDto
			{
				DepartmentName = x.Key,
				Doctors = x.ToList()
			}).ToList();
			return View(groupedDoctors);	

		}

		[HttpPost]
		public async Task<IActionResult> AddDoctors(
	DoctorDTO doctordto,
	string FromTimeHour,
	string FromTimeMinute,
	string FromTimeAMPM,
	string ToTimeHour,
	string ToTimeMinute,
	string ToTimeAMPM)
		{
			if (!ModelState.IsValid)
			{
				return View(doctordto);
			}

	

	string fromTime = $"{FromTimeHour}:{FromTimeMinute.PadLeft(2, '0')} {FromTimeAMPM}";
			string toTime = $"{ToTimeHour}:{ToTimeMinute.PadLeft(2, '0')} {ToTimeAMPM}";
		 string DoctorTiming = $"{fromTime} - {toTime}";

			// Save Image

			var fileName = $"{Guid.NewGuid()}{Path.GetExtension(doctordto.DoctorImage.FileName)}";
				var imagesPath = Path.Combine(_env.WebRootPath, "uploads");

				if (!Directory.Exists(imagesPath))
					Directory.CreateDirectory(imagesPath);

				var filePath = Path.Combine(imagesPath, fileName);
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await doctordto.DoctorImage.CopyToAsync(stream);
				}




				var doctor = _mapper.Map<Doctor>(doctordto);
			doctor.DoctorTiming = DoctorTiming;
				doctor.DepartmentImagePath = $"uploads/{fileName}";
				await _doctorService.AddDoctor(doctor);
			
			return RedirectToAction("ViewDoctor", new { id = doctor.Id });
		}

		[HttpGet]
		public async Task<IActionResult> ViewDoctor(Guid id)
		{

			var doctor=await _doctorService.GetDoctorById(id);
			
				
				return View(doctor);

			


		}

	}
}
