using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Dtos;
using Services.Interfaces;

namespace End_Point.Admin.Pages.Patient
{
    public class AddressListModel : PageModel
    {
        private readonly IPatientService patientService;
        private readonly IPersonService personService;
        public AddressListModel(IPersonService personService, IPatientService patientService)
        {
            this.patientService = patientService;
            this.personService = personService;
        }
        public int patientId { get; set; }
        public ResultDto result { get; set; }
        public string FullName { get; set; }
        [BindProperty]
        public AddressDto editAddressModel { get; set; }
        public List<AddressDto> AddressList { get; set; }
        public void OnGet()
        {
            result = new ResultDto(false, "");
        }
        public async Task<IActionResult> OnPostEditAddressAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            result = await patientService.EditAddressAsync(editAddressModel);
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAddressAsync(int AddressId)
        {
            if (AddressId == 0)
                return Page();
            result = await patientService.DeleteAddressAsync(AddressId);
            return Page();
        }
        public async Task<IActionResult> OnPostFindPatientAsync(int PatientId)
        {
            if (PatientId == 0)
            {
                result = new ResultDto(false, "Enter The Patient ID");
                return Page();
            }
            var patient = await patientService.GetPatientAsync(PatientId);
            if (patient.iSuccess)
            {
                var person = await personService.GetPersonAsync(patient.Data.NATIONALCODE);
                FullName = person.Data.FirstName + " " + person.Data.LastName;
                patientId = PatientId;
                var addreslist = await patientService.GetAddressListAsync(PatientId);
                AddressList = addreslist.Data;
                result = new ResultDto(true, "");
                return Page();
            }
            result = new ResultDto(false, patient.Message);
            return Page();

        }
    }
}
