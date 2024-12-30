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
        public EditPatientDto editPatModel { get; set; }

        public async Task OnGetAsync()
        {
            var pats = await patientService.GetPatientListAsync();
            result = new ResultDto(true, pats.Message);
            PatientsList = pats.Data;

        }
        public async Task<IActionResult> OnPostEditPatAsync()
        {
            if (!ModelState.IsValid)
            {
                result = new ResultDto(false, "Complete The Model");
                return Page();
            }
            PatientDto patientModel = new PatientDto
            {
                ID = editPatModel.ID,
                BLOOD_TYPE = editPatModel.BLOOD_TYPE,
                BIRTH_DATE = editPatModel.BIRTH_DATE,
                GENDER = editPatModel.GENDER == 1 ? true : false
            };
            var editResult = await patientService.EditPatientAsync(patientModel);
            result = editResult;
            return Redirect("/patient/list");
        }
        public async Task<IActionResult> OnPostDeletePatAsync(int PatId)
        {
            if (PatId != null)
            {
                var deleteResult = await patientService.DeletePatientAsync(PatId);
                result = deleteResult;
                return Redirect("/patient/list");
            }
            result = new ResultDto(false, "Error in Database");
            return Page();
        }
    }

    public class EditPatientDto
    {
        public int ID { get; set; }
        public DateTime BIRTH_DATE { get; set; }
        public int GENDER { get; set; }
        public string BLOOD_TYPE { get; set; }
    }
}

