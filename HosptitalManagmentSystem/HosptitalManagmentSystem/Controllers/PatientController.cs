using AutoMapper;
using HosptitalManagmentSystem.DTOs;
using HosptitalManagmentSystem.Interface;
using HosptitalManagmentSystem.Models;
using HosptitalManagmentSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HosptitalManagmentSystem.Controllers
{
	public class PatientController: Controller
	{
		IPatientService _patientService;
		IMapper _mapper;

		public PatientController(IPatientService patientService,IMapper mapper)
		{
			_patientService = patientService;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<IActionResult> AddPatient()
		{

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> AddPatient(PatientDTO patient)
		{
			try
			{
				await _patientService.RegisterPatients(patient);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			


			return RedirectToAction("PatientRegistration");
		}

		[HttpGet]
		public async Task<IActionResult> PatientRegistration(string searchTerm)
		{
			var patients = await _patientService.ViewAllPAtients();
			var patientDto = _mapper.Map<List<PatientDTO>>(patients);

			if (!string.IsNullOrEmpty(searchTerm))
			{
				// search by phone OR name (case-insensitive for name)
				var matchedPatients = patientDto
					.Where(p => (!string.IsNullOrEmpty(p.Phone) && p.Phone.Contains(searchTerm)) ||
								(!string.IsNullOrEmpty(p.Name) && p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)))
					.ToList();

				if (matchedPatients.Any())
				{
					// highlight first match
					ViewBag.HighlightedId = matchedPatients.First().Id;
					return View(matchedPatients);
				}
				else
				{
					ViewBag.NotFound = true;
					return View(new List<PatientDTO>()); // return empty list if no match
				}
			}

			// if no search term → show all patients
			return View(patientDto);
		}


		[HttpPost]
		public async Task<IActionResult> PatientRegistration(PatientDTO patient)
		{

			var registerPatients= await  _patientService.RegisterPatients(patient);
			return RedirectToAction("PatientDetails", new { id = registerPatients.Id });
		}
		[HttpGet]
		public async Task<IActionResult> SearchedPatients(string searchedPatients)
		{

			var registerPatients = await _patientService.GetPatientByPhone(searchedPatients);
			return RedirectToAction("PatientDetails", new { id = registerPatients.Id });
		}
		[HttpGet]
		public async Task<IActionResult> SearchPatient(string searchTerm)
		{
			var patients =await _patientService.GetPatientByPhone(searchTerm);

			return View("PatientDetails", patients);
		}

		[HttpGet]
		public async Task<IActionResult> PatientDetails(int id)
		{
			// Get patient from DB
			var patient = await _patientService.GetPatientById(id);

			if (patient == null)
			{
				return NotFound();
			}

			return View(patient);
		}
		[HttpGet]
		public async Task<IActionResult> EditPatient(int id)
		{
			var patient = await _patientService.GetPatientById(id);
			return View(patient);
		}
		[HttpPost]
		public async Task<IActionResult> EditPatient(int id,Patient patient)
		{
			await _patientService.EditPatient(patient);
			return View(patient);
		}
	}
}
