using Dapper;
using InvoiceCoreAPI.Contracts;
using InvoiceCoreAPI.DTO;
using InvoiceCoreAPI.Entities;
using System.Data;

namespace InvoiceCoreAPI.Repositories;

public class UserRepositorySpDap : IUsersRepository
{
    private readonly IDbConnection _connection;

    public UserRepositorySpDap(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<Users>> GetAllAsync()
    {
        return await _connection.QueryAsync<Users>(
            "dbo.sp_Users_GetAll",
            commandType: CommandType.StoredProcedure);
    }

    public async Task<Users?> GetByIdAsync(int id)
    {
        return await _connection.QueryFirstOrDefaultAsync<Users>(
            "dbo.sp_Users_GetById",
            new
            {
                Id = id
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<Users?> GetByUserNameAsync(
        string userName)
    {
        return await _connection.QueryFirstOrDefaultAsync<Users>(
            "dbo.sp_Users_GetByUserName",
            new
            {
                UserName = userName
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<Users?> GetByEmailAsync(
        string email)
    {
        return await _connection.QueryFirstOrDefaultAsync<Users>(
            "dbo.sp_Users_GetByEmail",
            new
            {
                Email = email
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<int> AddAsync(Users user)
    {
        var parameters = new
        {
            user.UserName,
            user.Email,
            user.PasswordHash,
            user.FirstName,
            user.MiddleName,
            user.LastName,
            user.DisplayName,
            user.PhoneNumber,
            user.AlternatePhone,
            user.AddressLine1,
            user.AddressLine2,
            user.City,
            user.State,
            user.ZipCode,
            user.Country,
            user.DateOfBirth,
            user.IsActive,
            user.CreatedBy
        };

        return await _connection.ExecuteScalarAsync<int>(
            "dbo.sp_Users_Insert",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> UpdateAsync(
        int id,
        Users user)
    {
        var parameters = new DynamicParameters();

        parameters.Add("Id", id);
        parameters.Add("UserName", user.UserName);
        parameters.Add("Email", user.Email);
        //parameters.Add("PasswordHash", user.PasswordHash);
        parameters.Add("FirstName", user.FirstName);
        parameters.Add("MiddleName", user.MiddleName);
        parameters.Add("LastName", user.LastName);
        parameters.Add("DisplayName", user.DisplayName);
        parameters.Add("PhoneNumber", user.PhoneNumber);
        parameters.Add("AlternatePhone", user.AlternatePhone);
        parameters.Add("AddressLine1", user.AddressLine1);
        parameters.Add("AddressLine2", user.AddressLine2);
        parameters.Add("City", user.City);
        parameters.Add("State", user.State);
        parameters.Add("ZipCode", user.ZipCode);
        parameters.Add("Country", user.Country);
        parameters.Add("DateOfBirth", user.DateOfBirth);
        parameters.Add("IsActive", user.IsActive);
        parameters.Add("UpdatedBy", user.UpdatedBy);

        var result = await _connection.ExecuteAsync(
            "dbo.sp_Users_Update",
            parameters,
            commandType: CommandType.StoredProcedure);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id, string updatedBy)
    {
        var parameters = new DynamicParameters();

        parameters.Add("Id", id);
        parameters.Add("UpdatedBy", updatedBy);

        var result = await _connection.QuerySingleAsync<bool>(
            "dbo.sp_Users_Delete",
            parameters,
            commandType: CommandType.StoredProcedure);

        return result;
    }
    /* public async Task<bool> DeleteAsync(int id, string updatedBy)
     {
         var result = await _connection.ExecuteAsync(
             "dbo.sp_Users_Delete",
             new
             {
                 Id = id,
                 UpdatedBy = updatedBy
             },
             commandType: CommandType.StoredProcedure);

         return result > 0;
     }*/

    public async Task<PagedResultDto<Users>> GetAllPagedAsync(
        UsersFilterDto filter)
    {
        using var multi = await _connection.QueryMultipleAsync(
            "dbo.sp_Users_GetPaged",
            new
            {
                UserName = filter.UserName,
                Email = filter.Email,
                FirstName = filter.FirstName,
                LastName = filter.LastName,
                IsActive = filter.IsActive,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            },
            commandType: CommandType.StoredProcedure);

        var data = (await multi.ReadAsync<Users>()).ToList();

        var totalRecords =
            await multi.ReadFirstOrDefaultAsync<int>();

        return new PagedResultDto<Users>
        {
            Data = data,
            TotalRecords = totalRecords
        };
    }

    public async Task<bool> UpdateLastLoginAsync(int id)
    {
        var result = await _connection.ExecuteAsync(
            "dbo.sp_Users_UpdateLastLogin",
            new
            {
                Id = id
            },
            commandType: CommandType.StoredProcedure);

        return result > 0;
    }
}