using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Dtos;
using Services.Interfaces;

namespace End_Point.Admin.Pages.Prescription
{
    public class ListModel : PageModel
    {
        private readonly IAppointmentService appointmentService;
        public ListModel(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }
        public List<PrescriptionListDto> prescriptionList { get; set; }
        public ResultDto result { get; set; }
        [BindProperty]
        public EditPrescriptionDto editPrescriptionModel { get; set; }

        public async Task OnGetAsync()
        {
            var pres = await appointmentService.GetPrescriptionListAsync();
            prescriptionList = pres.Data;
            result = new ResultDto(true, "");
        }
        public async Task<IActionResult> OnPostEditPrAsync()
        {

            if (!ModelState.IsValid)
            {
                result = new ResultDto(false, "Complete The Model");
                return Page();
            }
            var editmodel = new PrescriptionDto
            {
                ID = editPrescriptionModel.ID,
                DESCRIPTION = editPrescriptionModel.DESCRIPTION
            };
            var editResult = await appointmentService.EditPrescriptionAsync(editmodel);
            result = editResult;
            return Redirect("/prescription/list");
        }
        public async Task<IActionResult> OnPostDeletePrAsync(int presId)
        {
            if (presId != null)
            {
                var deleteResult = await appointmentService.DeletePrescriptionAsync(presId);
                result = deleteResult;
                return Redirect("/prescription/list");
            }
            result = new ResultDto(false, "Error in Database");
            return Page();
        }

    }
    public class EditPrescriptionDto
    {
        public int ID { get; set; }
        public string DESCRIPTION { get; set; }
    }
}
