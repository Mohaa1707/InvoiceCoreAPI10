using InvoiceCoreAPI.Contracts;
using InvoiceCoreAPI.DTO;
using InvoiceCoreAPI.DTOs;
using InvoiceCoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceCoreAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class ItemMasterController : ControllerBase
    {
        private readonly IItemMasterService _service;
        private readonly ILogger<ItemMasterController> _logger;
        public ItemMasterController(IItemMasterService service, ILogger<ItemMasterController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _service.GetAllAsync();
                return Ok(new ApiResponse<IEnumerable<ItemmasterDto>>
                {
                    Success = true,
                    Message = "Itemmaster retrieved successfully",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error retrieving Itemmaster",
                    Error = new ApiError
                    {
                        Code = "500",
                        Details = ex.Message
                    }
                });
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Item not found"
                    });
                }
                return Ok(new ApiResponse<ItemmasterDto>
                {
                    Success = true,
                    Message = "Item retrieved successfully",
                    Data = item
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error retrieving item",
                    Error = new ApiError
                    {
                        Code = "500",
                        Details = ex.Message
                    }
                });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ItemmasterDto dto)
        {
            try
            {
                var id = await _service.AddAsync(dto);
                return Ok(new ApiResponse<int>
                {
                    Success = true,
                    Message = "Item created successfully",
                    Data = id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error creating item",
                    Error = new ApiError
                    {
                        Code = "500",
                        Details = ex.Message
                    }
                });
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, ItemmasterDto dto)
        {
            try
            {
                dto.Id = id;
                var updated = await _service.UpdateAsync(dto);
                if (!updated)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Item not found"
                    });
                }
                return Ok(new ApiResponse<string>
                {
                    Success = true,
                    Message = "Item Updated successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error updating item",
                    Error = new ApiError
                    {
                        Code = "500",
                        Details = ex.Message
                    }
                });
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Item not found"
                    });
                }
                return Ok(new ApiResponse<string>
                {
                    Success = true,
                    Message = "Item deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error deleting item",
                    Error = new ApiError

                    {
                        Code = "500",
                        Details = ex.Message
                    }
                });
            }
        }
        [HttpGet("GetAllPaged")]
        public async Task<IActionResult> GetAllPaged([FromQuery] ItemmasterFilterDto search)
        {
            try
            {
                var result = await _service.GetAllPagedAsync(search);
                _logger.LogInformation("Items retrieved successfully");

                return Ok(new ApiResponse<IEnumerable<ItemmasterDto>>
                {
                    Success = true,
                    Message = "Items retrieved successfully",
                    Data = result.Data,
                    TotalRecords = result.TotalRecords
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error retrieving items",
                    Error = new ApiError
                    {
                        Code = "500",
                        Details = ex.Message
                    }
                });
            }
        }

        [HttpGet("TestException")]
        public IActionResult TestException()
        {
            throw new Exception("This is a test exception");
        }
    }
}