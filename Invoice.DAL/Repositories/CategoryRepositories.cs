using Invoice.Data.Db;
using Invoice.DTOs;
using Invoice.Model.AI;
using Invoice.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;

namespace Invoice.DAL.Repositories
{
    public class CategoryRepositories
    {
        private readonly AppDbContext _dbContext;
        public CategoryRepositories(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //public async Task<int> AddAsync(CategoryEntity entity)
        //{
        //    var result = await _dbContext.Database.ExecuteSqlRawAsync(
        //        @"EXEC sp_Category_Insert
        //    @Code,
        //    @Name,
        //    @Description,
        //    @IsActive",
        //        new SqlParameter("@Code", entity.Code),
        //        new SqlParameter("@name", entity.Name),
        //        new SqlParameter("@Description", (object?)entity.Description ?? DBNull.Value),
        //        new SqlParameter("@IsActive", (object?)entity.IsActive ?? DBNull.Value)
        //        );
        //    return result;

        //}

        public async Task<int> AddAsync(CategoryEntity category)
        {
            var connection = _dbContext.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }
            using var command = connection.CreateCommand();
            command.CommandText = "sp_Category_Insert";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Code", category.Code));
            command.Parameters.Add(new SqlParameter("@Name", category.Name));
            command.Parameters.Add(new SqlParameter("@Description",(object?)category.Description ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@IsActive",(object?)category.IsActive ?? DBNull.Value));
            var result = await command.ExecuteScalarAsync();
            if (result == null || result == DBNull.Value)
            {
                throw new InvalidOperationException("Category insert did not return the generated Id.");
            }
            return Convert.ToInt32(result);
        }

        public async Task<bool> UpdateAsync(CategoryEntity entity)
        {
            var affectedRows = await _dbContext.Database.ExecuteSqlRawAsync(
                @"EXEC sp_Category_Update
            @Id,
            @Code,
            @name,
            @Description,
            @IsActive",
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Code", entity.Code),
                new SqlParameter("@name", entity.Name),
                new SqlParameter("Description", (object?)entity.Description ?? DBNull.Value),
                new SqlParameter("@IsActive", (object?)entity.IsActive ?? DBNull.Value)
                );

            return affectedRows > 0;
        }

        public async Task<CategoryEntity?> GetByIdAsync(int id)
        {
            var items = await _dbContext.Categories
                .FromSqlRaw("EXEC sp_Category_GetById @Id",
                new SqlParameter("@Id", id))
                .AsNoTracking()
                .ToListAsync();
            return items.FirstOrDefault();
        }

        public async Task<IEnumerable<CategoryEntity>> GetAllAsync()
        {
            return await _dbContext.Categories
                .FromSqlRaw("EXEC sp_Category_GetAll")
                .ToListAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var affectedRows = await _dbContext.Database.ExecuteSqlRawAsync(
                "EXEC sp_Category_Delete @Id",
                new SqlParameter("@Id", id));

            return affectedRows > 0;
        }
        public async Task<PagedResultDto<CategoryEntity>> GetAllPagedAsync(
    string? Code,
    string? Name,
    int pageNumber,
    int pageSize)
        {
            using (var connection = _dbContext.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                command.CommandText = "sp_Category_GetPaged";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Code", (object?)Code ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Name", (object?)Name ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
                command.Parameters.Add(new SqlParameter("@PageSize", pageSize));

                using var reader = await command.ExecuteReaderAsync();

                var items = new List<CategoryEntity>();

                while (await reader.ReadAsync())
                {
                    items.Add(new CategoryEntity
                    {
                        Id = reader.GetInt32(0),
                        Code = reader.GetString(1),
                        Name = reader.GetString(2),
                        Description = reader.GetString(3),
                        IsActive = reader.GetBoolean(4)
                    });
                }

                await reader.NextResultAsync();

                int totalRecords = 0;
                if (await reader.ReadAsync())
                {
                    totalRecords = reader.GetInt32(0);
                }

                return new PagedResultDto<CategoryEntity>
                {
                    Data = items,
                    TotalRecords = totalRecords
                };

            }
        }

        public async Task<CategoryEntity?> GetByNameAsync(string name)
        {
            var categories = await _dbContext.Categories
                .FromSqlRaw(
                    "EXEC sp_Category_GetByName @Name",
                    new SqlParameter("@Name", name))
                .AsNoTracking()
                .ToListAsync();

            return categories.FirstOrDefault();
        }

        public async Task<CategoryItemCountResult?> GetCategoryItemCountAsync(string categoryName, bool categoryActiveOnly, bool? itemActiveOnly)
        {
            using var connection = _dbContext.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();

            command.CommandText = "dbo.sp_AI_CategoryItemCount";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(
                new SqlParameter("@CategoryName", categoryName));

            command.Parameters.Add(
                new SqlParameter("@CategoryActiveOnly", categoryActiveOnly));

            command.Parameters.Add(
                new SqlParameter("@ItemActiveOnly",
                    itemActiveOnly.HasValue
                        ? itemActiveOnly.Value
                        : DBNull.Value));

            using var reader = await command.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
            {
                return null;
            }

            return new CategoryItemCountResult
            {
                CategoryName = reader.GetString(
                    reader.GetOrdinal("CategoryName")),

                CategoryIsActive = reader.GetBoolean(
                    reader.GetOrdinal("CategoryIsActive")),

                ItemCount = reader.GetInt32(
                    reader.GetOrdinal("ItemCount"))
            };
        }
    }
}
