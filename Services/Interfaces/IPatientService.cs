using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPatientService
    {
        Task<ResultDto> AddPatientAsync(PatientDto patient);
        Task<ResultDto> EditPatientAsync(PatientDto patient);
        Task<ResultDto> DeletePatientAsync(int patientId);
        Task<ResultDto<List<PatientsListDto>?>> GetPatientListAsync();
        Task<ResultDto<PatientDto?>> GetPatientAsync(int patientId);
        Task<ResultDto<List<AddressDto>?>> GetAddressListAsync(int patientID);
        Task<ResultDto> DeleteAddressAsync(int addressId);
        Task<ResultDto> EditAddressAsync(AddressDto address);
        Task<ResultDto> AddAddressAsync(AddressDto address);
    }
}
