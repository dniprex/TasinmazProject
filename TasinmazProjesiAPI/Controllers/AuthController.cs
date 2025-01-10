﻿using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using TasinmazProjesiAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using TasinmazProjesiAPI.Entitites.Concrete;
using System.Linq;

namespace TasinmazProjesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || user.Password != PasswordHelper.HashPassword(request.Password))
                return Unauthorized("Invalid credentials");

            // Token oluşturma simülasyonu
            var token = "Generated_JWT_Token";
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (_context.Users.Any(u => u.Email == request.Email))
                return BadRequest("Email already exists");

            var user = new User
            {
                Email = request.Email,
                Password = PasswordHelper.HashPassword(request.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User registered successfully");
        }
    }
}
public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
