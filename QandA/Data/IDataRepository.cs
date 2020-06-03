using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QandA.Data.Model;
namespace QandA.Data
{
    public interface IDataRepository
    {
        // Get all students
        IEnumerable<StudentGetManyResponse> GetStudents();
        //Get all appointments
        IEnumerable<AppointmentGetManyResponse> GetAppointments();
        // Get students by search
        IEnumerable<StudentGetManyResponse> GetStudentsBySearch(string search);
        // Check if student exists
        bool StudentExists(int studentId);
        // Get open unshown/open appointments
        IEnumerable<AppointmentGetManyResponse> GetUnshownAppointments();
        // Get single appointment by id
        AppointmentGetSingleResponse GetAppointment(int appointmentId);
        // add Appointment
        AppointmentGetSingleResponse PostAppointment(AppointmentPostRequest appointment);


    }

}
