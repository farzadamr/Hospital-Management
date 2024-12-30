using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public class PrescriptionListDto
    {
        public int ID { get; set; }
        public string DESCRIPTION { get; set; }
        public string PATIENTNAME{ get; set; }
        public string DOCTORNAME { get; set; }
    }
}
