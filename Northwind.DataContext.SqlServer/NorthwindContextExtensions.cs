using Microsoft.Data.SqlClient; //för SqlConnectionBuilder
using Microsoft.EntityFrameworkCore; //för addDbContext, UseSqlServer metod
using Microsoft.Extensions.DependencyInjection; //för IServiceCollection
using Microsoft.Extensions.Options;
using Northwind.DataContext.SqlServer; //för NorthwindDatabaseContextLogger
using Northwind.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.EntityModels
{
    public static class NorthwindContextExtensions
    {
        public static IServiceCollection AddNorthwindContext(
            this IServiceCollection services, //typen som extends
            string? connectionString = null)
        {
            if (connectionString is null) 
            {
                //can do this way:
                //connectionString = "Server=(localdb)\\mssqllocaldb;Database=Northwind;Trusted_Connection=True;MultipleActiveResultSets=true";
                
                //or you can copy from the DatabaseContext.cs from line 78 to 95and paste below
                SqlConnectionStringBuilder builder = new();

                builder.DataSource = "(localdb)\\MSSQLLocalDB"; //ServerName/InstanceName sqllocaldb info
                builder.InitialCatalog = "NorthwindDatabase";
                builder.TrustServerCertificate = true;
                builder.MultipleActiveResultSets = true;

                //visar timeout i 3 sekunder, default är 15 sekunder
                builder.ConnectTimeout = 3;

                //om ni vill använda Windows Authentication
                builder.IntegratedSecurity = true;

                //om ni vill använda SQL Server Authentication
                //builder.UserID = Environment.GetEnvironmentVariable("MY_SQL_USR");
                //builder.Password = Environment.GetEnvironmentVariable("MY_SQL_USR");

                connectionString = builder.ConnectionString;

            }
            services.AddDbContext<NorthwindDatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString);

                //the LogTo can copy from the DatabaseContext.cs from line 98 to 99 and paste below
                options.LogTo(NorthwindContextLogger.WriteLine,
                new[] { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting });
            },
            //Registrera NorthwindDatabaseContext med service lifetime Transient för att undvika problem med DbContext concurrency i Blazor Server projects
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Transient);

            return services;
        }
    }
}
