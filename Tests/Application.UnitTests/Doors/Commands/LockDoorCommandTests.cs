using Application.Common.Services;
using Application.Doors.Commands;
using AutoFixture;
using Domain.Common.Exceptions;
using Domain.Doors;
using Domain.Doors.Commands;
using Domain.Interfaces;
using Domain.Offices;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using Xunit;

namespace Application.UnitTests.Doors.Commands;

public class LockDoorCommandTests
{
    private readonly Fixture _autoFixture = new();

    [Fact]
    public async Task Should_Lock_Door_If_Client_Is_Authorized()
    {
        // Arrange
        var officeId = _autoFixture.Create<Guid>();
        var officeName = _autoFixture.Create<string>();

        var doorId = _autoFixture.Create<Guid>();
        var doorName = _autoFixture.Create<string>();
        var scope = _autoFixture.Create<string>();

        var door = new Door(officeId, doorId, doorName, scope);

        var office = new Office(officeId, officeName, new List<Door> { door });

        var command = new LockDoorCommand(new LockDoorPayload(officeId, doorId));

        var eventSourceRepoMock = new Mock<IEventSourcedRepository<Office>>();
        eventSourceRepoMock.Setup(x => x.GetByIdAsync(officeId)).ReturnsAsync(office);

        var clientServiceMock = new Mock<IClientService>();
        clientServiceMock.Setup(x => x.IsClientAuthorized(It.IsAny<string>(), It.IsAny<string[]>())).Returns(true);

        var currentUserServiceMock = new Mock<ICurrentUserService>();

        var handler = new LockDoorCommandHandler(eventSourceRepoMock.Object, clientServiceMock.Object, currentUserServiceMock.Object);

        // Act
        await handler.Handle(command, new CancellationToken());

        // Assert
        using (new AssertionScope())
        {
            eventSourceRepoMock.Verify(x => x.SaveAsync(office), Times.Once);
        }
    }

    [Fact]
    public async Task Should_Throw_NotFoundException_When_Office_Cannot_Be_Found()
    {
        // Arrange
        var officeId = _autoFixture.Create<Guid>();
        var officeName = _autoFixture.Create<string>();

        var doorId = _autoFixture.Create<Guid>();
        var doorName = _autoFixture.Create<string>();
        var scope = _autoFixture.Create<string>();

        var command = new LockDoorCommand(new LockDoorPayload(officeId, doorId));

        var eventSourceRepoMock = new Mock<IEventSourcedRepository<Office>>();

        var clientServiceMock = new Mock<IClientService>();
        var currentUserServiceMock = new Mock<ICurrentUserService>();

        var handler = new LockDoorCommandHandler(eventSourceRepoMock.Object, clientServiceMock.Object, currentUserServiceMock.Object);

        // Act
        var act = async () => await handler.Handle(command, new CancellationToken());

        // Assert
        using (new AssertionScope())
        {
            await act.Should().ThrowAsync<NotFoundException>();

            eventSourceRepoMock.Verify(x => x.SaveAsync(It.IsAny<Office>()), Times.Never);
        }
    }

    [Fact]
    public async Task Should_Throw_ForbiddenAccessException_When_Client_Not_Authorized()
    {
        // Arrange
        var officeId = _autoFixture.Create<Guid>();
        var officeName = _autoFixture.Create<string>();

        var doorId = _autoFixture.Create<Guid>();
        var doorName = _autoFixture.Create<string>();
        var scope = _autoFixture.Create<string>();

        var door = new Door(officeId, doorId, doorName, scope);

        var office = new Office(officeId, officeName, new List<Door> { door });

        var command = new LockDoorCommand(new LockDoorPayload(officeId, doorId));

        var eventSourceRepoMock = new Mock<IEventSourcedRepository<Office>>();
        eventSourceRepoMock.Setup(x => x.GetByIdAsync(officeId)).ReturnsAsync(office);

        var clientServiceMock = new Mock<IClientService>();
        clientServiceMock.Setup(x => x.IsClientAuthorized(It.IsAny<string>(), It.IsAny<string[]>())).Returns(false);

        var currentUserServiceMock = new Mock<ICurrentUserService>();

        var handler = new LockDoorCommandHandler(eventSourceRepoMock.Object, clientServiceMock.Object, currentUserServiceMock.Object);

        // Act
        var act = async () => await handler.Handle(command, new CancellationToken());

        // Assert
        using (new AssertionScope())
        {
            await act.Should().ThrowAsync<ForbiddenAccessException>();

            eventSourceRepoMock.Verify(x => x.SaveAsync(It.IsAny<Office>()), Times.Never);
        }
    }
}