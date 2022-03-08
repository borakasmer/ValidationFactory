namespace ValidationFactory.Validators
{
    public record Validator
    {
        public HttpContext _httpContext => new HttpContextAccessor().HttpContext;
        public bool IsEncryted(string value)
        {
            return value.IndexOf("encß") == 0;
        }
        public bool IsHashed(string value)
        {
            return value.IndexOf("hasß") == 0;
        }
        public bool IsGetMethod()
        {
            return _httpContext.Request.Method == "GET";
        }
    }
}
