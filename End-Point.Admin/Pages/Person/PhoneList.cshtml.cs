using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Dtos;
using Services.Interfaces;
using System.Numerics;

namespace End_Point.Admin.Pages.Person
{
    public class PhoneListModel : PageModel
    {
        private readonly IPersonService personService;
        public PhoneListModel(IPersonService personService)
        {
            this.personService = personService;
        }
        public ResultDto result { get; set; }
        public string FullName { get; set; }
        public string PersonId { get; set; }
        public List<string> PhoneList { get; set; }
        [BindProperty]
        public AddPhoneDto editPhoneModel { get; set; }
        [BindProperty]
        public AddPhoneDto deletePhoneModel { get; set; }
        public async Task OnGetAsync()
        {
            result = new ResultDto(false, "");


        }
        public async Task<IActionResult> OnPostDeletePhoneAsync()
        {
            if (string.IsNullOrWhiteSpace(deletePhoneModel.NATIONALCODE))
            {
                result = new ResultDto(false, "error in receive data");
                return Page();
            }
            result = await personService.DeletePhoneAsync(deletePhoneModel);
            return Page();
        }
        public async Task<IActionResult> OnPostEditPhoneAsync()
        {
            if (string.IsNullOrWhiteSpace(editPhoneModel.TEL))
            {
                result = new ResultDto(false, "complete the feilds");
                return Page();
            }
            result = await personService.EditPhoneAsync(editPhoneModel);
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
                var phones = await personService.GetPhoneListAsync(NationalCode);
                if (phones.iSuccess)
                    PhoneList = phones.Data;
                else
                    PhoneList = null;
                return Page();
            }
            FullName = "";
            result = new ResultDto(false, person.Message);
            return Page();
        }
    }
}
