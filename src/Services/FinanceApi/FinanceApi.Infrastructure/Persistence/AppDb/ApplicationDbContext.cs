using System.Reflection;
using FinanceApi.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Infrastructure.Persistence.AppDb
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IMediator _mediator;


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
            await _mediator.DispatchDomainEventsAsync(this);

            return await base.SaveChangesAsync();
        }
    }
}