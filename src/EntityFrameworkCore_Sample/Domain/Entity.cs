using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCore_Sample.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public Entity()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
