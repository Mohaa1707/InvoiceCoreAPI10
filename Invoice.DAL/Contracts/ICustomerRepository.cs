using Invoice.DTOs;
using Invoice.Data.Entities;

namespace Invoice.DAL.Contracts
{
    public interface ICustomerRepository
    {
        Task<int> AddAsync(CustomerEntity entity);
        Task<IEnumerable<CustomerEntity>> GetAllAsync();
        Task<CustomerEntity?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(CustomerEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<PagedResultDto<CustomerEntity>> GetAllPagedAsync(string? CustomerCode,string? CustomerName,string? MobileNo,string? City,int PageNumber,int PageSize);
        Task<int> GetCustomerCountAsync(bool? activeOnly);

    }
}
