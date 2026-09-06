using AutoMapper;
using InvoiceCoreAPI.Contracts;
using InvoiceCoreAPI.DTO;
using InvoiceCoreAPI.Entities;
using InvoiceCoreAPI.Repositories;
namespace InvoiceCoreAPI.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;
    public CustomerService(ICustomerRepository repository, IMapper mapper)
    {
        _repository = repository; _mapper = mapper;
    }
    public async Task<int> AddAsync(CustomerDto dto)
    {
        var entity = _mapper.Map<Customer>(dto);
        return await _repository.AddAsync(entity);
    }
    public async Task<IEnumerable<CustomerDto>> GetAllAsync()
    {
        var customers = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<CustomerDto>>(customers);
    }
    public async Task<CustomerDto?> GetByIdAsync(int id)
    {
        var customers = await _repository.GetByIdAsync(id);
        return customers == null ? null : _mapper.Map<CustomerDto?>(customers);
    }
    public async Task<bool> UpdateAsync(CustomerDto dto)
    {
        var entiry = _mapper.Map<Customer>(dto);
        return await _repository.UpdateAsync(entiry);
    }
    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
    public async Task<PagedResultDto<CustomerDto>> GetAllPagedAsync(
        string? CustomerCode,
        string? CustomerName,
        string? MobileNo,
        string? City,
        int PageNumber,
        int PageSize
        )
    {
        var result = await _repository.GetAllPagedAsync(CustomerCode, CustomerName, MobileNo, City,
            PageNumber, PageSize);
        return new PagedResultDto<CustomerDto>
        {
            Data = _mapper.Map<IEnumerable<CustomerDto>>(result.Data),
            TotalRecords = result.TotalRecords

        };
    }
}
