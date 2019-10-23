using System;
using System.Collections.Generic;
using System.Text;

namespace Klenzer.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = new Guid();
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
