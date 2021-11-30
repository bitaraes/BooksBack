using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApi.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
