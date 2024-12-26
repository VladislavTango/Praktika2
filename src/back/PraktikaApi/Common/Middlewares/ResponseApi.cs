namespace Common.Middlewares
{
    public class ResponseApi<T>
    {
        public T Response { get; set; }
        public bool Successed { get; set; }

        public ResponseApi(T response, bool result)
        {
            Response = response;
            Successed = result;
        }

        public static ResponseApi<string> Fail(string message)
        {
            return new ResponseApi<string>(message, false);
        }
        public static ResponseApi<T> Success(T response)
        {
            return new ResponseApi<T>(response, true);
        }
    }
}