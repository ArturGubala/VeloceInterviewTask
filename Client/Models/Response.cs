namespace UserSpying.Client.Models
{
    public record Response<T>
    {
        public T Data { get; set; }
        public int? StatusCode { get; set; }
        public string Message { get; set; }
    }
}
