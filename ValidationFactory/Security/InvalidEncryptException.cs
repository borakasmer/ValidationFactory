namespace ValidationFactory.Security
{ 
    public class InvalidEncryptException : Exception
    {
        public string _errorCode = "430";
        public override string Message
        {
            get
            {
                return "Invalid Token Exeception";
            }
        }

        public string ErrorCode
        {
            get
            {
                return _errorCode;
            }
        }
    }
}
