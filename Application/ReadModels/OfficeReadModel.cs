using Application.Doors.Models;
using Domain.Common;
using Domain.Doors;

namespace Application.ReadModels;

public class OfficeReadModel : ReadModel
{
    public OfficeReadModel()
        : base("Office")
    {
    }

    public override int Version => 1;

    public Guid OfficeId { get; set; }
    public string? Name { get; set; }
    public IList<DoorDto> Doors { get; set; } = new List<DoorDto>();
}