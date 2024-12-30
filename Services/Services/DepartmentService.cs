using Dapper;
using Services.Dtos;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly string _connection;
        public DepartmentService(string _connection)
        {
            this._connection = _connection;
        }
        public async Task<ResultDto> AddDepartmentAsync(DepartmentDto department)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                var depId = await connection.QueryAsync<int>("InsertDepartment", new
                {
                    DESCRIPTION_ = department.DESCRIPTION_,
                    TITLE = department.TITLE
                }, commandType: CommandType.StoredProcedure);
                if (depId == null)
                    return new ResultDto(false, "Error in Execute SP");
                return new ResultDto(true, $"Department {string.Join(" ",depId)} Added Successfully");
            }
        }
        public async Task<ResultDto> DeleteDepartmentAsync(int departmentId)
        {
            using(SqlConnection connection = new SqlConnection(_connection))
            {
                using (SqlCommand command = new SqlCommand("DeleteDepartment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("ID", departmentId);
                    await connection.OpenAsync();
                    int rows = await command.ExecuteNonQueryAsync();
                    if (rows > 0)
                        return new ResultDto(true, "Department Deleted Successfully");
                    return new ResultDto(false, "Error in Executing SP");
                }
            }
        }
        public async Task<ResultDto> EditDepartmentAsync(DepartmentDto department)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                using(SqlCommand command = new SqlCommand("UpdateDepartment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("ID", department.ID);
                    command.Parameters.AddWithValue("TITLE", department.TITLE);
                    command.Parameters.AddWithValue("DESCRIPTION_", department.DESCRIPTION_);
                    await connection.OpenAsync();
                    int rows = await command.ExecuteNonQueryAsync();
                    if (rows > 0)
                        return new ResultDto(true, "Department Edited Successfully");
                    return new ResultDto(false, "Error in Executing SP");
                }
            }
        }
        public async Task<ResultDto<List<DepartmentDto>?>> GetDepartmentListAsync()
        {
            IEnumerable<DepartmentDto> departmentsEnum;
            using(SqlConnection connection = new SqlConnection(_connection))
            {
                departmentsEnum = await connection.QueryAsync<DepartmentDto>(
                    "GetDepartmentList",
                    commandType: CommandType.StoredProcedure
                    );
                if (departmentsEnum == null)
                    return new ResultDto<List<DepartmentDto>?>(null, false, "Department Don't Found");
                return new ResultDto<List<DepartmentDto>?>(departmentsEnum.ToList(), true, "Departments Found");
            }
        }
    }
}
