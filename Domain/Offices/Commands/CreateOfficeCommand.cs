using Domain.Common;
using Domain.Interfaces;
using MediatR;

namespace Domain.Offices.Commands;

public record CreateOfficePayload(string Name);

public class CreateOfficeCommand : Command<CreateOfficePayload, Guid>
{
    public CreateOfficeCommand(CreateOfficePayload payload)
        : base("CreateOffice", payload)
    {
    }
}

public class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, Guid>
{
    private readonly IOfficeFactory _officeFactory;
    private readonly IEventStore _eventStore;

    public CreateOfficeCommandHandler(IOfficeFactory officeFactory, IEventStore eventStore)
    {
        _officeFactory = officeFactory;
        _eventStore = eventStore;
    }

    public async Task<Guid> Handle(CreateOfficeCommand command, CancellationToken cancellationToken)
    {
        var office = _officeFactory.Create(Guid.NewGuid(), command.Payload.Name);

        foreach (var @event in office.GetChanges())
        {
            await _eventStore.SaveEvent(office.Id, @event);
        }

        office.ClearChanges();

        return office.Id;
    }
}
