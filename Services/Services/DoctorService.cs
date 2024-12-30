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
    public class DoctorService:IDoctorService
    {
        private readonly string _connection;
        public DoctorService(string _connection)
        {
            this._connection = _connection;
        }
        public async Task<ResultDto> AddDoctorAsync(DoctorDto doctor)
        {
            using(SqlConnection connection = new SqlConnection(_connection))
            {
                var doctorId = await connection.QueryAsync<int>("InsertDoctor",
                    new
                    {
                        M_S_N = doctor.M_S_N,
                        RESUME_ = doctor.RESUME_,
                        DEPARTMENTID = doctor.DEPARTMENTID,
                        RATE = doctor.Rate,
                        PersonId = doctor.PersonId
                    }, commandType: CommandType.StoredProcedure);
                if(doctorId == null)
                {
                    return new ResultDto(false, "Error In Execute SP");
                }
                return new ResultDto(true, $"Doctor {string.Join("",doctorId)} Added Successfully");
            }
        }

        public async Task<ResultDto> DeleteDoctorAsync(int DoctorId)
        {
            using(SqlConnection connection = new SqlConnection(_connection))
            {
                using(SqlCommand command = new SqlCommand("DeleteDoctor", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Id", DoctorId);
                    await connection.OpenAsync();
                    int rows = await command.ExecuteNonQueryAsync();
                    if (rows > 0)
                        return new ResultDto(true, "Doctor Deleted Successfully");
                    return new ResultDto(false, "Error Executing SP");
                }
            }
        }

        public async Task<ResultDto> EditDoctorAsync(DoctorDto doctor)
        {
            using(SqlConnection connection = new SqlConnection(_connection))
            {
                using(SqlCommand command = new SqlCommand("UpdateDoctor", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("M_S_N", doctor.M_S_N);
                    command.Parameters.AddWithValue("DEPARTMENTID", doctor.DEPARTMENTID);
                    command.Parameters.AddWithValue("Rate", doctor.Rate);
                    command.Parameters.AddWithValue("RESUME_", doctor.RESUME_);

                    await connection.OpenAsync();
                    int rows = await command.ExecuteNonQueryAsync();
                    if (rows > 0)
                        return new ResultDto(true, $"Doctor{string.Join("", doctor.M_S_N)} Edited Successfully");
                    return new ResultDto(false, "Error Execute SP");
                }
            }
        }

        public async Task<ResultDto<List<DoctorsListDto>?>> GetDoctorListAsync()
        {
            IEnumerable<DoctorsListDto> doctorsEnum;
            using(SqlConnection connection = new SqlConnection(_connection))
            {
                doctorsEnum = await connection.QueryAsync<DoctorsListDto>(
                    "GetDoctorsList",
                    commandType: CommandType.StoredProcedure
                    );
                if (doctorsEnum == null)
                    return new ResultDto<List<DoctorsListDto>?>(null,false, "Error Execute SP");
                return new ResultDto<List<DoctorsListDto>?>(doctorsEnum.ToList(), true, "Fetch Doctors Successfully");
            }
        }
    }
}
