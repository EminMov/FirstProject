namespace FirstProject.Models
{
    public class ResponseModel<T>
    {
        public int StatusCode { get; set; }
        public T Data { get; set; }
    }
}
