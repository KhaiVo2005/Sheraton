namespace Sheraton.Models.ViewModel
{
    public class VnpayOptions
    {
        public string TmnCode { get; set; } = "";
        public string HashSecret { get; set; } = "";
        public string BaseUrl { get; set; } = "";
        public string CallbackUrl { get; set; } = "";
    }
}
