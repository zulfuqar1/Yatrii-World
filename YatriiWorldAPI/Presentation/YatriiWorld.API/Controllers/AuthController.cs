using Microsoft.AspNetCore.Mvc;
using YatriiWorld.Application.DTOs.Registers;
using YatriiWorld.Application.Interfaces.Repositories;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Domain.Entities.YatriiWorld.Domain.Entities;

public class AuthController : Controller
{
    private readonly IRepository<User> _userRepository;

    //public AuthController(IRepository<User> userRepository)
    //{
    //    _userRepository = userRepository;
    //}

    //[HttpPost]
    //public async Task<IActionResult> Register(RegisterDto dto)
    //{
    //    var user = new User
    //    {
    //        Name = dto.Name,
    //        Surname = dto.Surname,
    //        Email = dto.Email,
    //        PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
    //        Role = "User"
    //    };

    //    await _userRepository.AddAsync(user);
    //    await _userRepository.SaveAsync();

    //    return RedirectToAction("Index", "Home");
    //}


    //[HttpPost]
    //public async Task<IActionResult> Register(RegisterDto dto)
    //{
    //    if (!ModelState.IsValid)
    //        return RedirectToAction("Index", "Home");

    //    var user = new User
    //    {
    //        Name = dto.Name,
    //        Surname = dto.Surname,
    //        Email = dto.Email,
    //        PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
    //        Role = "User"
    //    };

    //    await _userRepository.AddAsync(user);
    //    await _userRepository.SaveAsync();

    //    return RedirectToAction("Index", "Home");
    //}
}
