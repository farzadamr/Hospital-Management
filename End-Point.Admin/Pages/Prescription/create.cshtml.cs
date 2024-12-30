using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Dtos;
using Services.Interfaces;

namespace End_Point.Admin.Pages.Prescription
{
    public class createModel : PageModel
    {
        private readonly IAppointmentService appointmentService;
        private readonly IDoctorService doctorService;
        private readonly IPatientService patientService;
        public createModel(IPatientService patientService, IAppointmentService appointmentService, IDoctorService doctorService)
        {
            this.appointmentService = appointmentService;
            this.doctorService = doctorService;
            this.patientService = patientService;
        }
        public ResultDto result { get; set; }
        [BindProperty]
        public PrescriptionDto addPrescriptionModel { get; set; }
        public SelectList DoctorsList { get; set; }
        public async Task OnGetAsync()
        {
            var docs = await doctorService.GetDoctorListAsync();
            List<DoctorsListSelect> selectlist = docs.Data.Select(doctor => new DoctorsListSelect
            {
                ID = doctor.M_S_N,
                FullName = doctor.FirstName + " " + doctor.LastName
            }).ToList();
            result = new ResultDto(false, "");
            DoctorsList = new SelectList(selectlist, "ID", "FullName");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                result = new ResultDto(false, "Complete Model");
                return Page();
            }
            var patientFind = await patientService.GetPatientAsync(addPrescriptionModel.PATIENTID);
            if (!patientFind.iSuccess)
            {
                result = new ResultDto(false, "Patient Not Found!");
                return Page();
            }
            var addResult = await appointmentService.AddPrescriptionAsync(addPrescriptionModel);
            result = addResult;
            return Page();
        }
    }
    public class DoctorsListSelect
    {
        public int ID { get; set; }
        public string FullName { get; set; }
    }
}
