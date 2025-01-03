using End_Point.Admin.Pages.Patient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Dtos;
using Services.Interfaces;
using Services.Services;

namespace End_Point.Admin.Pages.Person
{
    public class AddPhoneModel : PageModel
    {
        private readonly IPersonService personService;
        private readonly IPatientService patientService;
        public AddPhoneModel(IPatientService patientService, IPersonService personService)
        {
            this.personService = personService;
            this.patientService = patientService;
        }
        public ResultDto result { get; set; }
        public string FullName { get; set; }
        public string PersonId { get; set; }

        [BindProperty]
        public AddPhoneDto addPhoneModel { get; set; }
        public async Task OnGetAsync()
        {
            result = new ResultDto(false, "");


        }
        public async Task<IActionResult> OnPostAddPhoneAsync()
        {
            if (!ModelState.IsValid)
            {
                result = new ResultDto(false, "مقادیر را وارد کنید");
                return Page();
            }
            var addResult = await personService.AddPhoneAsync(addPhoneModel);
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
}
