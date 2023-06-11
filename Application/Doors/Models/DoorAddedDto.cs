namespace Application.Doors.Models;

public class DoorAddedDto
{
    public DoorAddedDto(Guid officeId, Guid doorId)
    {
        OfficeId = officeId;
        DoorId = doorId;
    }

    public Guid OfficeId { get; set; }
    public Guid DoorId { get; set; }
}