using Domain.Common;
using Domain.Doors.Events;
using Domain.Interfaces;
using Domain.Offices;
using MediatR;

namespace Domain.Doors.Commands;

public record LockDoorPayload(Guid OfficeId, Guid DoorId);

public class LockDoorCommand : Command<LockDoorPayload>
{
    public LockDoorCommand(LockDoorPayload payload)
        : base("LockDoor", payload)
    {
    }
}

public class LockDoorCommandHandler : IRequestHandler<LockDoorCommand>
{
    private readonly IEventStore _eventStore;
    private readonly IOfficeFactory _officeFactory;

    public LockDoorCommandHandler(IEventStore eventStore, IOfficeFactory officeFactory)
    {
        _eventStore = eventStore;
        _officeFactory = officeFactory;
    }

    public async Task Handle(LockDoorCommand command, CancellationToken cancellationToken)
    {
        var officeEvents = await _eventStore.GetEvents(command.Payload.OfficeId);
        var office = _officeFactory.Rehydrate(officeEvents);

        if (office is not null)
        {
            office.LockDoor(command.Payload.DoorId);

            foreach (var @event in office.GetChanges())
            {
                await _eventStore.SaveEvent(command.Payload.OfficeId, @event);
            }

            office.ClearChanges();   
        }
    }
}