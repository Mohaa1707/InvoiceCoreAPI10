using InvoiceCoreAPI.Entities;
using InvoiceCoreAPI.DTO;   // <--- add this if PagedResultDto<T> is here

namespace InvoiceCoreAPI.Contracts
{
    public interface ICustomerRepository
    {
        Task<int> AddAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(int id);
        Task<PagedResultDto<Customer>> GetAllPagedAsync(
            string? CustomerCode,
            string? CustomerName,
            string? MobileNo,
            string? City,
            int PageNumber,
            int PageSize
            );
    }
}
