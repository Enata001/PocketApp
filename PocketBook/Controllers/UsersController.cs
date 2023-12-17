using Microsoft.AspNetCore.Mvc;
using PocketBook.Core.IConfiguration;
using PocketBook.Models;

namespace PocketBook.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UsersController(ILogger<UsersController> logger, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        if (ModelState.IsValid)
        {
            user.Id = Guid.NewGuid();
            await _unitOfWork.Users.Add(user);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction("GetItem", new { user.Id }, user);
        }

        return new JsonResult("Something went wrong") { StatusCode = 500 };
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItem(Guid id)
    {
        var user = await _unitOfWork.Users.GetById(id);
        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var user = await _unitOfWork.Users.GetAll();
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await _unitOfWork.Users.GetById(id);
        if (user is null)
        {
            return BadRequest();
        }

        await _unitOfWork.Users.Delete(id);
        await _unitOfWork.CompleteAsync();

        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        await _unitOfWork.Users.Upsert(user);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }
}