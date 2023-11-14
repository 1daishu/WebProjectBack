using Pod.Models.Enums;

namespace Pod.Models.ServiceResponses
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public string Description { get; set; }
        public StatusCode StatusCode { get;set; }
    }
}
