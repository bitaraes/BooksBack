using BooksApi.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApi.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<object> Login(UserDto user);
        Task<object> Create(UserDto user);
    }
}
