namespace CodingTestApi.Models
{
    public class APIResponseModel<T> where T : class
    {
        public int StatusCode { get; set; }
        public T Data { get; set; }
    }
}
