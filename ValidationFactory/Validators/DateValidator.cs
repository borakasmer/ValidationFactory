namespace ValidationFactory.Validators
{
    public record DateValidator<T>() : Validator, IValidator<T>
    {
        public List<(bool, Exception)> Validate(T value, int? minYear,int? param2, string source, System.Reflection.PropertyInfo pi,object model)
        {            
            var errorList = new List<(bool, Exception)>();
            if (!DateTime.TryParse(value.ToString(), out DateTime temp))
            {
                throw new ArgumentException("T must be proper System.DateTime.");
            }
            string stringValue = value.ToString();
            //if (!IsGetMethod() && !IsEncryted(stringValue))
            if (!IsEncryted(stringValue))
            {
                // Check minYear
                if ((DateTime.Parse(stringValue)).Year < minYear)
                {
                    Console.WriteLine("Time is too old. Wrong Date!");
                    errorList.Add((false, new Exception($"Time is too old. Wrong Date! Year Must Bigger Than > {minYear}") { Source = source }));
                }
                if (errorList.Count == 0) Console.WriteLine("All tests succesful");
            }
            return errorList;
        }
    }
}
