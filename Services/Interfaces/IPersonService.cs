using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPersonService
    {
        Task<ResultDto> AddPersonAsync(PersonDto person);
        Task<ResultDto> EditPersonAsync(PersonDto person);
        Task<ResultDto<PersonDto?>> GetPersonAsync(string nationalCode);
        Task<ResultDto<List<PersonDto>?>> GetPersonListAsync(string nationalCode);
        Task<ResultDto> DeletePersonAsync(string nationalCode);
    }
}
