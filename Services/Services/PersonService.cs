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
    public class PersonService : IPersonService
    {
        private readonly string _connection;
        public PersonService(string _connection)
        {
            this._connection = _connection;
        }
        public async Task<ResultDto> AddPersonAsync(PersonDto person)
        {
            using(SqlConnection connection =  new SqlConnection(_connection))
            {
                using(SqlCommand command = new SqlCommand("InsertPerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("NationalCode", person.NationalCode);
                    command.Parameters.AddWithValue("FirstName", person.FirstName);
                    command.Parameters.AddWithValue("LastName", person.LastName);
                    command.Parameters.AddWithValue("Password", person.Password);

                    await connection.OpenAsync();
                    int rows = await command.ExecuteNonQueryAsync();
                    if(rows > 0)
                    {
                        return new ResultDto(true, "person added successfully!");
                    }
                    return new ResultDto(false, "Error in Execute Stored Procedure:(");
                
                }
            }
        }
    }
}
