using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace QandA.Data.Model
{
    public class AppointmentPostRequest
    {
        [Required]
        public int StudentId { get; set; }
        [Required(ErrorMessage =
            "All appointments must have a valid student id")]
        public string CourseId { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime Finish { get; set; }

        public bool Shown { get; set; }
    }
}
