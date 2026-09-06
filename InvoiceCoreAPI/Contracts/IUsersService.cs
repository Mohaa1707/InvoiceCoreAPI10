using InvoiceCoreAPI.DTO;
using InvoiceCoreAPI.Models;

namespace InvoiceCoreAPI.Contracts;

public interface IUsersService
{
    Task<ApiResponse<UsersDto>> AddAsync(UsersCreateDto users);
    Task<ApiResponse<IEnumerable<UsersDto>>> GetAllAsync();
    Task<ApiResponse<UsersDto?>> GetByIdAsync(int id);
    Task<ApiResponse<UsersDto>> UpdateAsync(int id, UserUpdateDto users);
    Task<ApiResponse<bool>> DeleteAsync(int id, string updatedBy);
    Task<ApiResponse<PagedResultDto<UsersDto>>> GetAllPagedAsync(UsersFilterDto filter);
    Task<UsersDto?> ValidateUserAsync(string username, string password);

}
