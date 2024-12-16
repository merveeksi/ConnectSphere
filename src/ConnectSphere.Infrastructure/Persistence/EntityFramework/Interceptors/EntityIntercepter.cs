using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ConnectSphere.Infrastructure.Persistence.EntityFramework.Interceptors;

    public class EntityIntercepter : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;

        public EntityIntercepter(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService; // Mevcut kullanıcı hizmetini al
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateAuditableEntities(eventData.Context); // Denetim bilgilerini güncelle

            return result; 
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateAuditableEntities(eventData.Context); 

            return new ValueTask<InterceptionResult<int>>(result); 
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            UpdateAuditableEntities(eventData.Context); 

            return result;
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            UpdateAuditableEntities(eventData.Context); 
            return new ValueTask<int>(result);
        }

        private void UpdateAuditableEntities(DbContext? context)
        {
            if (context is null) 
                return; // Eğer bağlam null ise, işlemi sonlandır

            var createdByEntries = context
                .ChangeTracker
                .Entries<ICreatedByEntity>()
                .Where(e => e.State is EntityState.Added); // Eklenen varlıkları al

            foreach (var entry in createdByEntries)
            {
                entry.Entity.CreatedByUserId = _currentUserService.UserId.HasValue ? _currentUserService.UserId.Value.ToString() : null; // Oluşturan kullanıcıyı ayarla
                entry.Entity.CreatedOn = DateTimeOffset.UtcNow; // Oluşturulma zamanını ayarla
            }

            var modifiedByEntries = context
                .ChangeTracker
                .Entries<IModifiedByEntity>()
                .Where(e => e.State is EntityState.Modified); // Güncellenen varlıkları al

            foreach (var entry in modifiedByEntries)
            {
                entry.Entity.ModifiedByUserId = _currentUserService.UserId.HasValue ? _currentUserService.UserId.Value.ToString() : null; // Güncelleyen kullanıcıyı ayarla
                entry.Entity.ModifiedOn = DateTimeOffset.UtcNow; // Güncellenme zamanını ayarla
            }
        }
    }
