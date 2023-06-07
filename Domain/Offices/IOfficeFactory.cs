using Domain.Common;

namespace Domain.Offices;

public interface IOfficeFactory
{
    Office Create(string officeId);

    Office? Rehydrate(IEnumerable<Event> events);
}