using Dapper;
using Invoice.DAL.Contracts;
using Invoice.DTOs;
using Invoice.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Invoice.DAL.Repositories;

public class UserRepositorySpDap : IUsersRepository
{
    private readonly IDbConnection _connection;

    public UserRepositorySpDap(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<UsersEntity>> GetAllAsync()
    {
        return await _connection.QueryAsync<UsersEntity>(
            "dbo.sp_Users_GetAll",
            commandType: CommandType.StoredProcedure);
    }

    public async Task<UsersEntity?> GetByIdAsync(int id)
    {
        return await _connection.QueryFirstOrDefaultAsync<UsersEntity>(
            "dbo.sp_Users_GetById",
            new
            {
                Id = id
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<UsersEntity?> GetByUserNameAsync(
        string userName)
    {
        return await _connection.QueryFirstOrDefaultAsync<UsersEntity>(
            "dbo.sp_Users_GetByUserName",
            new
            {
                UserName = userName
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<UsersEntity?> GetByEmailAsync(
        string email)
    {
        return await _connection.QueryFirstOrDefaultAsync<UsersEntity>(
            "dbo.sp_Users_GetByEmail",
            new
            {
                Email = email
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<int> AddAsync(UsersEntity entity)
    {
        var parameters = new
        {
            entity.UserName,
            entity.Email,
            entity.PasswordHash,
            entity.FirstName,
            entity.MiddleName,
            entity.LastName,
            entity.DisplayName,
            entity.PhoneNumber,
            entity.AlternatePhone,
            entity.AddressLine1,
            entity.AddressLine2,
            entity.City,
            entity.State,
            entity.ZipCode,
            entity.Country,
            entity.DateOfBirth,
            entity.IsActive,
            entity.CreatedBy
        };

        return await _connection.ExecuteScalarAsync<int>(
            "dbo.sp_Users_Insert",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> UpdateAsync(
        int id,
        UsersEntity entity)
    {
        var parameters = new DynamicParameters();

        parameters.Add("Id", id);
        parameters.Add("UserName", entity.UserName);
        parameters.Add("Email", entity.Email);
        //parameters.Add("PasswordHash", user.PasswordHash);
        parameters.Add("FirstName", entity.FirstName);
        parameters.Add("MiddleName", entity.MiddleName);
        parameters.Add("LastName", entity.LastName);
        parameters.Add("DisplayName", entity.DisplayName);
        parameters.Add("PhoneNumber", entity.PhoneNumber);
        parameters.Add("AlternatePhone", entity.AlternatePhone);
        parameters.Add("AddressLine1", entity.AddressLine1);
        parameters.Add("AddressLine2", entity.AddressLine2);
        parameters.Add("City", entity.City);
        parameters.Add("State", entity.State);
        parameters.Add("ZipCode", entity.ZipCode);
        parameters.Add("Country", entity.Country);
        parameters.Add("DateOfBirth", entity.DateOfBirth);
        parameters.Add("IsActive", entity.IsActive);
        parameters.Add("UpdatedBy", entity.UpdatedBy);

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

    public async Task<PagedResultDto<UsersEntity>> GetAllPagedAsync(
        UserFilterDto filter)
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

        var data = (await multi.ReadAsync<UsersEntity>()).ToList();

        var totalRecords =
            await multi.ReadFirstOrDefaultAsync<int>();

        return new PagedResultDto<UsersEntity>
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
