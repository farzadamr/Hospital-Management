using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Dtos;
using Services.Interfaces;

namespace End_Point.Admin.Pages.Person
{
    public class ListModel : PageModel
    {
        private readonly IPersonService personService;
        public ListModel(IPersonService personService)
        {
            this.personService = personService;
        }
        public List<PersonDto> PersonsList { get; set; }
        public ResultDto result { get; set; }
        [BindProperty]
        public PersonDto editPersonModel { get; set; }
        [BindProperty]
        public string personId { get; set; }
        public void OnGet()
        {
            result = new ResultDto(false, "");
        }
        public async Task<IActionResult> OnPostGetList(string SearchKey = null)
        {
            var persons = await personService.GetPersonListAsync(SearchKey);
            if (!persons.iSuccess)
            {
                result = new ResultDto(false, persons.Message);
                return Page();
            }
            PersonsList = persons.Data;
            return Page();

        }
    }
}
