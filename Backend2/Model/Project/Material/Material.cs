using MongoDB.Bson.Serialization.Attributes;

using Fitichos.Constructora.Repository;
using Fitichos.Constructora.Dto;

namespace Fictichos.Constructora.Model
{
    public class Material : Entity
    {
        [BsonElement("qty")]
        public int Quantity { get; set; }
        [BsonElement("owner")]
        public string Owner { get; set; }
        [BsonElement("empResponsible")]
        public string EmpResponsible { get; set; }
        [BsonElement("brand")]
        public string Brand { get; set; }
        [BsonElement("location")]
        public Address Location { get; set; }
        [BsonElement("status")]
        public int? Status { get; set; }
        [BsonElement("price")]
        public double BoughtFor { get; set; }
        [BsonElement("currentPrice")]
        public double CurrentPrice { get; set; }
        [BsonElement("provider")]
        public string Provider { get; set; }

        public Material(NewMaterialDto data) : base(data.Name, null)
        {
            Quantity = data.Quantity;
            Location = new(data.Location);
            if (data.Status is not null) Status = data.Status;
            BoughtFor = data.BoughtFor;
            Brand = data.Brand;
            Provider = data.Provider;
            Owner = data.Owner;
            EmpResponsible = data.EmpResponsible;
            CurrentPrice = data.CurrentPrice;
        }

        public void Update(UpdatedMaterialDto data)
        {
            Quantity = data.Quantity ?? Quantity;
            Status = data.Status ?? Status;
            BoughtFor = data.BoughtFor ?? BoughtFor;
            Closed = data.Closed ?? Closed;
            Brand = data.Brand ?? Brand;
            Provider = data.Provider ?? Provider;
            Owner = data.Owner ?? Owner;
            EmpResponsible = data.EmpResponsible ?? EmpResponsible;
            CurrentPrice = data.CurrentPrice ?? CurrentPrice;
            /*
            if (data.Quantity is not null) Quantity = (int)data.Quantity;
            if (data.Status is not null) Status = data.Status;
            if (data.BoughtFor is not null) BoughtFor = (double)data.BoughtFor;
            if (data.Closed is not null) Closed = data.Closed;
            if (data.Brand is not null) Brand = data.Brand;
            if (data.Provider is not null) Provider = data.Provider;
            if (data.Owner is not null) Owner = data.Owner;
            */
        }
    }
}