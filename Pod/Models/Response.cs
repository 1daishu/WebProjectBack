namespace Pod.Models
{
    public class Response
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message {  get; set; }

        public int UserId {  get; set; }
        public User User { get; set; }
    }
}
