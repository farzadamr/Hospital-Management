using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<ResultDto<List<DepartmentDto>?>> GetDepartmentListAsync();
        Task<ResultDto> AddDepartmentAsync(DepartmentDto department);
        Task<ResultDto> EditDepartmentAsync(DepartmentDto department);
        Task<ResultDto> DeleteDepartmentAsync(int departmentId);
    }
}
