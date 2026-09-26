using Invoice.DTOs;
using Invoice.Data.Entities;
using Invoice.Model;


namespace Invoice.BAL.Contracts
{
    public interface IUsersService
    {
        Task<ApiResponse<UsersDto>> AddAsync(UserCreateDto users);
        Task<ApiResponse<IEnumerable<UsersEntity>>> GetAllAsync();
        Task<ApiResponse<UsersEntity?>> GetByIdAsync(int id);
        Task<ApiResponse<UsersEntity>> UpdateAsync(int id, UserUpdateDto users);
        Task<ApiResponse<bool>> DeleteAsync(int id, string updatedBy);
        Task<ApiResponse<PagedResultDto<UsersEntity>>> GetAllPagedAsync(UserFilterDto filter);
        Task<UsersEntity?> ValidateUserAsync(string username, string password);
    }
}
