using MongoDB.Bson.Serialization;

using Fictichos.Constructora.Model;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Utilities.MongoDB
{
    /// <summary>
    /// Contains mapping methods to be called during Server initialization.
    /// </summary>
    public class EntityMapper
    {
        /// <summary>
        /// Driver class for mapping all entities on the database.
        /// </summary>
        public void MapClasses()
        {
            MapBaseEntity();
            MapProject();
            MapMaterial();
            MapMaterialCategory();
            MapFTasks();
            MapAccount();
            MapPayment();
            MapSalary();
            MapPerson();
            MapJob();
            MapEmployee();
            MapSchedule();
            MapEducation();
            MapGrade();
            MapUser();
            MapContact();
            MapAddress();
            MapCoordinates();
        }

        public void MapBaseEntity()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(BaseEntity)))
            {
                BsonClassMap.RegisterClassMap<BaseEntity>(classMap => {
                    classMap.MapMember(p => p.Id).SetElementName("_id");
                    classMap.MapMember(p => p.CreatedAt).SetElementName("created");
                    classMap.MapMember(p => p.ModifiedAt).SetElementName("modified");
                });
            }
        }

        public void MapProject()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Project)))
            {
                BsonClassMap.RegisterClassMap<Project>(classMap => {
                    classMap.MapMember(p => p.Responsible).SetElementName("responsible");
                    classMap.MapMember(p => p.PayHistory).SetElementName("payments");
                    classMap.MapMember(p => p.Tasks).SetElementName("tasks");
                });
            }
        }

        public void MapMaterial()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Material)))
            {
                BsonClassMap.RegisterClassMap<Material>(classMap => {
                    classMap.MapMember(p => p.Quantity).SetElementName("qty");
                    classMap.MapMember(p => p.Owner).SetElementName("owner");
                    classMap.MapMember(p => p.Handler).SetElementName("handler");
                    classMap.MapMember(p => p.Location).SetElementName("location");
                    classMap.MapMember(p => p.Status).SetElementName("status");
                    classMap.MapMember(p => p.BoughtFor).SetElementName("price");
                    classMap.MapMember(p => p.Depreciation).SetElementName("depreciation");
                    classMap.MapMember(p => p.Provider).SetElementName("provider");
                });
            }
        }

        public void MapMaterialCategory()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(MaterialCategory)))
            {
                BsonClassMap.RegisterClassMap<MaterialCategory>(classMap => {
                    classMap.MapMember(p => p.Parent).SetElementName("parent");
                    classMap.MapMember(p => p.SubCategory).SetElementName("subcategory");
                    classMap.MapMember(p => p.Children).SetElementName("children");
                });
            }
        }

        public void MapFTasks()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(FTasks)))
            {
                BsonClassMap.RegisterClassMap<FTasks>(classMap => {
                    classMap.MapMember(p => p.StartDate).SetElementName("starts");
                    classMap.MapMember(p => p.Closed).SetElementName("closed");
                    classMap.MapMember(p => p.Parent).SetElementName("parent");
                    classMap.MapMember(p => p.Subtasks).SetElementName("subtasks");
                    classMap.MapMember(p => p.EmployeesAssigned).SetElementName("employees");
                    classMap.MapMember(p => p.Material).SetElementName("Material");
                    classMap.MapMember(p => p.Address).SetElementName("address");
                });
            }
        }

        public void MapAccount()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Account)))
            {
                BsonClassMap.RegisterClassMap<Account>(classMap => {
                    classMap.MapMember(p => p.Payments).SetElementName("payments");
                    classMap.MapMember(p => p.Owner).SetElementName("owner");
                });
            }
        }

        public void MapPayment()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Payment)))
            {
                BsonClassMap.RegisterClassMap<Payment>(classMap => {
                    classMap.MapMember(p => p.Amount).SetElementName("amount");
                    classMap.MapMember(p => p.Complete).SetElementName("complete");
                });
            }
        }

        public void MapSalary()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Salary)))
            {
                BsonClassMap.RegisterClassMap<Salary>(classMap => {
                    classMap.MapMember(p => p.Reductions).SetElementName("reductions");
                    classMap.MapMember(p => p.Rate).SetElementName("rate");
                    classMap.MapMember(p => p.PayPeriod).SetElementName("payPeriod");
                    classMap.MapMember(p => p.HoursWeek).SetElementName("hoursWeek");
                });
            }
        }

        public void MapPerson()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Person)))
            {
                BsonClassMap.RegisterClassMap<Person>(classMap => {
                    classMap.MapMember(p => p.LastName).SetElementName("lastName");
                    classMap.MapMember(p => p.Contacts).SetElementName("contacts");
                    classMap.MapMember(p => p.Employed).SetElementName("employed");
                });
            }
        }

        public void MapJob()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Job)))
            {
                BsonClassMap.RegisterClassMap<Job>(classMap => {
                    classMap.MapMember(p => p.SalaryHistory).SetElementName("salaryHistory");
                    classMap.MapMember(p => p.Role).SetElementName("role");
                    classMap.MapMember(p => p.Area).SetElementName("area");
                    classMap.MapMember(p => p.Responsible).SetElementName("responsible");
                    classMap.MapMember(p => p.Material).SetElementName("material");
                    classMap.MapMember(p => p.Parent).SetElementName("parent");
                    classMap.MapMember(p => p.Responsibilities).SetElementName("responsibilities");
                });
            }
        }

        public void MapEmployee()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Employee)))
            {
                BsonClassMap.RegisterClassMap<Employee>(classMap => {
                    classMap.MapMember(p => p.Active).SetElementName("active");
                    classMap.MapMember(p => p.DOB).SetElementName("DOB");
                    classMap.MapMember(p => p.RFC).SetElementName("RFC");
                    classMap.MapMember(p => p.CURP).SetElementName("CURP");
                    classMap.MapMember(p => p.Documents).SetElementName("docs");
                    classMap.MapMember(p => p.InternalKey).SetElementName("key");
                    classMap.MapMember(p => p.Charges).SetElementName("charges");
                    classMap.MapMember(p => p.ScheduleHistory).SetElementName("scheduleHistory");
                });
            }
        }

        public void MapSchedule()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Schedule)))
            {
                BsonClassMap.RegisterClassMap<Schedule>(classMap => {
                    classMap.MapMember(p => p.Hours).SetElementName("hours");
                    classMap.MapMember(p => p.Location).SetElementName("location");
                });
            }
        }

        public void MapEducation()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Education)))
            {
                BsonClassMap.RegisterClassMap<Education>(classMap => {
                    classMap.MapMember(p => p.Grades).SetElementName("grades");
                    classMap.MapMember(p => p.Certifications).SetElementName("Certifications");
                });
            }
        }

        public void MapGrade()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Grade)))
            {
                BsonClassMap.RegisterClassMap<Grade>(classMap => {
                    classMap.MapMember(p => p.SchoolGrade).SetElementName("grade");
                    classMap.MapMember(p => p.Overseas).SetElementName("overseas");
                });
            }
        }

        public void MapUser()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(User)))
            {
                BsonClassMap.RegisterClassMap<User>(classMap => {
                    classMap.MapMember(p => p.Password).SetElementName("password");
                    classMap.MapMember(p => p.Avatar).SetElementName("avatar");
                    classMap.MapMember(p => p.Active).SetElementName("active");
                    classMap.MapMember(p => p.Email).SetElementName("email");
                    classMap.MapMember(p => p.Credentials).SetElementName("credentials");
                });
            }
        }

        public void MapContact()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Contact)))
            {
                BsonClassMap.RegisterClassMap<Contact>(classMap => {
                    classMap.MapMember(p => p.Addresses).SetElementName("addresses");
                    classMap.MapMember(p => p.Phones).SetElementName("phones");
                    classMap.MapMember(p => p.Emails).SetElementName("emails");
                });
            }
        }

        public void MapAddress()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Address)))
            {
                BsonClassMap.RegisterClassMap<Address>(classMap => {
                    classMap.MapMember(p => p.Street).SetElementName("street");
                    classMap.MapMember(p => p.Number).SetElementName("number");
                    classMap.MapMember(p => p.Colony).SetElementName("colony");
                    classMap.MapMember(p => p.PostalCode).SetElementName("postalCode");
                    classMap.MapMember(p => p.City).SetElementName("city");
                    classMap.MapMember(p => p.Country).SetElementName("country");
                    classMap.MapMember(p => p.Coordinates).SetElementName("coor");
                });
            }
        }

        public void MapCoordinates()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Coordinates)))
            {
                BsonClassMap.RegisterClassMap<Coordinates>(classMap => {
                    classMap.MapMember(p => p.Latitude).SetElementName("latitude");
                    classMap.MapMember(p => p.Longitude).SetElementName("longitude");
                });
            }
        }
    }
}