using Common.Entities.Interfaces;
using Common.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MicrocontrollersInfo.Repositories
{
    public class AutoincrementalRepository<T> : Repository<T>
             where T : class, IEntity
    {
        public AutoincrementalRepository(ICollection<T> collection)
            : base(collection)
        {
        }

        public override void Add(T item)
        {
            if (GetAll().Any())
            {
                item.Id = GetAll().Select(e => e.Id).Max() + 1;
            }
            else
            {
                item.Id = 1;
            }
            base.Add(item);
        }
    }
}

