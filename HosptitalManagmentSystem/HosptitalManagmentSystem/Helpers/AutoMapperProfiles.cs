using AutoMapper;
using HosptitalManagmentSystem.DTOs;
using HosptitalManagmentSystem.Models;

namespace HosptitalManagmentSystem.Helpers
{
	public class AutoMapperProfiles:Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<PatientDTO, Patient>().ReverseMap();
			CreateMap<DepartmentDTO, Department>().ReverseMap();
			CreateMap<Department, DepartmentLIstDTO>().ReverseMap();
			CreateMap<DoctorDTO, Doctor>().ReverseMap();
			CreateMap<Doctor, DoctorListDto>()
	   .ForMember(dest => dest.Name,
				  opt => opt.MapFrom(src => src.Department.Name));
			CreateMap<DoctorListforApoiment, Doctor>().ReverseMap();
			CreateMap<DoctorListDto, DoctorListforApoiment>().ReverseMap();
			CreateMap<Appointment, AppoinmentViewDTo>().ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.DoctorName))
				.ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.Patient.Name)).ForMember(dest=>dest.DepartmentName,opt=>opt.MapFrom(src=>src.Doctor.Department.Name)).ForMember(dest
				=>dest.Location,opt=>opt.MapFrom(src=>
				src.Patient.Location)).ForMember(dest=>dest.phone,opt=>opt.MapFrom(src=>src.Patient.Phone));
			CreateMap<Appointment, AppoinmentListViewDTO>().ForMember(dest => dest.departmentName, opt => opt.MapFrom(src => src.Doctor.Department.Name)).ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.DoctorName)).ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Patient.Name)).ForMember(dest=>dest.Phone,opt=>opt.MapFrom(src=>src.Patient.Phone));
			CreateMap<Appointment, AppoinmentViewDTo>().ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Doctor.Department.Name)).ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.DoctorName)).ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Patient.Name)).ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Patient.Phone));
			CreateMap<Appointment, EditAppoinmentDTO>().ForMember(dest=>dest.PatientName,opt=>opt.MapFrom(src=>src.Patient.Name)).ForMember(dest=>dest.CustomId,opt=>opt.MapFrom(src=>src.Patient.CustomId)).ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Patient.Phone)).ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Patient.Location)).ForMember(dest=>dest.DepartmentId,opt=>opt.MapFrom(src=>src.Doctor.DepartmentID));	
			CreateMap<EditAppoinmentDTO, Appointment>();
		}
	}
}
