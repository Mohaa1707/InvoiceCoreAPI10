using Invoice.DTOs;

namespace Invoice.BAL.Contracts;

public interface ICustomerService
{
    Task<int> AddAsync(CustomerDto dto);
    Task<IEnumerable<CustomerDto>> GetAllAsync();
    Task<CustomerDto?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(CustomerDto dto);
    Task<bool> DeleteAsync(int id);
    Task<PagedResultDto<CustomerDto>> GetAllPagedAsync(
        string? CustomerCode,
        string? Customername,
        string? MobileNo,
        string? City,
        int PageNumber,
        int PageSize
        );
}
