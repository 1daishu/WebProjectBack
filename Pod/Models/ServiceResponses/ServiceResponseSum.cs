using Pod.Models.Enums;

namespace Pod.Models.ServiceResponses
{
    public class ServiceResponseSum<T>
    {
        public T Data { get; set; }
        public string Description { get; set; }
        public StatusCode StatusCode { get; set; }
        public int TotalSum { get; set; }
    }
}
