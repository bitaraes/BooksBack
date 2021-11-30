using System.ComponentModel.DataAnnotations;

namespace BooksApi.Domain.Dtos
{
    public class BookDtoCreate
    {
        [Required]
        [StringLength(50)]
        public string BookName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [StringLength(20)]
        public string Category { get; set; }
        [Required]
        [StringLength(20)]
        public string Author { get; set; }
    }
}
