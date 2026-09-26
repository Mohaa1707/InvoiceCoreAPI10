using Invoice.DTOs;

namespace Invoice.BAL.Contracts;

public interface IItemMasterService
{
    Task<int> AddAsync(ItemmasterDto entity);
    Task<IEnumerable<ItemmasterDto>> GetAllAsync();
    Task<ItemmasterDto?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(ItemmasterDto entity);
    Task<bool> DeleteAsync(int id);

    Task<PagedResultDto<ItemmasterDto>> GetAllPagedAsync(ItemmasterFilterDto search);
}
