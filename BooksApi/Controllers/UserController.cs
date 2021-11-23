using BooksApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BooksApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser
                {
                    UserName = user.Login,
                    PasswordHash = user.Password
                };
                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                {
                    return Ok("Usuário Criado");
                } else
                {
                    return BadRequest(result.Errors);
                }
            }
            return BadRequest("Faltam parâmetros");
        }
       [HttpPost]
        public async Task<IActionResult> Login([Required] User user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = await _userManager.FindByNameAsync(user.Login);
                
                if (appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, user.Password, false, false);
                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Login),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        };
                        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("valid-authentication"));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: "Alura.WebApp",
                            audience: "Postman",
                            claims: claims,
                            signingCredentials: credentials,
                            expires: DateTime.Now.AddDays(7)
                            );
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                        return Ok(tokenString);
                    } else
                    {
                        return Unauthorized("Desautorizado");
                    }
                }
                else
                {
                    return BadRequest("Falta usuário");
                }
            }
            return BadRequest("Faltam parâmetros");
        }
    }
}
