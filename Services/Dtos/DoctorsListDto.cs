using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public class DoctorsListDto
    {
        public int M_S_N { get; set; }
        public int Rate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Resume { get; set; }
        public string PersonId { get; set; }
    }
}
