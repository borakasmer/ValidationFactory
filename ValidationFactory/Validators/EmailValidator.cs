using System.Text.RegularExpressions;
using ValidationFactory.Attributes;

namespace ValidationFactory.Validators
{
    public record EmailValidator<T>() : Validator, IValidator<T>
    {
        public List<(bool, Exception)> Validate(T value, int? validateType, int? param2, string source, System.Reflection.PropertyInfo pi, object model)
        {
            var errorList = new List<(bool, Exception)>();
            if (!typeof(T).IsValueType && typeof(T) != typeof(String))
            {
                throw new ArgumentException("T must be a value type or System.String.");
            }
            string emailValue = value.ToString();
            //if (!IsGetMethod() && !IsEncryted(emailValue))
            if (!IsEncryted(emailValue))
            {
                if (validateType != null)
                {
                    switch ((EmailValidateType)validateType)
                    {
                        case EmailValidateType.Syntax:
                            {
                                Regex mailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                                // Check for length
                                if (!mailRegex.Match(emailValue).Success)
                                {
                                    Console.WriteLine("Email is not correct");
                                    errorList.Add((false, new Exception("Email is not correct") { Source = source }));
                                }
                                break;
                            }
                        case EmailValidateType.Gmail:
                            {
                                Regex mailRegex = new Regex(@"^([\w\.\-]+)@(gmail)\.(com)$");
                                // Check for length
                                if (!mailRegex.Match(emailValue).Success)
                                {
                                    Console.WriteLine("Email is not gmail");
                                    errorList.Add((false, new Exception("Email is not gmail!") { Source = source }));
                                }
                                break;
                            }
                        case EmailValidateType.Government:
                            {
                                Regex mailRegex = new Regex(@"^([\w\.\-]+)@(gov)\.(tr)$");
                                // Check for length
                                if (!mailRegex.Match(emailValue).Success)
                                {
                                    Console.WriteLine("Email is not government email");
                                    errorList.Add((false, new Exception("Email is not government email!") { Source = source }));
                                }
                                break;
                            }
                        case EmailValidateType.Education:
                            {
                                Regex mailRegex = new Regex(@"^([\w\.\-]+)@(edu)\.(tr)$");
                                // Check for length
                                if (!mailRegex.Match(emailValue).Success)
                                {
                                    Console.WriteLine("Email is not educatinal email");
                                    errorList.Add((false, new Exception("Email is not educational email!") { Source = source }));
                                }
                                break;
                            }
                    }
                }
                if (errorList.Count == 0) Console.WriteLine("All tests succesful");
            }
            return errorList;
        }
    }
}
