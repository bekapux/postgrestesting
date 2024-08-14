using PostgreSQL.Domain;
using Bogus;

namespace PostgreSQL;
public static class DataGenerator
{
    private static readonly string[] Roles = ["Admin", "User", "Moderator"];
    private static readonly string[] Hobbies = ["Ski", "Music", "Animals", "Travel", "Guitar", "Programming", "Gaming", "Collecting"];

    public static List<User> GenerateUsers(int numberOfUsers)
    {
        var addressFaker = new Faker<Address>()
            .RuleFor(a => a.Address1, f => f.Address.StreetAddress())
            .RuleFor(a => a.Address2, f => f.Address.SecondaryAddress())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.PostalCode, f => f.Address.ZipCode())
            .RuleFor(a => a.AdditionalDetails, f => new Geolocation
            {
                Lat = f.Address.Latitude(),
                Lng = f.Address.Longitude()
            });

        var userFaker = new Faker<User>()
            .RuleFor(u => u.Id, f => Guid.NewGuid())
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.MiddleName, f => f.Name.FirstName())
            .RuleFor(u => u.DateOfBirth, f => f.Date.Past(30, DateTime.UtcNow.AddYears(-18).ToUniversalTime()))
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.PasswordHash, f => f.Internet.Password())
            .RuleFor(u => u.CreatedAt, f => f.Date.Past(2).ToUniversalTime())
            .RuleFor(u => u.UpdatedAt, f => f.Date.Recent().ToUniversalTime())
            .RuleFor(u => u.IsActive, f => f.Random.Bool())
            .RuleFor(u => u.ProfilePictureUrl, f => f.Internet.Avatar())
            .RuleFor(u => u.Bio, f => f.Lorem.Sentence())
            .RuleFor(u => u.Occupation, f => f.Name.JobTitle())
            .RuleFor(u => u.CompanyName, f => f.Company.CompanyName())
            .RuleFor(u => u.Nationality, f => f.Address.Country())
            .RuleFor(u => u.FriendIds, f => f.Make(5, () => Guid.NewGuid()))
            .RuleFor(u => u.SocialSecurityNumber, f => f.Random.Replace("###-##-####"))
            .RuleFor(u => u.PassportNumber, f => f.Random.Replace("########"))
            .RuleFor(u => u.LastLoginAt, f => f.Date.Recent().ToUniversalTime())
            .RuleFor(u => u.Roles, f => f.Make(2, () => f.PickRandom(Roles)))
            .RuleFor(u => u.Interests, f => f.Make(3, () => f.PickRandom(Hobbies)))
            .RuleFor(u => u.Address, f => addressFaker.Generate());

        return userFaker.Generate(numberOfUsers);
    }
}