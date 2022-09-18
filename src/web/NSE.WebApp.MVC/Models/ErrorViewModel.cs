namespace NSE.WebApp.MVC.Models
{
    public class ErrorViewModel
    {
        public int Code { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class ResponseResult
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorMessage Errors { get; set; }
    }

    public class ResponseErrorMessage
    {
        public IEnumerable<string> Mensagens { get; set; } = new List<string>();
    }
}