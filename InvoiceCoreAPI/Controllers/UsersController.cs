using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InvoiceCoreAPI.Contracts;
using InvoiceCoreAPI.DTO;
using InvoiceCoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using InvoiceCoreAPI.Services;

namespace InvoiceCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _service;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUsersService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _service.GetAllAsync();

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occurred while getting all users.");

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        Success = false,
                        Message = "An error occurred while retrieving users."
                    });
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _service.GetByIdAsync(id);

                if (!response.Success)
                {
                    return NotFound(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
               

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        Success = false,
                        Message = "An error occurred while retrieving the user."
                    });
            }
        }

        [HttpPost("GetPaged")]
        public async Task<IActionResult> GetPaged(
        [FromBody] UsersFilterDto filter)
        {
            try
            {
                var response =
                    await _service.GetAllPagedAsync(filter);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error occurred while getting paged users.");

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        Success = false,
                        Message = "An error occurred while retrieving users."
                    });
            }
        }

        [HttpPost("Create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] UsersCreateDto dto)
        {
            try
            {
                var response =
                    await _service.AddAsync(dto);

                if (!response.Success)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error occurred while creating user.");

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        Success = false,
                        Message = "An error occurred while creating the user."
                    });
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(
        int id,
        [FromBody] UserUpdateDto dto)
        {
            try
            {
                var response =
                    await _service.UpdateAsync(id, dto);

                if (!response.Success)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error occurred while updating user. UserId: {UserId}",
                    id);

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        Success = false,
                        Message = "An error occurred while updating the user."
                    });
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id, string updatedBy)
        {
            try
            {
                var response =
                    await _service.DeleteAsync(id, updatedBy);

                if (!response.Success)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error occurred while deleting user. UserId: {UserId}",
                    id);

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        Success = false,
                        Message = "An error occurred while deleting the user."
                    });
            }
        }
    }
}