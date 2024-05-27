using BlazorAppC2Corridor.Data;

namespace BlazorAppC2Corridor.Views
{
    public class UserSummary
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Name => $"{FirstName} {LastName}";
        public UserSummary(ApplicationUser user)
        {
            Id = user.Id;
            Email = user.Email;
            FirstName = user.Firstname;
            LastName = user.Lastname;
        }
    }
}
