using Domain.Common;

namespace Application.ReadModels;

public class OfficeReadModel : ReadModel
{
    public OfficeReadModel()
        : base("Office")
    {
    }

    public override int Version => 1;
}