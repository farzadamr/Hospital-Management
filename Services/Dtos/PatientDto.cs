using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public class PatientDto
    {
        public int ID { get; set; }
        public string NATIONALCODE { get; set; }
        public DateTime BIRTH_DATE { get; set; }
        public bool GENDER { get; set; }
        public string BLOOD_TYPE { get; set; }
    }
}
