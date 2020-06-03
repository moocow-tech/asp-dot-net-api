using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QandA.Data.Model
{
    public class StudentGetManyResponse
    {
        public int StudentId { get; set; }
        public string StuNam {get; set;}
        public string Email { get; set; }
    }
}
