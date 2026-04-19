using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using YatriiWorld.Application.DTOs.Tickets;
using YatriiWorld.Application.DTOs.Users;
using YatriiWorld.Application.Interfaces.Repositories;
using YatriiWorld.Domain.Entities;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public UsersController(UserManager<AppUser> userManager, IMapper mapper, ITicketRepository ticketRepository)
    {
        _userManager = userManager;
        _mapper = mapper;
        _ticketRepository = ticketRepository;
    }

    [HttpGet("GetProfile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null) return NotFound(new { message = "User not found" });

        return Ok(user);
    }

    [HttpPut("UpdateProfile")]
    public async Task<IActionResult> UpdateProfile([FromForm] UserUpdateDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.PhoneNumber = dto.PhoneNumber;
        user.Bio = dto.Bio;
        user.Address = dto.Address;
        user.City = dto.City;
        user.Region = dto.Region;
        user.Country = dto.Country;
        user.ZipCode = dto.ZipCode;

        if (dto.ProfileImage != null && dto.ProfileImage.Length > 0)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ProfileImage.FileName);

            user.ProfileImageUrl = "images/UserPP/" + fileName;
        }
        else if (!string.IsNullOrEmpty(dto.ProfileImageUrl))
        {
            user.ProfileImageUrl = dto.ProfileImageUrl;
        }

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return Ok(new { message = "redy", imageUrl = user.ProfileImageUrl });
        }
        return BadRequest(result.Errors);
    }






    [HttpGet("MyTickets")]
    public async Task<IActionResult> MyTickets()
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

        long userId = long.Parse(userIdStr);

        var tickets = await _ticketRepository.GetUserTicketsWithDetailsAsync(userId);

        return Ok(tickets);
    }
}