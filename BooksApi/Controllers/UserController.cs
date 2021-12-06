using BooksApi.Domain.Entities;
using BooksApi.Domain.Security;
using BooksApi.Infraestructure.Data.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BooksApi.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenConfigurations _token;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TokenConfigurations token)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._token = token;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserEntity user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser
                {
                    UserName = user.UserName,
                    PasswordHash = user.Password
                };
                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                {
                    return Ok();
                } 
                    return BadRequest(result.Errors);
            }
            return BadRequest("Faltam parâmetros");
        }
       [HttpPost]
        public async Task<IActionResult> Login([Required] UserEntity user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = await _userManager.FindByNameAsync(user.UserName);
                
                if (appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, user.Password, false, false);
                    if (result.Succeeded)
                    {
                        var tokenString = _token.TokenGenerate(user);
                        return Ok(new {token = tokenString});
                    }
                     return Unauthorized("Desautorizado");
                }
                    return BadRequest("Falta usuário");
            }
            return BadRequest("Faltam parâmetros");
        }
    }
}
