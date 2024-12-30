using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Dtos;
using Services.Interfaces;

namespace End_Point.Admin.Pages.Doctor
{
    public class CreateModel : PageModel
    {
        private readonly IDepartmentService departmentService;
        private readonly IPersonService personService;
        private readonly IDoctorService doctorService;
        public CreateModel(IDoctorService doctorService, IPersonService personService, IDepartmentService departmentService)
        {
            this.personService = personService;
            this.doctorService = doctorService;
            this.departmentService = departmentService;
        }
        public ResultDto result { get; set; }
        public SelectList departments { get; set; }
        public string FullName { get; set; }
        public string PersonId { get; set; }

        [BindProperty]
        public DoctorDto addDoctorModel { get; set; }
        public async Task OnGetAsync()
        {
            result = new ResultDto(false, "");
            

        }
        public async Task<IActionResult> OnPostAddDoctorAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            var addDoctor = await doctorService.AddDoctorAsync(addDoctorModel);
            result = addDoctor;
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
                var deps = await departmentService.GetDepartmentListAsync();
                if (!deps.iSuccess)
                    result = new ResultDto(false, "Error in retrieving data");
                departments = new SelectList(deps.Data, "ID", "TITLE");
                return Page();
            }
            FullName = "";
            result = new ResultDto(false, person.Message);
            return Page();
        }
    }
}
