using Application.Doors.Models;
using Domain.Common;

namespace Application.Doors.ReadModels;

public class DoorHistoryReadModel : ReadModel
{
    public DoorHistoryReadModel()
        : base("DoorHistory")
    {
    }

    public override int Version => 1;

    public override DateTimeOffset Timestamp { get; set; }

    public Guid OfficeId { get; set; }

    public Guid DoorId { get; set; }

    public IList<DoorEventDto> DoorHistory { get; set; } = new List<DoorEventDto>();
}