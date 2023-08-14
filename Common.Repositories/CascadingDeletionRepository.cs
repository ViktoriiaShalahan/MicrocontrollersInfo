using Common.Entities.Interfaces;
using MicrocontrollersInfo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repositories
{
    public class CascadingDeletionRepository<T> : AutoincrementalRepository<T>
           where T : class, IEntity{
        public Action<T> BeforeDelete;
        public CascadingDeletionRepository(ICollection<T> collection, Action<T> BeforeDelete) : base(collection){
            this.BeforeDelete = BeforeDelete;
        }
        public override void Delete(T item)
        {

            BeforeDelete(item);
            base.Delete(item);
        }
    }
}
