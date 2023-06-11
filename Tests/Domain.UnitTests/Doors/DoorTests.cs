using Domain.Doors;
using Domain.Doors.Commands;
using Domain.Doors.Events;
using Xunit;

namespace Domain.UnitTests.Doors;

public class DoorTests
{
    [Fact]
    public void Door_Lock_Should_Lock_Door()
    {
        // Arrange
        var officeId = Guid.NewGuid();
        var doorId = Guid.NewGuid();

        var door = new Door(officeId, doorId, "test");

        // Act
        door.Lock();
        
        // Assert
        Assert.True(door.IsLocked);
    }

    [Fact]
    public void Door_Unlock_Should_Unlock_Door()
    {
        // Arrange
        var officeId = Guid.NewGuid();
        var doorId = Guid.NewGuid();

        var door = new Door(officeId, doorId, "test");

        // Act
        door.Unlock();
        
        // Assert
        Assert.False(door.IsLocked);
    }

    [Fact]
    public void Door_ApplyEvents_DoorAddedEvent_Should_Set_All_Properties()
    {
        // Arrange
        var officeId = Guid.NewGuid();
        var doorId = Guid.NewGuid();

        var command = new AddDoorCommand(new AddDoorPayload(officeId, doorId, "test"));

        var @event = new DoorAddedEvent(officeId, command);

        var door = new Door();

        // Act
        door.ApplyEvent(@event);
        
        // Assert
        Assert.Equal(officeId, door.OfficeId);
        Assert.Equal(doorId, door.DoorId);
        Assert.Equal("test", door.Name);
        Assert.False(door.IsLocked);
    }

    [Fact]
    public void Door_ApplyEvents_DoorLockedEvent_Should_Lock_Door()
    {
        // Arrange
        var officeId = Guid.NewGuid();
        var doorId = Guid.NewGuid();

        var command = new LockDoorCommand(new LockDoorPayload(officeId, doorId));

        var @event = new DoorLockedEvent(officeId, command);

        var door = new Door();

        // Act
        door.ApplyEvent(@event);
        
        // Assert
        Assert.True(door.IsLocked);
    }
}