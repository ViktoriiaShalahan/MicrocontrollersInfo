using Common.Entities.Interfaces;
using System;

namespace Common.Entities {
    [Serializable]
    public class Entity : IEntity {
        public int Id { get; set; }

        public override string ToString()
        {//
            return string.Format("{0} {1}", this.GetType().Name, this.Id);
        }
    }
}
