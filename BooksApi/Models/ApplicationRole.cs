using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDbGenericRepository.Attributes;
using System;

namespace BooksApi.Models
{
    [CollectionName("Roles")]
    class ApplicationRole : MongoIdentityRole<Guid>
    {
    }
}