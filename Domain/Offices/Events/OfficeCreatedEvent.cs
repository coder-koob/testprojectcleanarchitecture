using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Offices.Events;

public class OfficeCreatedEvent : Event
{
    public OfficeCreatedEvent(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; set; }
}