using InvoiceCoreAPI.DTO;
using InvoiceCoreAPI.Entities;
using InvoiceCoreAPI.Models.AI;

namespace InvoiceCoreAPI.Contracts;

public interface ICategoryRepository
{
    Task<int> AddAsync(Category category);
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(Category category);
    Task<bool> DeleteAsync(int id);
    Task<PagedResultDto<Category>> GetAllPagedAsync(
    string? Code,
    string? Name,
    int pageNumber,
    int pageSize);
    Task<Category?> GetByNameAsync(string name);
    Task<CategoryItemCountResult?> GetCategoryItemCountAsync(
    string categoryName,
    bool categoryActiveOnly,
    bool? itemActiveOnly);
}
