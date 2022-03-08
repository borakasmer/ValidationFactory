namespace ValidationFactory.Validators
{
    public record DefaultValidator<T> : IValidator<T>
    {
        public List<(bool, Exception)> Validate(T value,int? type, int? param2, string source,System.Reflection.PropertyInfo pi,object model)
        {
            return new List<(bool, Exception)>();
        }
    }
}
