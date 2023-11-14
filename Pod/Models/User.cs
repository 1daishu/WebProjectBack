namespace Pod.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }



        public int UserAuthId { get; set; }
        public UserAuth UserAuth { get; set; }

        public Cart Cart { get; set; }

        public List<Response> Responses { get; set; }
    }
}
