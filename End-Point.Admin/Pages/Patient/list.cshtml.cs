using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Dtos;
using Services.Interfaces;

namespace End_Point.Admin.Pages.Patient
{
    public class listModel : PageModel
    {
        private readonly IPatientService patientService;
        public listModel(IPatientService patientService)
        {
            this.patientService = patientService;
        }
        public List<PatientsListDto> PatientsList { get; set; }

        public ResultDto result { get; set; }
        [BindProperty]
        public PatientDto editPatModel { get; set; }

        public async Task OnGetAsync()
        {
            var pats = await patientService.GetPatientListAsync();
            result = new ResultDto(true, pats.Message);
            PatientsList = pats.Data;

        }
        //public async Task<IActionResult> OnPostEditDoctorAsync()
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        result = new ResultDto(false, "Complete The Model");
        //        return Page();
        //    }
        //    var editResult = await doctorService.EditDoctorAsync(editDoctorModel);
        //    result = editResult;
        //    return Redirect("/doctor/list");
        //}
        //public async Task<IActionResult> OnPostDeleteDoctorAsync(int DoctorId)
        //{
        //    if (DoctorId != null)
        //    {
        //        var deleteResult = await doctorService.DeleteDoctorAsync(DoctorId);
        //        result = deleteResult;
        //        return Redirect("/doctor/list");
        //    }
        //    result = new ResultDto(false, "Error in Database");
        //    return Page();
        //}
    }
}
