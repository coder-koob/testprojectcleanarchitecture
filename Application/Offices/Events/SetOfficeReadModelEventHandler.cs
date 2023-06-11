using Application.Offices.ReadModels;
using Domain.Interfaces;
using Domain.Offices.Events;
using MediatR;

namespace Application.Offices.Events;

public class SetOfficeReadModelEventHandler : INotificationHandler<OfficeCreatedEvent>
{
    private readonly IReadModelService<OfficeReadModel> _officeReadModelService;

    public SetOfficeReadModelEventHandler(IReadModelService<OfficeReadModel> officeReadModelService)
    {
        _officeReadModelService = officeReadModelService;
    }

    public async Task Handle(OfficeCreatedEvent notification, CancellationToken cancellationToken)
    {
        var readModel = await _officeReadModelService.GetByIdAsync(notification.OfficeId.ToString());

        readModel ??= new OfficeReadModel
        {
            OfficeId = notification.OfficeId,
            Name = notification.Name,
        };

        readModel.Timestamp = notification.Timestamp;

        await _officeReadModelService.SaveAsync(readModel, notification.OfficeId.ToString());
    }
}