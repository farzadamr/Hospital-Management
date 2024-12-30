using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Dtos;
using Services.Interfaces;

namespace End_Point.Admin.Pages.Department
{
    public class CreateModel : PageModel
    {
        private readonly IDepartmentService departmentService;
        public CreateModel(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        [BindProperty]
        public DepartmentDto addDepartmentModel { get; set; }
        public ResultDto result { get; set; }
        public void OnGet()
        {
            result = new ResultDto(false, "");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var addresult = await departmentService.AddDepartmentAsync(addDepartmentModel);
            result = addresult;
            return Page();
        }
    }
}
