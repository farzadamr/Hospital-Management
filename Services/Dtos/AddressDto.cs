using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public class AddressDto
    {
        public int ID { get; set; }
        public int PLAK { get; set; }
        public string STREET { get; set; }
        public string POSTALCODE { get; set; }
        public int PATIENTID { get; set; }
    }
}
