using AutoMapper;
using BooksApi.Domain.Dtos;
using BooksApi.Domain.Entities;
using BooksApi.Domain.Security;
using BooksApi.Infraestructure.Data.Repostory;
using BooksApi.Infraestructure.Data.Settings;
using Microsoft.AspNetCore.Authorization;
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

        private readonly UserRepository<UserEntity> _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenConfigurations _token;
        private readonly IMapper _mapper;

        public UserController(
            UserRepository<UserEntity> userService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            TokenConfigurations token,
            IMapper mapper)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _token = token;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDto user)
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
        public async Task<IActionResult> Login([Required] UserDto user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = await _userManager.FindByNameAsync(user.UserName);

                if (appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, user.Password, false, false);
                    if (result.Succeeded)
                    {
                        user.Id = appUser.Id.ToString();
                        var tokenString = _token.TokenGenerate(_mapper.Map<UserEntity>(user));
                        return Ok(new { token = tokenString });
                    }
                    return Unauthorized("Não autorizado");
                }
                return BadRequest("Falta usuário");
            }
            return BadRequest("Faltam parâmetros");
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([Required] string id)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = await _userManager.FindByIdAsync(id) ;
                return Ok(_mapper.Map<UserDto>(appUser));
            }
            return BadRequest();
        }
    }
}
