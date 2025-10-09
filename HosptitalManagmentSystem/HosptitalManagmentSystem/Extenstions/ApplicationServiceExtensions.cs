using HosptitalManagmentSystem.Helpers;
using HosptitalManagmentSystem.Interface;
using HosptitalManagmentSystem.Models;
using HosptitalManagmentSystem.Repository;
using HosptitalManagmentSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HosptitalManagmentSystem.Extenstions
{
	public static class ApplicationServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
		{
		services.AddDbContext<HospitalContext>(options =>
	options.UseSqlServer(config.GetConnectionString("DefaultConnectionString")));
			services.AddScoped<IAppoimentService, AppoimentService>();
			services.AddScoped<IAppoimentRepository, AppoimentRepository>();
			services.AddScoped<IPatientRepository, PatientRepository>();
			services.AddScoped<IPatientService, PatientService>();
			services.AddScoped<IDepartmentService, DepartmentService>();
			services.AddScoped<IDoctorService, DoctorService>();
			services.AddScoped<IDoctorRepository, Doctorrepostory>();
			services.AddScoped<IDepartmentRepository, DepartmentReposity>();
			services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
			services.AddScoped<ILoginRepository, LoginRepository>();
			services.AddScoped<IloginService, LoginService>();	
			return services;
		}
	}
}
