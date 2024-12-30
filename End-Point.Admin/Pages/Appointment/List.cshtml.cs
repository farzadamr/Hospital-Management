using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Dtos;
using Services.Interfaces;

namespace End_Point.Admin.Pages.Appointment
{
    public class ListModel : PageModel
    {
        private readonly IAppointmentService appointmentService;
        public ListModel(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }
        public List<AppointmentListDto> AppointmentsList { get; set; }

        public ResultDto result { get; set; }
        [BindProperty]
        public AppointmentDto editAppointmentModel { get; set; }

        public async Task OnGetAsync()
        {
            var apps = await appointmentService.GetAppointmentListAsync();
            AppointmentsList = apps.Data;
            result = new ResultDto(true, apps.Message);

        }
        public async Task<IActionResult> OnPostEditAppAsync()
        {
            if (!ModelState.IsValid)
            {
                result = new ResultDto(false, "Complete The Model");
                return Page();
            }
            var editResult = await appointmentService.EditAppointmentAsync(editAppointmentModel);
            result = editResult;
            return Redirect("/appointment/list");
        }
        public async Task<IActionResult> OnPostDeleteAppAsync(int appId)
        {
            if (appId != null)
            {
                var deleteResult = await appointmentService.DeleteAppointmentAsync(appId);
                result = deleteResult;
                return Redirect("/appointment/list");
            }
            result = new ResultDto(false, "Error in Database");
            return Page();
        }

    }
}
