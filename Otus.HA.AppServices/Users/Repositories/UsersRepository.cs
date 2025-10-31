using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Otus.HA.AppServices.Users.Data.Dto;
using Otus.HA.AppServices.Users.Data.Enums;
using Otus.HA.AppServices.Users.Data.Requests;

namespace Otus.HA.AppServices.Users.Repositories;

internal class UsersRepository : IUsersRepository
{
    private readonly string _connectionString;

    public UsersRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("otus-ha-service-db-connection") ??
                            throw new InvalidOperationException("Строка подключения к БД otus-ha-db не найдена в конфигурации.");
    }

    public async Task<Guid> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string query = """
                             INSERT INTO otus_ha.users (first_name, last_name, sex, birth_date, biography, city, password_hash)
                             VALUES (@first_name, @last_name, @sex, @birth_date, @biography, @city, crypt(@password, gen_salt('bf')))
                             RETURNING id;
                             """;

        var sex = request.Sex switch
        {
            Sex.Male => "М",
            Sex.Female => "Ж",
            _ => throw new ArgumentOutOfRangeException()
        };
        
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@first_name", request.FirstName);
        command.Parameters.AddWithValue("@last_name", request.LastName);
        command.Parameters.AddWithValue("@sex", sex);
        command.Parameters.AddWithValue("@birth_date", request.BirthDate);
        command.Parameters.AddWithValue("@biography", request.Biography);
        command.Parameters.AddWithValue("@city", request.City);
        command.Parameters.AddWithValue("@password", request.Password);

        var id = await command.ExecuteScalarAsync(cancellationToken);
        if (id == null)
        {
            throw new Exception("Не удалось создать пользователя.");
        }

        return (Guid)id;
    }

    public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string query = """
                             SELECT id, first_name, last_name, sex, birth_date, biography, city, password_hash
                             FROM otus_ha.users 
                             WHERE id = @id
                             """;

        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
    
        if (await reader.ReadAsync(cancellationToken))
        {
            return new UserDto
            {
                Id = reader.GetGuid(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Sex = reader.GetChar(3) == 'М' ? Sex.Male : Sex.Female,
                BirthDate = DateOnly.FromDateTime(reader.GetDateTime(4)),
                Biography = reader.GetString(5),
                City = reader.GetString(6),
                PasswordHash = reader.GetString(7),
            };
        }

        return null;
    }
}