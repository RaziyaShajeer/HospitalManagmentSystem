using HosptitalManagmentSystem.Models;

namespace HosptitalManagmentSystem.MiddleWare
{
	public class AppointmentCleanupMiddleware
	{
		private readonly RequestDelegate _next;
		private static bool _hasCleaned = false;

		// ✅ Constructor to inject next delegate
		public AppointmentCleanupMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
		{
			if (!_hasCleaned)
			{
				using (var scope = serviceProvider.CreateScope())
				{
					var db = scope.ServiceProvider.GetRequiredService<HospitalContext>();
					var today = DateTime.Today;

					var oldAppointments = db.Appointments
						.Where(a => a.AppointmentDate < today)
						.ToList();

					if (oldAppointments.Any())
					{
						db.Appointments.RemoveRange(oldAppointments);
						await db.SaveChangesAsync();
					}
				}

				_hasCleaned = true; // ✅ prevent multiple runs
			}

			await _next(context);
		}
	}

	// ✅ Separate static class for extension method
	public static class AppointmentCleanupMiddlewareExtensions
	{
		public static IApplicationBuilder UseAppointmentCleanup(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<AppointmentCleanupMiddleware>();
		}
	}
}
