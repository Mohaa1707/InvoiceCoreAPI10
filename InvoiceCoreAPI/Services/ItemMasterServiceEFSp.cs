using AutoMapper;
using InvoiceCoreAPI.Contracts;
using InvoiceCoreAPI.Controllers;
using InvoiceCoreAPI.DTO;
using InvoiceCoreAPI.DTOs;
using InvoiceCoreAPI.Entities;
using InvoiceCoreAPI.Repositories;
namespace InvoiceCoreAPI.Services
{
    public class ItemMasterServiceEFSp : IItemMasterService
    {
        private readonly IItemmasterRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ItemMasterServiceEFSp> _logger;
        public ItemMasterServiceEFSp(IItemmasterRepository repository, IMapper mapper, ILogger<ItemMasterServiceEFSp> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<int> AddAsync(ItemmasterDto dto)
        {
            var entity = _mapper.Map<Itemmaster>(dto);
            return await _repository.AddAsync(entity);
        }
        public async Task<IEnumerable<ItemmasterDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ItemmasterDto>>(items);
        }
        public async Task<ItemmasterDto?> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item == null ? null : _mapper.Map<ItemmasterDto>(item);
        }
        public async Task<bool> UpdateAsync(ItemmasterDto dto)
        {
            var entity = _mapper.Map<Itemmaster>(dto);
            return await _repository.UpdateAsync(entity);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
        public async Task<PagedResultDto<ItemmasterDto>> GetAllPagedAsync(
ItemmasterFilterDto search)
        {
            _logger.LogInformation("ItemMaster Service GetAllPaged Method Called ");
            var result = await _repository.GetAllPagedAsync(
                search);

            return new PagedResultDto<ItemmasterDto>
            {
                Data = _mapper.Map<IEnumerable<ItemmasterDto>>(result.Data),
                TotalRecords = result.TotalRecords
            };
        }
    }
}