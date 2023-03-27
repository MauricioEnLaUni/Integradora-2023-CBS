using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using MongoDB.Driver;

namespace Fictichos.Constructora.Repository
{
    public class UserService
        : BaseRepositoryService<User, NewUserDto, UpdatedUserDto>
    {
        public IMongoCollection<EmailContainer> EmailCollection { get; }
        public UserService(MongoSettings container)
            : base(container, "cbs", "users")
        {
            EmailCollection = container.Client.GetDatabase("cbs")
                .GetCollection<EmailContainer>("emails");
        }

        public async void ValidateEmail(
        List<UpdateList<string>> data,
        User usr)
        {
            foreach (var email in data)
            {
                if (email.Operation == 1)
                {
                    if (email.Key >= usr.Email.Count) data.Remove(email);
                }
                else
                {
                    if (email.NewItem is null ||
                        email.NewItem == string.Empty ||
                        !email.NewItem.IsEmailFormatted())
                    {
                        data.Remove(email);
                        continue;
                    }
                    var emailFilter = Builders<EmailContainer>.Filter
                        .Eq(x => x.value, email.NewItem);
                    bool emailIsTaken = await EmailCollection.Find(emailFilter)
                        .SingleOrDefaultAsync() is null ? false : true;
                    if (emailIsTaken) data.Remove(email);
                }
            }
        }
    }
}