using AutoMapper;
using BooksApi.Domain.Entities;
using BooksApi.Infraestructure.Data.Mappings;
using BooksApi.Infraestructure.Data.Repostory;
using BooksApi.Infraestructure.Data.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApi.Test
{
    public class BaseTest
    {
       
    }

    public class DbTeste
    {
        private string databaseName = "BookStoreTest";
        public ServiceProvider serviceProvider { get; private set; }

        public DbTeste()
        {
            var connectionString = "mongodb://localhost:27017";
            var booksCollectionName = "Books";

            var serviceCollection = new ServiceCollection();
            var config = new AutoMapper.MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            IMapper mapper = config.CreateMapper();
            serviceCollection.AddSingleton(mapper);
            serviceCollection.AddTransient<BaseRepository<BookEntity>>();
            serviceCollection.AddIdentity<ApplicationUser, ApplicationRole>().AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
                    connectionString, databaseName
                );
            serviceCollection.Configure<BookstoreDatabaseSettings>(c => {
                c.ConnectionString = connectionString;
                c.DatabaseName = databaseName;
                c.BooksCollectionName = booksCollectionName;
            });
            serviceCollection.AddSingleton<IBookstoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<BookstoreDatabaseSettings>>().Value);


            serviceProvider = serviceCollection.BuildServiceProvider();


        }
    }
}