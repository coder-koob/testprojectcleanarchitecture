using Application.Common.Security;
using Application.Doors.Models;
using Application.Doors.ReadModels;
using Domain.Common.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Doors.Queries;

[Authorize(Scope = Config.ReadHistoryScope)]
public class GetDoorHistoryQuery : IRequest<DoorHistoryDto>
{
    public GetDoorHistoryQuery(Guid doorId)
    {
        DoorId = doorId;
    }

    public Guid DoorId { get; set; }
}

public class GetDoorHistoryQueryHandler : IRequestHandler<GetDoorHistoryQuery, DoorHistoryDto>
{
    private readonly IReadModelService<DoorHistoryReadModel> _doorHistoryReadModelService;

    public GetDoorHistoryQueryHandler(IReadModelService<DoorHistoryReadModel> doorHistoryReadModelService)
    {
        _doorHistoryReadModelService = doorHistoryReadModelService;
    }

    public async Task<DoorHistoryDto> Handle(GetDoorHistoryQuery request, CancellationToken cancellationToken)
    {
        var readModel = await _doorHistoryReadModelService.GetByIdAsync(request.DoorId.ToString()) ?? throw new NotFoundException(nameof(DoorHistoryReadModel), request.DoorId);
        
        var response = new DoorHistoryDto(readModel.OfficeId, readModel.DoorId, readModel.DoorHistory, readModel.Timestamp);

        return response;
    }
}