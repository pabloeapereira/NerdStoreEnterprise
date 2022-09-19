namespace NSE.Core.Comunication
{
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