using Domain.Common;

namespace Domain.Offices;

public interface IOfficeFactory
{
    Office Create(Guid officeId, string name);

    Office? Rehydrate(IEnumerable<Event> events);
}