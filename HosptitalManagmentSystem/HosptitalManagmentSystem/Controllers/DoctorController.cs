using AutoMapper;
using HosptitalManagmentSystem.DTOs;
using HosptitalManagmentSystem.Interface;
using HosptitalManagmentSystem.Models;
using HosptitalManagmentSystem.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HosptitalManagmentSystem.Controllers
{
	public class DoctorController : Controller
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
			Response.Headers["Pragma"] = "no-cache";
			Response.Headers["Expires"] = "0";
			base.OnActionExecuting(context);
		}

		private readonly IDepartmentService _departmentService;
		IMapper _mapper;
		IDoctorService _doctorService;
		private readonly EmailService _emailService;
		IAppoimentService _appoinmentService;
		private readonly IWebHostEnvironment _env;
		public DoctorController(IWebHostEnvironment env, IDepartmentService departmentService, IMapper mapper, IDoctorService doctorService,IAppoimentService appoimentService,EmailService emailService)
		{
			_appoinmentService = appoimentService;
			_departmentService = departmentService;
			_mapper = mapper;
			_doctorService = doctorService;
			_env = env;
			_emailService = emailService;

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
				var addedDoctor=await _doctorService.AddDoctor(doctor);
			await _emailService.SendEmailAsync(doctor.DoctorName,doctor.Email,doctor.Password);
			
			return RedirectToAction("ViewDoctor", new { id = doctor.Id });
		}

		[HttpGet]
		public async Task<IActionResult> ViewDoctor(Guid id)
		{

			var doctor=await _doctorService.GetDoctorById(id);
			
				
				return View(doctor);

			


		}
		[HttpGet]
		public async Task<IActionResult> getAppinmentsofDoctor(DateTime? date)
		{
			var userId=HttpContext.Session.GetString("UserId");
			if (userId == null)
			{
				ViewBag.message = "Please Login";
				return RedirectToAction("Login", "Login");                                                               
			}
			Guid id = Guid.Parse(userId);
			var doctor=await _doctorService.GetDoctorById(id);
			ViewBag.DoctorName = doctor.DoctorName;
			var appoinments = await _appoinmentService.GetAppoinmentsofDoctor(id);
			if (date.HasValue)
			{


				appoinments = appoinments.Where(a => a.AppointmentDate.Date == date.Value.Date).ToList();
			}
				var appoinmentDto = _mapper.Map<List<AppoinmentListViewDTO>>(appoinments);
			if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
			{
				return PartialView("_AllAppoinmentspartial", appoinmentDto);
			}

			// Normal full page view
			return View("getAppinmentsofDoctor", appoinmentDto);


		}
		public async Task<IActionResult> Logout()

		{
			// Clear all session values
			HttpContext.Session.Clear();

			// Optionally clear authentication cookies if using Identity
			HttpContext.SignOutAsync();

			// Redirect to login page
			return RedirectToAction("Login", "Login");
		}
	}
}
