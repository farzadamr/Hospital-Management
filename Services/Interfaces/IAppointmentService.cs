using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<ResultDto> AddAppointmentAsync(AppointmentDto appointment);
        Task<ResultDto<List<AppointmentListDto>?>> GetAppointmentListAsync();
        Task<ResultDto> DeleteAppointmentAsync(int appointmentId);
        Task<ResultDto> EditAppointmentAsync(AppointmentDto appointment);
    }
}
