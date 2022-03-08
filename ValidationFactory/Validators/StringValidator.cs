namespace ValidationFactory.Validators
{
    public record StringValidator<T>() : Validator, IValidator<T>
    {
        public List<(bool, Exception)> Validate(T value, int? max, int? min, string source, System.Reflection.PropertyInfo pi, object model)
        {
            var errorList = new List<(bool, Exception)>();
            if (!typeof(T).IsValueType && typeof(T) != typeof(String))
            {
                throw new ArgumentException("T must be a value type or System.String.");
            }
            string stringValue = value.ToString();
            //if (!IsGetMethod() && !IsEncryted(stringValue))
            if (!IsEncryted(stringValue))
            {
                List<string> invalidChars = new List<string>() { "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-" };
                // Check for length
                if (stringValue.Length > max)
                {
                    Console.WriteLine("String too Long");
                    errorList.Add((false, new Exception($"String is too Long. Text Must Shorter Than < {max}") { Source = source }));
                }
                else if (stringValue.Length < min)
                {
                    Console.WriteLine("String too Short");
                    errorList.Add((false, new Exception($"String is too Short. Text Must Longer Than >= {min}") { Source = source }));
                }
                if (!(!stringValue.Equals(stringValue.ToLower())))
                {
                    //Check for min 1 uppercase
                    Console.WriteLine("Requres at least one uppercase");
                    errorList.Add((false, new Exception("Requres at least one uppercase") { Source = source }));
                }
                //Iterate your list of invalids and check if input has one
                foreach (string s in invalidChars)
                {
                    if (stringValue.Contains(s))
                    {
                        Console.WriteLine("String contains invalid character: " + s);
                        Exception exception = new Exception("String contains invalid character: " + s) { Source = source };
                        errorList.Add((false, exception));
                        //pi.SetValue(model, "Bora Kasmer");
                        break;
                    }
                }
                if (errorList.Count == 0) Console.WriteLine("All tests succesful");
            }
            return errorList;
        }
    }
}
