using Invoice.DTOs;
using Invoice.Data.Entities;

namespace Invoice.DAL.Contracts
{
    public interface IUsersRepository
    {
        Task<int> AddAsync(UsersEntity entity);
        Task<IEnumerable<UsersEntity?>> GetAllAsync();
        Task<UsersEntity?> GetByIdAsync(int id);
        Task<UsersEntity?> GetByUserNameAsync(string userName);
        Task<UsersEntity?> GetByEmailAsync(string email);
        Task<bool> UpdateAsync(int id, UsersEntity entity);
        Task<bool> DeleteAsync(int id, string updatedBy);
        Task<PagedResultDto<UsersEntity>> GetAllPagedAsync(UserFilterDto filter);
        Task<bool> UpdateLastLoginAsync(int id);
    }
}
