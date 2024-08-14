namespace PostgreSQL.Domain;

public sealed class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Address? Address { get; set; }
    
    // Additional Fields
    public string MiddleName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    public string ProfilePictureUrl { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new List<string>();
    public string Occupation { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public string MaritalStatus { get; set; } = string.Empty;
    public List<Guid> FriendIds { get; set; } = new List<Guid>();
    public string SocialSecurityNumber { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public List<string> Interests { get; set; } = new List<string>();
    public DateTime LastLoginAt { get; set; }
}

public sealed class Address
{
    public string Address1 { get; set; } = string.Empty;
    public string? Address2 { get; set; }
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public Geolocation? AdditionalDetails { get; set; }
}

public sealed class Geolocation
{
    public double Lat { get; set; }
    public double Lng { get; set; }
}