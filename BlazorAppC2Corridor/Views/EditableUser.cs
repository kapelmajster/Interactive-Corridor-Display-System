namespace BlazorAppC2Corridor.Views
{
    public class EditableUser
    {
        public string Id { get; set; }
        public bool Admin { get; set; }
        public bool SmallScreen { get; set; }
        public bool BigScreen { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public bool Enabled { get; set; }
        public string Name
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
