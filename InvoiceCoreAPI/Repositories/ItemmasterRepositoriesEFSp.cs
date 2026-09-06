using InvoiceCoreAPI.Contracts;
using InvoiceCoreAPI.Controllers;
using InvoiceCoreAPI.Data;
using InvoiceCoreAPI.DTO;
using InvoiceCoreAPI.DTOs;
using InvoiceCoreAPI.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace InvoiceCoreAPI.Repositories;

public class ItemmasterRepositoriesEFSp : IItemmasterRepository
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<ItemmasterRepositoriesEFSp> _logger;
    public ItemmasterRepositoriesEFSp(AppDbContext dbContext, ILogger<ItemmasterRepositoriesEFSp> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public async Task<int> AddAsync(Itemmaster itemmaster)
    {
        var result = await _dbContext.Database.ExecuteSqlRawAsync(
            @"EXEC sp_Itemmaster_Insert
            @CategoryId,
            @ItemBarCode,
            @ItemCode,
            @ItemName,
            @Description,
            @Uom,
            @Rate,
            @Minimumstock,
            @Maximumstock,
            @IsActive",
            new SqlParameter("@CategoryId", itemmaster.CategoryId),
            new SqlParameter("@ItemBarCode", itemmaster.ItemBarCode),
            new SqlParameter("@Itemcode", itemmaster.ItemCode),
            new SqlParameter("@Itemname", itemmaster.ItemName),
            new SqlParameter("@Description", (object?)itemmaster.Description ?? DBNull.Value),
            new SqlParameter("@Uom", itemmaster.Uom),
            new SqlParameter("@Rate", (object?)itemmaster.Rate ?? DBNull.Value),
            new SqlParameter("@Minimumstock", (object?)itemmaster.MinimumStock ?? DBNull.Value),
            new SqlParameter("@Maximumstock", (object?)itemmaster.MaximumStock ?? DBNull.Value),
            new SqlParameter("@IsActive", (object?)itemmaster.IsActive ?? DBNull.Value)
            );
        return result;// returns affected rows

    }
    public async Task<bool> UpdateAsync(Itemmaster itemmaster)
    {
        var affectedRows = await _dbContext.Database.ExecuteSqlRawAsync(
            @"EXEC sp_Itemmaster_Update
            @Id,
            @CategoryId,
            @ItemBarCode,
            @Itemcode,
            @Itemname,
            @Description,
            @Uom,
            @Rate,
            @Minimumstock,
            @Maximumstock,
            @IsActive",
            new SqlParameter("@Id", itemmaster.Id),
            new SqlParameter("@CategoryId", itemmaster.CategoryId),
            new SqlParameter("@ItemBarCode", itemmaster.ItemBarCode),
            new SqlParameter("@Itemcode", itemmaster.ItemCode),
            new SqlParameter("@Itemname", itemmaster.ItemName),
            new SqlParameter("Description", (object?)itemmaster.Description ?? DBNull.Value),
            new SqlParameter("@Uom", itemmaster.Uom),
            new SqlParameter("@Rate", (object?)itemmaster.Rate ?? DBNull.Value),
            new SqlParameter("@Minimumstock", (object?)itemmaster.MinimumStock ?? DBNull.Value),
            new SqlParameter("@Maximumstock", (object?)itemmaster.MaximumStock ?? DBNull.Value),
            new SqlParameter("@IsActive", (object?)itemmaster.IsActive ?? DBNull.Value)
            );

        return affectedRows > 0;
    }

    public async Task<Itemmaster?> GetByIdAsync(int id)
    {
        var items = await _dbContext.Itemmasters
            .FromSqlRaw("EXEC sp_Itemmaster_GetById @Id",
            new SqlParameter("@Id", id))
            .AsNoTracking()
            .ToListAsync();
        return items.FirstOrDefault();
    }

    public async Task<IEnumerable<Itemmaster>> GetAllAsync()
    {
        return await _dbContext.Itemmasters
            .FromSqlRaw("EXEC sp_Itemmaster_GetAll")
            .ToListAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var affectedRows = await _dbContext.Database.ExecuteSqlRawAsync(
            "EXEC sp_Itemmaster_Delete @Id",
            new SqlParameter("@Id", id));

        return affectedRows > 0;
    }
    public async Task<PagedResultDto<Itemmaster>> GetAllPagedAsync(
     ItemmasterFilterDto search)
    {
        _logger.LogInformation("ItemsMaster Service Repostiory GetAllPaged Async Method Called");
        using var connection = _dbContext.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

        using var command = connection.CreateCommand();

        command.CommandText = "dbo.sp_Itemmaster_GetPaged";
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(
            new SqlParameter(
                "@CategoryId",
                (object?)search.CategoryId ?? DBNull.Value));

        command.Parameters.Add(
            new SqlParameter(
                "@ItemBarCode",
                (object?)search.ItemBarCode ?? DBNull.Value));

        command.Parameters.Add(
            new SqlParameter(
                "@ItemCode",
                (object?)search.ItemCode ?? DBNull.Value));

        command.Parameters.Add(
            new SqlParameter(
                "@ItemName",
                (object?)search.ItemName ?? DBNull.Value));

        command.Parameters.Add(
            new SqlParameter(
                "@Uom",
                (object?)search.Uom ?? DBNull.Value));

        command.Parameters.Add(
            new SqlParameter(
                "@IsActive",
                (object?)search.IsActive ?? DBNull.Value));

        command.Parameters.Add(
            new SqlParameter(
                "@PageNumber",
                search.PageNumber));

        command.Parameters.Add(
            new SqlParameter(
                "@PageSize",
                search.PageSize));

        using var reader = await command.ExecuteReaderAsync();

        var items = new List<Itemmaster>();

        // Result Set 1 - Itemmaster records
        while (await reader.ReadAsync())
        {
            items.Add(new Itemmaster
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),

                CategoryId = reader.GetInt32(
                    reader.GetOrdinal("CategoryId")),

                ItemBarCode = reader.GetString(
                    reader.GetOrdinal("ItemBarCode")),

                ItemCode = reader.GetString(
                    reader.GetOrdinal("ItemCode")),

                ItemName = reader.GetString(
                    reader.GetOrdinal("ItemName")),

                Description = reader.IsDBNull(
                    reader.GetOrdinal("Description"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("Description")),

                Uom = reader.GetString(
                    reader.GetOrdinal("Uom")),

                Rate = reader.GetDecimal(
                    reader.GetOrdinal("Rate")),

                MinimumStock = reader.GetDecimal(
                    reader.GetOrdinal("MinimumStock")),

                MaximumStock = reader.GetDecimal(
                    reader.GetOrdinal("MaximumStock")),

                IsActive = reader.GetBoolean(
                    reader.GetOrdinal("IsActive"))
            });
        }

        // Result Set 2 - TotalRecords
        await reader.NextResultAsync();

        var totalRecords = 0;

        if (await reader.ReadAsync())
        {
            totalRecords = reader.GetInt32(
                reader.GetOrdinal("TotalRecords"));
        }

        return new PagedResultDto<Itemmaster>
        {
            Data = items,
            TotalRecords = totalRecords
        };
    }
}
