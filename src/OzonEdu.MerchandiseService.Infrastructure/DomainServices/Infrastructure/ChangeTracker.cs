using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Models;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Infrastructure.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices.Infrastructure
{
    public class ChangeTracker : IChangeTracker
    {
        public IEnumerable<Entity> TrackedEntities => _usedEntitiesBackingField.ToArray();

        // Можно заменить на любую другую имплементацию. Не только через ConcurrentBag
        private readonly ConcurrentBag<Entity> _usedEntitiesBackingField;

        public ChangeTracker()
        {
            _usedEntitiesBackingField = new ConcurrentBag<Entity>();
        }
        
        public void Track(Entity entity)
        {
            if (entity is not null)
            {
                _usedEntitiesBackingField.Add(entity);
            }
            else
            {
                throw new ArgumentNullException($"The {nameof(entity)} should not be null");
            }
        }
    }
}