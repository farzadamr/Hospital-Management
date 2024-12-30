using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public class AppointmentDto
    {
        public int ID { get; set; }
        public int DOCTORID { get; set; }
        public DateTime TIME_ { get; set; }
        public string DESCRIPTIONN { get; set; }
    }
}
