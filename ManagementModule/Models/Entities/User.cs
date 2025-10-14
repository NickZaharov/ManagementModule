namespace ManagementModule.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public List<UserRole> UserRoles { get; set; }  = new();
    }
}
