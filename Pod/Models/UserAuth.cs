namespace Pod.Models
{
    public class UserAuth
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public User User { get; set; }
    }
}
