using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Dtos;
using Services.Interfaces;

namespace End_Point.Admin.Pages.Doctor
{
    public class ListModel : PageModel
    {
        private readonly IDoctorService doctorService;
        private readonly IDepartmentService departmentService;
        public ListModel(IDepartmentService departmentService, IDoctorService doctorService)
        {
            this.doctorService = doctorService;
            this.departmentService = departmentService;
        }
        public List<DoctorsListDto> DoctorsList { get; set; }
        public SelectList Departments { get; set; }

        public ResultDto result { get; set; }
        [BindProperty]
        public DoctorDto editDoctorModel { get; set; }

        public async Task OnGetAsync()
        {
            var docs = await doctorService.GetDoctorListAsync();
            result = new ResultDto(true, docs.Message);
            DoctorsList = docs.Data;
            var deps = await departmentService.GetDepartmentListAsync();
            Departments = new SelectList(deps.Data, "ID", "TITLE");

        }
        public async Task<IActionResult> OnPostEditDoctorAsync()
        {

            if (!ModelState.IsValid)
            {
                result = new ResultDto(false, "Complete The Model");
                return Page();
            }
            var editResult = await doctorService.EditDoctorAsync(editDoctorModel);
            result = editResult;
            return Redirect("/doctor/list");
        }
        public async Task<IActionResult> OnPostDeleteDoctorAsync(int DoctorId)
        {
            if(DoctorId != null)
            {
                var deleteResult = await doctorService.DeleteDoctorAsync(DoctorId);
                result = deleteResult;
                return Redirect("/doctor/list");
            }
            result = new ResultDto(false, "Error in Database");
            return Page();
        }

    }
}
