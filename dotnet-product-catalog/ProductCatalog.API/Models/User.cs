namespace ProductCatalag.API.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;

    // Password as hashed
    public string PasswordHash { get; set; } = string.Empty;

    // Roles of User (User, Admin etc.)
    public string Role { get; set; }=  "User";
}