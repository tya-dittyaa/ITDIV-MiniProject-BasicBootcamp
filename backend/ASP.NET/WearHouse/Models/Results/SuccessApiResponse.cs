namespace WearHouse.Models.Results
{
    public class SuccessApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string RequestMethod { get; set; }
        public T Payload { get; set; }

    }
}
