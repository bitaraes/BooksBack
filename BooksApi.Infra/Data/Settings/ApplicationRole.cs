using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDbGenericRepository.Attributes;
using System;

namespace BooksApi.Infraestructure.Data.Settings
{
    [CollectionName("Roles")]
    public class ApplicationRole : MongoIdentityRole<Guid>
    {
    }
}