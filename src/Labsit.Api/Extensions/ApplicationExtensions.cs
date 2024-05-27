using Labsit.Api.Middlewares;
using Labsit.Domain.Entities;
using Labsit.Infrastructure.Context;

namespace Labsit.Api.Extensions
{
    internal static class ApplicationExtensions
    {
        internal static void ConfigureApplication(this IApplicationBuilder app)
        {
            app.ConfigureSwagger();
            app.ConfigureMiddlewares();
            app.EnsureDatabaseCreated();
        }

        internal static void ConfigureMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }

        internal static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", typeof(Program).Assembly.GetName().Name);
                options.RoutePrefix = "swagger";
                options.DisplayRequestDuration();
            });
        }

        internal static void EnsureDatabaseCreated(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LabsitContext>();
                context.Database.EnsureCreated();
                Seed(context);
            }
        }

        internal static void Seed(LabsitContext _context)
        {
            var customer = new Customer("Roberto Santos Andrade", "57208611068", DateOnly.FromDateTime(DateTime.Today.AddYears(-18)));
            _context.Customers.Add(customer);
            _context.SaveChanges();

            var bankAccount = new BankAccount(1, "001", "12345678", 7000, 7000, 7000);
            _context.BankAccounts.Add(bankAccount);
            _context.SaveChanges();

            var card = new Card(1, "5192858385719792", "Roberto S A", "123", Domain.Enums.ECardBrand.Visa, DateOnly.Parse("2040-01-01"));
            _context.Cards.Add(card);
            _context.SaveChanges();
        }
    }
}
