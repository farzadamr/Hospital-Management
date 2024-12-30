using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Dtos;
using Services.Interfaces;

namespace End_Point.Admin.Pages.Appointment
{
    public class createModel : PageModel
    {
        private readonly IAppointmentService appointmentService;
        private readonly IDoctorService doctorService;
        public createModel(IAppointmentService appointmentService,IDoctorService doctorService)
        {
            this.appointmentService = appointmentService;
            this.doctorService = doctorService;
        }
        public ResultDto result { get; set; }
        [BindProperty]
        public AppointmentDto addAppointmentModel { get; set; }
        public SelectList DoctorsList { get; set; }
        public async Task OnGetAsync()
        {
            var docs = await doctorService.GetDoctorListAsync();
            List<DoctorsListSelect> selectlist = docs.Data.Select(doctor => new DoctorsListSelect
            {
                ID = doctor.M_S_N,
                FullName = doctor.FirstName + " " + doctor.LastName
            }).ToList();
            DoctorsList = new SelectList(selectlist, "ID", "FullName");
            result = new ResultDto(false, "");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                result = new ResultDto(false, "Complete Model");
                return Page();
            }
            var addResult = await appointmentService.AddAppointmentAsync(addAppointmentModel);
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
