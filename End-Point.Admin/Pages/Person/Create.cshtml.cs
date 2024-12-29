using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Dtos;
using Services.Interfaces;

namespace End_Point.Admin.Pages.Person
{
    public class CreateModel : PageModel
    {
        private readonly IPersonService personService;
        public CreateModel(IPersonService personService)
        {
            this.personService = personService;
        }
        public ResultDto result { get; set; }
        [BindProperty]
        public PersonDto personModel { get; set; }
        public void OnGet()
        {
            result = new ResultDto(false, "");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                result = new ResultDto(false, "invalid model state");
                return Page();
            }
            var insertedResult = await personService.AddPersonAsync(personModel);
            result = insertedResult;
            return Page();
        }
    }
}
