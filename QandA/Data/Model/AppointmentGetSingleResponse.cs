using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QandA.Data.Model
{
    public class AppointmentGetSingleResponse
    {
        public int AppointmentId { get; set; }
        public int StudentId { get; set; }
        public string CourseId { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public bool Shown { get; set; }

    }
}
