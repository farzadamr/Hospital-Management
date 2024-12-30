using Dapper;
using Services.Dtos;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PatientService : IPatientService
    {
        private readonly string _connection;
        public PatientService(string _connection)
        {
            this._connection = _connection;
        }
        public async Task<ResultDto> AddPatientAsync(PatientDto patient)
        {
            using(SqlConnection connection = new SqlConnection(_connection))
            {
                var patId = await connection.QueryAsync<int>(
                    "InsertPatient",
                    new
                    {
                        BIRTH_DATE = patient.BIRTH_DATE.Date,
                        BLOOD_TYPE = patient.BLOOD_TYPE,
                        GENDER = patient.GENDER,
                        NATIONALCODE = patient.NATIONALCODE
                    }, commandType: CommandType.StoredProcedure
                    );
                if (patId == null)
                    return new ResultDto(false, "Error Execute SP");
                return new ResultDto(true, $"Patient {string.Join("", patId)} Added Successfully");
            }
        }

        public async Task<ResultDto> DeletePatientAsync(int patientId)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                using (SqlCommand command = new SqlCommand("DeletePatient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Id", patientId);
                    await connection.OpenAsync();
                    int rows = await command.ExecuteNonQueryAsync();
                    if (rows > 0)
                        return new ResultDto(true, "Patient Deleted Successfully");
                    return new ResultDto(false, "Error Executing SP");
                }
            }
        }

        public async Task<ResultDto> EditPatientAsync(PatientDto patient)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                using (SqlCommand command = new SqlCommand("UpdatePatient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("ID", patient.ID);
                    command.Parameters.AddWithValue("BIRTH_DATE", patient.BIRTH_DATE);
                    command.Parameters.AddWithValue("GENDER", patient.GENDER);
                    command.Parameters.AddWithValue("BLOOD_TYPE", patient.BLOOD_TYPE);

                    await connection.OpenAsync();
                    int rows = await command.ExecuteNonQueryAsync();
                    if (rows > 0)
                        return new ResultDto(true, $"Patient {string.Join("", patient.ID)} Edited Successfully");
                    return new ResultDto(false, "Error Execute SP");
                }
            }
        }
        public async Task<ResultDto<List<PatientsListDto>?>> GetPatientListAsync()
        {
            using(SqlConnection connection = new SqlConnection(_connection))
            {
                var patientsEnum = await connection.QueryAsync<PatientsListDto>("GetPatientList", commandType: CommandType.StoredProcedure);
                if (patientsEnum == null)
                    return new ResultDto<List<PatientsListDto>?>(null, false, "Error in Executing SP");
                return new ResultDto<List<PatientsListDto>?>(patientsEnum.ToList(), true, "Fetching Successfully");
            }
        }
    }
}
