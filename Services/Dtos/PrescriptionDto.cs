using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public class PrescriptionDto
    {
        public int ID { get; set; }
        public string DESCRIPTION { get; set; }
        public int PATIENTID { get; set; }
        public int DOCTORID { get; set; }
    }
}
