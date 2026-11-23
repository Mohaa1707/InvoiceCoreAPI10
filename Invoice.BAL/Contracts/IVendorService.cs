using Invoice.DTOs;

namespace Invoice.BAL.Contracts;

public interface IVendorService
{
    
    Task<int> AddAsync(VendorDto Dto);

    Task<IEnumerable<VendorDto>> GetAllAsync();

    Task<VendorDto?> GetByIdAsync(int id);

    Task<bool> UpdateAsync(VendorDto entity);

    Task<bool> DeleteAsync(int id);

    Task<PagedResultDto<VendorDto>> GetAllPagedAsync(string? VendorCode,string? VendorName,string? MobileNo,string? City,bool IsActive, int pageNumber,int pageSize);
}

