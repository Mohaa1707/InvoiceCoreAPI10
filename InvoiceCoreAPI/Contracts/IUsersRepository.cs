using InvoiceCoreAPI.DTO;
using InvoiceCoreAPI.Entities;


namespace InvoiceCoreAPI.Contracts;

public interface IUsersRepository
{
    Task<int> AddAsync(Users users);
    Task<IEnumerable<Users?>> GetAllAsync();
    Task<Users?> GetByIdAsync(int id);
    Task<Users?> GetByUserNameAsync(string userName);
    Task<Users?> GetByEmailAsync(string email);
    Task<bool> UpdateAsync(int id, Users users);
    Task<bool> DeleteAsync(int id, string updatedBy);
    Task<PagedResultDto<Users>> GetAllPagedAsync(UsersFilterDto filter);
    Task<bool> UpdateLastLoginAsync(int id);
}
