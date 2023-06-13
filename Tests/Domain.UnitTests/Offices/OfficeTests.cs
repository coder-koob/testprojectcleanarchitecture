using Domain.Doors;
using Domain.Doors.Commands;
using Domain.Doors.Events;
using Domain.Offices;
using Domain.Offices.Commands;
using Domain.Offices.Events;
using Xunit;

namespace Domain.UnitTests.Offices;

public class OfficeTests
{
    [Fact]
    public void Office_Create_Should_Create_A_New_Office()
    {
        // Arrange
        var officeId = Guid.NewGuid();

        var command = new CreateOfficeCommand(new CreateOfficePayload("test"));

        // Act
        var office = Office.Create(officeId, command);

        // Assert
        Assert.NotNull(office);

        var changeEvents = office.GetChanges();
        Assert.Single(changeEvents);

        var firstEvent = changeEvents.First();

        Assert.IsType<OfficeCreatedEvent>(firstEvent);

        var officeCreatedEvent = firstEvent as OfficeCreatedEvent;

        Assert.Equal(officeId, officeCreatedEvent!.OfficeId);
        Assert.Equal("test", officeCreatedEvent!.Name);
    }

    [Fact]
    public void Office_AddDoor_Should_Add_A_New_Door()
    {
        // Arrange
        var officeId = Guid.NewGuid();
        var doorId = Guid.NewGuid();

        var office = new Office(officeId, "test-office");

        var command = new AddDoorCommand(new AddDoorPayload(officeId, doorId, "test-door", "test"));

        // Act
        office.AddDoor(command);
        
        // Assert
        Assert.Single(office.Doors);

        var changeEvents = office.GetChanges();
        Assert.Single(changeEvents);

        var firstEvent = changeEvents.First();

        Assert.IsType<DoorAddedEvent>(firstEvent);

        var doorAddedEvent = firstEvent as DoorAddedEvent;

        Assert.Equal(officeId, doorAddedEvent!.OfficeId);
        Assert.Equal("test-door", doorAddedEvent!.Name);
    }

    [Fact]
    public void Office_LockDoor_Should_Lock_The_Door()
    {
        // Arrange
        var officeId = Guid.NewGuid();
        var doorId = Guid.NewGuid();

        var office = new Office(officeId, "test-office", new List<Door> { new Door(officeId, doorId, "test", false, "test") });

        var lockDoorCommand = new LockDoorCommand(new LockDoorPayload(officeId, doorId));

        // Act
        office.LockDoor(lockDoorCommand);
        
        // Assert
        Assert.Single(office.Doors);

        var changeEvents = office.GetChanges();
        Assert.Single(changeEvents);

        var @event = changeEvents.First();

        Assert.IsType<DoorLockedEvent>(@event);

        var doorLockedEvent = @event as DoorLockedEvent;

        Assert.Equal(officeId, doorLockedEvent!.OfficeId);
        Assert.Equal(doorId, doorLockedEvent!.DoorId);
    }

    [Fact]
    public void Office_UnlockDoor_Should_Unlock_The_Door()
    {
        // Arrange
        var officeId = Guid.NewGuid();
        var doorId = Guid.NewGuid();

        var office = new Office(officeId, "test-office", new List<Door> { new Door(officeId, doorId, "test", true, "test") });

        var command = new UnlockDoorCommand(new UnlockDoorPayload(officeId, doorId));

        // Act
        office.UnlockDoor(command);
        
        // Assert
        Assert.Single(office.Doors);

        var changeEvents = office.GetChanges();
        Assert.Single(changeEvents);

        var @event = changeEvents.First();

        Assert.IsType<DoorUnlockedEvent>(@event);

        var doorUnlockedEvent = @event as DoorUnlockedEvent;

        Assert.Equal(officeId, doorUnlockedEvent!.OfficeId);
        Assert.Equal(doorId, doorUnlockedEvent!.DoorId);
    }
}