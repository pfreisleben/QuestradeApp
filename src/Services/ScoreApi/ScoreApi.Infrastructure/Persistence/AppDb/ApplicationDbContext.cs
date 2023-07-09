using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ScoreApi.Domain.Aggregates.ScoreAggregate;
using ScoreApi.Domain.Entities;
using ScoreApi.Infrastructure.Extensions;

namespace ScoreApi.Infrastructure.Persistence.AppDb
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IMediator _mediator;


        public DbSet<Occurrence> Occurrences { get; set; }
        public DbSet<Score> Scores { get; set; }
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