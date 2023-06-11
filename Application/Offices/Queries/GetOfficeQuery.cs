using Application.Offices.Models;
using Application.Offices.ReadModels;
using Domain.Common.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Offices.Queries;

public class GetOfficeQuery : IRequest<OfficeDto>
{
    public Guid OfficeId { get; set; }
}

public class GetOfficeQueryHandler : IRequestHandler<GetOfficeQuery, OfficeDto>
{
    private readonly IReadModelService<OfficeReadModel> _officeReadModelService;

    public GetOfficeQueryHandler(IReadModelService<OfficeReadModel> officeReadModelService)
    {
        _officeReadModelService = officeReadModelService;
    }

    public async Task<OfficeDto> Handle(GetOfficeQuery request, CancellationToken cancellationToken)
    {
        var readModel = await _officeReadModelService.GetByIdAsync(request.OfficeId.ToString());

        if (readModel is null)
        {
            throw new NotFoundException(nameof(OfficeReadModel), request.OfficeId);
        }
        
        var dto = new OfficeDto(readModel.OfficeId, readModel.Name, readModel.Doors);

        return dto;
    }
}