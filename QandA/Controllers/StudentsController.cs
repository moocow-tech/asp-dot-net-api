using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QandA.Data;
using QandA.Data.Model;
using Microsoft.AspNetCore.SignalR;
using QandA.Hubs;

namespace QandA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        private readonly IHubContext<AppointmentHub> _appointmentHubContext;
        public StudentsController(IDataRepository dataRepository, IHubContext<AppointmentHub> appointmentHubContext)
        { 
          _dataRepository = dataRepository;
          _appointmentHubContext = appointmentHubContext;
        }

        [HttpGet]
        public IEnumerable<StudentGetManyResponse> GetStudents(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return _dataRepository.GetStudents();
            } 
            else 
            {
                return _dataRepository.GetStudentsBySearch(search);
            }

        }
        [HttpGet("appointments")]
        public IEnumerable<AppointmentGetManyResponse> GetAppointments()
        {
            return _dataRepository.GetAppointments();
        }

        [HttpGet("unshown")]
        public IEnumerable<AppointmentGetManyResponse> GetUnshownAppointments() 
        {
            return _dataRepository.GetUnshownAppointments();
        }
        [HttpPost("unshown")]
        public ActionResult<AppointmentGetSingleResponse> PostAppointment(AppointmentPostRequest appointment)
        {
            var savedAppointment =
                _dataRepository.PostAppointment(appointment);
            _appointmentHubContext.Clients.Group(
                $"Question-{appointment}")
                .SendAsync(
                "RecievedAppointment",
                _dataRepository.GetUnshownAppointments()
                );
            return savedAppointment;
        }
    }
}
