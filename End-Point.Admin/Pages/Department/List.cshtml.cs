using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Dtos;
using Services.Interfaces;

namespace End_Point.Admin.Pages.Department
{
    public class ListModel : PageModel
    {
        private readonly IDepartmentService departmentService;
        public ListModel(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }
        public List<DepartmentDto> DepartmentsList { get; set; }
        public ResultDto result { get; set; }
        [BindProperty]
        public DepartmentDto editDepartmentModel { get; set; }

        public async Task OnGetAsync()
        {
            var deps = await departmentService.GetDepartmentListAsync();
            DepartmentsList = deps.Data;
            result = new ResultDto(true, "");

        }
        public async Task<IActionResult> OnPostEditDepAsync()
        {

            if (!ModelState.IsValid)
            {
                result = new ResultDto(false, "Complete The Model");
                return Page();
            }
            var editResult = await departmentService.EditDepartmentAsync(editDepartmentModel);
            result = editResult;
            return Redirect("/department/list");
        }
        public async Task<IActionResult> OnPostDeleteDepAsync(int departmentId)
        {
            if (departmentId != null)
            {
                var deleteResult = await departmentService.DeleteDepartmentAsync(departmentId);
                result = deleteResult;
                return Redirect("/department/list");
            }
            result = new ResultDto(false, "Error in Database");
            return Page();
        }
    }
}
