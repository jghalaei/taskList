using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericContracts.Common;
using GenericContracts.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GenericTools.DbContexts
{
    public class GenericDbContext : DbContext
    {
        private readonly IUserAccessor _accessor;
        public GenericDbContext(DbContextOptions options, IUserAccessor accessor) : base(options)
        {
            _accessor = accessor;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var currentDateTime = DateTime.UtcNow;
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = currentDateTime;
                    entry.Entity.CreatedBy = _accessor.UserId;
                    entry.Entity.ModifiedAt = currentDateTime;
                    entry.Entity.ModifiedBy = _accessor.UserId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.CreatedAt).IsModified = false;
                    entry.Property(x => x.CreatedBy).IsModified = false;
                    entry.Entity.ModifiedAt = currentDateTime;
                    entry.Entity.ModifiedBy = _accessor.UserId;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}