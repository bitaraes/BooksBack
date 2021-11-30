using System.ComponentModel.DataAnnotations;

namespace BooksApi.Domain.Dtos
{
    public class UserDtoCreate
    {
        [Required]
        [Display(Name = "Usuário")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
    }
}
