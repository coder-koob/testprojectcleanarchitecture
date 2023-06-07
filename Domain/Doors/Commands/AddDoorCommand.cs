using Domain.Common;
using Domain.Interfaces;
using Domain.Offices;
using MediatR;

namespace Domain.Doors.Commands;

public record AddDoorPayload(Guid OfficeId, Guid DoorId);

public class AddDoorCommand : Command<AddDoorPayload>
{
    public AddDoorCommand(AddDoorPayload payload)
        : base("AddDoor", payload)
    {
    }
}

public class AddDoorCommandHandler : IRequestHandler<AddDoorCommand>
{
    private readonly IEventStore _eventStore;
    private readonly IOfficeFactory _officeFactory;

    public AddDoorCommandHandler(IEventStore eventStore, IOfficeFactory officeFactory)
    {
        _eventStore = eventStore;
        _officeFactory = officeFactory;
    }

    public async Task Handle(AddDoorCommand command, CancellationToken cancellationToken)
    {
        var officeEvents = await _eventStore.GetEvents(command.Payload.OfficeId);
        var office = _officeFactory.Rehydrate(officeEvents);

        if (office is not null)
        {
            office.AddDoor(command.Payload.DoorId);

            foreach (var @event in office.GetChanges())
            {
                await _eventStore.SaveEvent(command.Payload.OfficeId, @event);
            }

            office.ClearChanges();   
        }
    }
}