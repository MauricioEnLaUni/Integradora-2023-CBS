using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Dto;
using Newtonsoft.Json;

namespace Fictichos.Constructora.Model;

internal class Record : BaseEntity,
    IQueryMask<Record, NewRecordDto, UpdatedRecordDto>
{
    internal string Event { get; set; } = string.Empty;
    internal string Description { get; set; } = string.Empty;
    internal DateTime Starts { get; set; }
    internal DateTime Ends { get; set; }

    public Record() { }
    private Record(NewRecordDto data)
    {
        Event = data.Event;
        Description = data.Description;
        Starts = data.Starts;
        Ends = data.Ends;
    }
    
    public Record Instantiate(NewRecordDto data)
    {
        return new(data);
    }

    public RecordDto ToDto()
    {
        return new()
        {
            Id = Id,
            Event = Event,
            Description = Description,
            Starts = Starts,
            Ends = Ends
        };
    }

    public string Serialize()
    {
        RecordDto data = ToDto();
        return JsonConvert.SerializeObject(data);
    }

    public void Update(UpdatedRecordDto data)
    {
        
    }
}