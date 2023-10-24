namespace WearHouse.Models.Results
{
    public class ErrorApiResponse
    {
        public int StatusCode { get; set; }
        public string RequestMethod { get; set; }
        public string Message { get; set; }
    }
}
