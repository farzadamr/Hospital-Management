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
    public class PersonService : IPersonService
    {
        private readonly string _connection;
        public PersonService(string _connection)
        {
            this._connection = _connection;
        }
        public async Task<ResultDto> AddPersonAsync(PersonDto person)
        {
            var existPerson = await GetPersonAsync(person.NationalCode);
            if (existPerson.iSuccess)
                return new ResultDto(false, "Person with this national code in already existed");
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
        public async Task<ResultDto> EditPersonAsync(PersonDto person)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                using (SqlCommand command = new SqlCommand("UpdatePerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("NationalCode", person.NationalCode);
                    command.Parameters.AddWithValue("FirstName", person.FirstName);
                    command.Parameters.AddWithValue("LastName", person.LastName);
                    command.Parameters.AddWithValue("Password", person.Password);

                    await connection.OpenAsync();
                    int rows = await command.ExecuteNonQueryAsync();
                    if (rows > 0)
                    {
                        return new ResultDto(true, "person edited successfully!");
                    }
                    return new ResultDto(false, "Error in Execute Stored Procedure:(");

                }
            }
        }
        public async Task<ResultDto<PersonDto?>> GetPersonAsync(string nationalCode)
        {
            using(SqlConnection connection = new SqlConnection(_connection))
            {
                var person = await connection.QuerySingleOrDefaultAsync<PersonDto>(
                    "GetPerson",
                    new { Id = nationalCode },
                    commandType: CommandType.StoredProcedure
                    );
                if (person == null)
                    return new ResultDto<PersonDto?>(null, false, "The Person Was Not Found");
                return new ResultDto<PersonDto?>(person, true, "The Person Was Found");
            }
        }
        public async Task<ResultDto<List<PersonDto>?>> GetPersonListAsync(string nationalCode)
        {
            IEnumerable<PersonDto> personsEnum;
            using(SqlConnection connection = new SqlConnection(_connection))
            {
                if (string.IsNullOrWhiteSpace(nationalCode))
                {
                    personsEnum = await connection.QueryAsync<PersonDto>(
                        "GetPersonList",
                        commandType: CommandType.StoredProcedure
                        );
                }
                else
                {
                    personsEnum = await connection.QueryAsync<PersonDto>(
                        "SearchPerson",
                        new { SearchKey = nationalCode },
                        commandType: CommandType.StoredProcedure
                        );
                }
                if (personsEnum == null)
                    return new ResultDto<List<PersonDto>?>(null, false, "No Person Found");
                var personsList = personsEnum.ToList();
                return new ResultDto<List<PersonDto>?>(personsList, true, "Persons Found");
            }
        }
        public async Task<ResultDto> DeletePersonAsync(string nationalCode)
        {
            using(SqlConnection connection = new SqlConnection(_connection))
            {
                using(SqlCommand command = new SqlCommand("DeletePerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Id", nationalCode);

                    await connection.OpenAsync();
                    int rows = await command.ExecuteNonQueryAsync();
                    if (rows > 0)
                        return new ResultDto(true, $"Person {nationalCode} Deleted Successfully");
                    return new ResultDto(false, "Error in Execute Stored Procedure:(");
                }
            }
        }
    }
}
