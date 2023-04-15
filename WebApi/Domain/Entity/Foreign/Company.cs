using Newtonsoft.Json;

namespace Fictichos.ERP.Domain;
internal class Company : BaseEntity
{
    internal string Name { get; set; } = string.Empty;
    internal string Activity { get;  set; } = string.Empty;
    internal string Relation { get; private set; } = string.Empty;

    internal void Update()
    {

    }

    internal class UpdatedCompany
    {

    }

    #region Updates
    private void NewName(string data)
    {
        Name = data;
    }

    private void NewActivity(string data)
    {
        Activity = data;
    }

    private void NewRelation(string data)
    {
        Relation = data;
    }
    #endregion Updates
}