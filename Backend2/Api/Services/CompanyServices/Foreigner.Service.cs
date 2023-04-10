using MongoDB.Driver;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Repository;

public class ExternalPersonService
    : BaseService<ExternalPerson, NewExPersonDto, UpdatedExPersonDto>
{
    private const string MAINCOLLECTION = "companies";

    public ExternalPersonService(MongoSettings container)
        : base(container, MAINCOLLECTION) { }
}
