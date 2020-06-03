using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using QandA.Data.Model;

namespace QandA.Data
{
	public class DataRepository : IDataRepository
	{
		private readonly string _connectionString;

		public DataRepository(IConfiguration configuration)
		{
			_connectionString = configuration["ConnectionStrings:DefaultConnection"];
		}

		public AppointmentGetSingleResponse GetAppointment(int appointmentId)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				var appointment =
					connection.QueryFirstOrDefault<AppointmentGetSingleResponse>(
						@"EXEC dbo.Appointment_GetSingle @AppointmentId = @AppointmentId",
						new { AppointmentId = appointmentId}
						);
				return appointment;
			}
		}

		public IEnumerable<AppointmentGetManyResponse> GetAppointments()
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				return connection.Query<AppointmentGetManyResponse>(
					@"EXEC dbo.Appointment_GetMany"
					);
			}
		}

		public IEnumerable<StudentGetManyResponse> GetStudents()
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				return connection.Query<StudentGetManyResponse>(
					@"EXEC dbo.Students_GetMany"
					);
			}
		}

		public IEnumerable<StudentGetManyResponse> GetStudentsBySearch(string search)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				return connection.Query<StudentGetManyResponse>(
					@"EXEC dbo.Students_Get_BySearch @Search = @Search",
					new { Search = search}
					);
			}
		}

		public IEnumerable<AppointmentGetManyResponse> GetUnshownAppointments()
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				return connection.Query<AppointmentGetManyResponse>(
					@"EXEC dbo.Appointment_GetUnanswered"
					);
			}
		}

		public AppointmentGetSingleResponse PostAppointment(AppointmentPostRequest appointment)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				var appointmentId = connection.QueryFirst<int>(
					@"EXEC dbo.Appointment_Post
						@StudentId = @StudentId, @CourseId = @CourseId,
						@Description = @Description, @Start = @Start, 
						@Finish = @Finish",
						appointment
					);
				return GetAppointment(appointmentId);
			}
		}

		
		public bool StudentExists(int studentId)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				return connection.QueryFirst<bool>(
					@"EXEC dbo.Students_Exist @StudentId = @StudentId",
					new { StudentId = studentId}
					);
			}
		}
	}
}

