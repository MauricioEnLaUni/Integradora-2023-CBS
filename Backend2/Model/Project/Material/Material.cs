using MongoDB.Bson.Serialization.Attributes;

using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Model
{
    public class Material : Entity, IMongoMask
    {
        [BsonElement("qty")]
        public int Quantity { get; private set; }
        [BsonElement("owner")]
        public string Owner { get; private set; }
        [BsonElement("handler")]
        public string Handler { get; private set; }
        [BsonElement("location")]
        public Address Location { get; private set; }
        [BsonElement("status")]
        public int? Status { get; private set; }
        [BsonElement("price")]
        public double BoughtFor { get; private set; }
        [BsonElement("currentPrice")]
        public double Depreciation { get; private set; }
        [BsonElement("provider")]
        public string Provider { get; private set; }

        public Material(NewMaterialDto data) : base(data.Name, null)
        {
            Quantity = data.Quantity;
            Location = new(data.Location);
            if (data.Status is not null) Status = data.Status;
            BoughtFor = data.BoughtFor;
            Provider = data.Provider;
            Owner = data.Owner;
            Handler = data.Handler;
            Depreciation = data.Depreciation;
        }

        public void Update(List<dynamic> values)
        {
            List<Action<int>> intActions = new()
            {
                SetQuantity,
                SetStatus
            };
            List<Action<double>> doubleActions = new()
            {
                SetBoughtFor,
                SetDepreciation
            };
            List<Action<string>> stringActions = new()
            {
                SetProvider,
                SetHandler,
                SetOwner
            };

            values.ForEach((e, i) => {
                if (e is not null)
                {
                    if (i < 2) intActions[i](e);
                    if (i < 4) doubleActions[i](e);
                    if (i > 3) stringActions[i](e);
                }
            });
        }

        public CurrentInventoryDto AsInventory()
        {
            return new CurrentInventoryDto()
            {
                Id = this.Id.ToString(),
                Name = this.Name,
                Quantity = this.Quantity
            };
        }

        public MaterialDto AsOverview()
        {
            return new MaterialDto()
            {
                Id = this.Id.ToString(),
                Name = this.Name,
                Quantity = this.Quantity,
                Owner = this.Owner
            };
        }

        private void SetQuantity(int data)
        {
            Quantity = data;
        }
        private void SetStatus(int data)
        {
            Status = data;
        }
        private void SetHandler(string data)
        {
            Handler = data;
        }
        private void SetOwner(string data)
        {
            Owner = data;
        }
        private void SetBoughtFor(double data)
        {
            BoughtFor = data;
        }
        private void SetDepreciation(double data)
        {
            Depreciation = data;
        }
        private void SetProvider(string data)
        {
            Provider = data;
        }
    }
}