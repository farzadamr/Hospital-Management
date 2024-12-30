using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDoctorService
    {
        Task<ResultDto> AddDoctorAsync(DoctorDto doctor);
        Task<ResultDto<List<DoctorsListDto>?>> GetDoctorListAsync();
        Task<ResultDto> EditDoctorAsync(DoctorDto doctor);
    }
}
