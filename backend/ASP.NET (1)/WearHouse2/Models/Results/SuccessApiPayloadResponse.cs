namespace WearHouse.Models.Results
{
    public class SuccessApiPayloadResponse<T>
    {
        public int StatusCode { get; set; }
        public string RequestMethod { get; set; }
        public string Message { get; set; }
        public T Payload { get; set; }

    }
}
