using Invoice.Data.Entities;
using Invoice.DTOs;

namespace Invoice.DAL.Contracts;

public interface IVendorRepository
{
    Task<int> AddAsync(VendorEntity entity);
    Task<IEnumerable<VendorEntity>> GetAllAsync();
    Task<VendorEntity?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(VendorEntity entity);
    Task<bool> DeleteAsync(int id);
    Task<PagedResultDto<VendorEntity>> GetAllPagedAsync(
        string? VendorCode,
        string? VendorName,
        string? MobileNo,
        string? City,
        int pageNumber,
        int pageSize);

    Task<int> GetVendorCountAsync(bool? activeOnly);
}
