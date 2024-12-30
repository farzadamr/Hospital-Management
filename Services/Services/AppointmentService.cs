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
    public class AppointmentService:IAppointmentService
    {
        private readonly string _connection;
        public AppointmentService(string _connection)
        {
            this._connection = _connection;
        }
        public async Task<ResultDto> AddAppointmentAsync(AppointmentDto appointment)
        {
            using(SqlConnection connection = new SqlConnection(_connection))
            {
                var appId = await connection.QueryAsync<int>("InsertAppointment", new
                {
                    ID = appointment.ID,
                    DOCTORID = appointment.DOCTORID,
                    DESCRIPTIONN = appointment.DESCRIPTIONN,
                    TIME_ = appointment.TIME_
                }, commandType: CommandType.StoredProcedure);
                
                if (appId == null)
                    return new ResultDto(false, "Error in Execute SP");
                return new ResultDto(true, "Appointment Added Successfully");
            }
        }
        public async Task<ResultDto<List<AppointmentListDto>?>> GetAppointmentListAsync()
        {
            IEnumerable<AppointmentListDto> appointmentsEnum;
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                appointmentsEnum = await connection.QueryAsync<AppointmentListDto>(
                    "GetAppointmentList",
                    commandType: CommandType.StoredProcedure
                    );
                if (appointmentsEnum == null)
                    return new ResultDto<List<AppointmentListDto>?>(null, false, "Appointment Don't Found");
                return new ResultDto<List<AppointmentListDto>?>(appointmentsEnum.ToList(), true, "Appointments Found");
            }
        }
        public async Task<ResultDto> EditAppointmentAsync(AppointmentDto appointment)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                using (SqlCommand command = new SqlCommand("UpdateAppointment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("ID", appointment.ID);
                    command.Parameters.AddWithValue("DESCRIPTIONN", appointment.DESCRIPTIONN);
                    command.Parameters.AddWithValue("TIME_", appointment.TIME_);
                    await connection.OpenAsync();
                    int rows = await command.ExecuteNonQueryAsync();
                    if (rows > 0)
                        return new ResultDto(true, "Appointment Edited Successfully");
                    return new ResultDto(false, "Error in Executing SP");
                }
            }
        }
        public async Task<ResultDto> DeleteAppointmentAsync(int appointmentId)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                using (SqlCommand command = new SqlCommand("DeleteAppointment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("ID", appointmentId);
                    await connection.OpenAsync();
                    int rows = await command.ExecuteNonQueryAsync();
                    if (rows > 0)
                        return new ResultDto(true, "Appointment Deleted Successfully");
                    return new ResultDto(false, "Error in Executing SP");
                }
            }
        }
    }
}
