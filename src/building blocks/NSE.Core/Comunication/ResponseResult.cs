namespace NSE.Core.Comunication
{
    public class ResponseResult
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorMessage Errors { get; set; }
        public bool IsSuccess => Errors is null || Errors.Mensagens.Any();
    }

    public class ResponseErrorMessage
    {
        public IEnumerable<string> Mensagens { get; set; } = new List<string>();
    }
}