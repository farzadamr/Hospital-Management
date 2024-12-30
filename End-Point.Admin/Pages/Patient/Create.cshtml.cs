using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Dtos;
using Services.Interfaces;

namespace End_Point.Admin.Pages.Patient
{
    public class CreateModel : PageModel
    {
        private readonly IPersonService personService;
        private readonly IPatientService patientService;
        public CreateModel(IPatientService patientService, IPersonService personService)
        {
            this.personService = personService;
            this.patientService = patientService;
        }
        public ResultDto result { get; set; }
        public string FullName { get; set; }
        public string PersonId { get; set; }

        [BindProperty]
        public AddPatientDto addPatModel { get; set; }
        public async Task OnGetAsync()
        {
            result = new ResultDto(false, "");


        }
        public async Task<IActionResult> OnPostAddPatAsync()
        {
            if (!ModelState.IsValid)
            {
                result = new ResultDto(false, "Invalid Model Data");
                return Page();
            }
            PatientDto patient = new PatientDto
            {
                BIRTH_DATE = addPatModel.BIRTH_DATE,
                BLOOD_TYPE = addPatModel.BLOOD_TYPE,
                GENDER = addPatModel.GENDER == 1 ? true : false,
                NATIONALCODE = addPatModel.NATIONALCODE
            };
            var addResult = await patientService.AddPatientAsync(patient);
            result = addResult;
            return Page();
        }
        public async Task<IActionResult> OnPostFindPersonAsync(string NationalCode)
        {
            if (string.IsNullOrWhiteSpace(NationalCode))
            {
                result = new ResultDto(false, "Complete The Field"); return Page();
            }
            var person = await personService.GetPersonAsync(NationalCode);
            if (person.iSuccess)
            {
                FullName = person.Data.FirstName + " " + person.Data.LastName;
                PersonId = person.Data.NationalCode;
                result = new ResultDto(person.iSuccess, person.Message);
                return Page();
            }
            FullName = "";
            result = new ResultDto(false, person.Message);
            return Page();
        }
    }

    public class AddPatientDto
    {
        public string NATIONALCODE { get; set; }
        public DateTime BIRTH_DATE { get; set; }
        public int GENDER { get; set; }
        public string BLOOD_TYPE { get; set; }
    }
}
