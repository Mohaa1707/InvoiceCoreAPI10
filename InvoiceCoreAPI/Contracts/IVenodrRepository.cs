using InvoiceCoreAPI.DTO;
using InvoiceCoreAPI.Entities;
using System.Numerics;

namespace InvoiceCoreAPI.Contracts;

public interface IVendorRepository
{
    Task<int> AddAsync(Vendor vendor);
    Task<IEnumerable<Vendor>> GetAllAsync();
    Task<Vendor?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(Vendor vendor);
    Task<bool> DeleteAsync(int id);
    Task<PagedResultDto<Vendor>> GetAllPagedAsync(
        string? VendorCode,
        string? VendorName,
        string? MobileNo,
        string? City,
        int pageNumber,
        int pageSize);
}