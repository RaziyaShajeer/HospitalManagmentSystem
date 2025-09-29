using AutoMapper;
using HosptitalManagmentSystem.DTOs;
using HosptitalManagmentSystem.Interface;
using HosptitalManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HosptitalManagmentSystem.Controllers
{
	public class AppoimentController : Controller
	{
		IPatientService _patientService;
		IDoctorService _doctorService;
		IMapper _mapper;
		IDepartmentService _departmentService;
		IAppoimentService _appoimentService;
		public AppoimentController(IPatientService patientService,IDoctorService doctorService,IMapper mapper,IDepartmentService departmentService,IAppoimentService appoimentService)
		{
			_patientService = patientService;
			_doctorService = doctorService;
			_mapper = mapper;
			_departmentService = departmentService;
			_appoimentService = appoimentService;
		}
		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> MakeAppoiment(int Id)
		{

			 var patienttoapoiment = await _patientService.GetPatientById(Id);
			var doctorList = await _doctorService.GetDoctors();

			var doctorListDto=_mapper.Map<List<DoctorListforApoiment>>(doctorList);
			ViewBag.doctorListDto = new SelectList(doctorListDto, "Id", "DoctorName");
			AppoimentDto appoiment = new AppoimentDto();
			appoiment.PatientId = patienttoapoiment.Id;
			appoiment.CustomId = patienttoapoiment.CustomId;
		
			appoiment.Name = patienttoapoiment.Name;
			appoiment.Location = patienttoapoiment.Location;
			appoiment.Phone = patienttoapoiment.Phone;
			ViewBag.departments = _departmentService.GetAllDepartment();
			return View(appoiment);
		}
		[HttpPost]
		public async Task<IActionResult> MakeAppoiment(AppoimentDto appoiment)
		{
			var doctor = await _doctorService.GetDoctorById(appoiment.SelectedDoctorId);
			var patient=await _patientService.GetPatientById(appoiment.PatientId); ;
			if (doctor == null) throw new Exception("Doctor not found");
			var todaysAppointments = doctor.Appointments.Where(a => a.AppointmentDate.Date == appoiment.Date)
		.ToList();
			if (todaysAppointments.Count >= doctor.TokenLimit)
			{
				throw new Exception("No more tokens available for this date.");
			}
			int nextToken = todaysAppointments.Count + 1;

			var appointment = new Appointment
			{
			
				Doctor = doctor,
				Patient = patient,
				AppointmentDate = appoiment.Date,
				TokenNumber = nextToken
			};
			_appoimentService.MakeAppoiment(appointment);

			
			return RedirectToAction("GetDoctorViewAppoiment",appointment);
		}
		[HttpGet]
		public async Task<IActionResult> GetDoctorsByDepartment(Guid departmentId)
		{
			var doctorsList = await _doctorService.GetDoctorsByDepartment(departmentId);
			var doctors = doctorsList.Select(d => new { d.Id, d.DoctorName }).ToList();
			return Json(doctors);
		}
		[HttpGet]
		public async Task<IActionResult> GetDoctorViewAppoiment(Appointment appointment)
		{
			var apoinmentwithDeatils=await _appoimentService.GetAppoinmentById(appointment.AppointmentId);
		 var apponmentview=_mapper.Map<AppoinmentViewDTo>(apoinmentwithDeatils);
			return View(apponmentview);
		}
		[HttpGet]
		public async Task<IActionResult>  getAllAppoinments()
		{
		var apoinmentList=	await _appoimentService.GetAppoinmentList();
			var apoinmentListDto = _mapper.Map<List<AppoinmentListViewDTO>>(apoinmentList);
			return View(apoinmentListDto);
		}
		public async Task<IActionResult> Appointed(Guid id)
		{
			var appoinment=_appoimentService.GetAppoinmentById(id);



		}
	}
}
