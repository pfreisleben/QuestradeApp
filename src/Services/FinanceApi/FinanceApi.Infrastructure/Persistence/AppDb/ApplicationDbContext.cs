using System.Reflection;
using FinanceApi.Domain.Entities;
using FinanceApi.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Infrastructure.Persistence.AppDb
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IMediator _mediator;


        public DbSet<Loan> Loans { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await base.SaveChangesAsync();
            await _mediator.DispatchDomainEventsAsync(this);
            return 1;
        }
    }
}