using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApi.Domain.Entities
{
    public class BookEntity: BaseEntity
    {
        [Required]
        [MaxLength(50)]
        [BsonElement("Name")]
        public string BookName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(20)]
        public string Category { get; set; }
        [Required]
        [MaxLength(20)]
        public string Author { get; set; }
    }
}
