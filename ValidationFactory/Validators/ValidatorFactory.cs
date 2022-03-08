using ValidationFactory.Attributes;

namespace ValidationFactory.Validators
{
    public static class ValidatorFactory<T>
    {
        //attribute.Name
        static Dictionary<string, IValidator<T>> validatorList = new();
        public static IValidator<T> GetValidator(Type attribute)
        {            
            if (validatorList.Count == 0)
            {
                validatorList.Add("DateData", new DateValidator<T>());
                validatorList.Add("EmailData", new EmailValidator<T>());
                validatorList.Add("EncryptData", new EncryptValidator<T>());            
                validatorList.Add("HashData", new HashValidator<T>());
                validatorList.Add("StringData", new StringValidator<T>());
                validatorList.Add("Default", new DefaultValidator<T>());
            }
            return validatorList.ContainsKey(attribute.Name) ? validatorList[attribute.Name] : validatorList["Default"];
            /*
            switch (attribute)
            {
                case Type when typeof(EmailData).IsAssignableFrom(attribute):
                    {
                        if (!validatorList.ContainsKey("Email"))
                            validatorList.Add("Email", new EmailValidator<T>());
                        return validatorList["Email"];
                    }
                case Type when typeof(StringData).IsAssignableFrom(attribute):
                    {
                        if (!validatorList.ContainsKey("String"))
                            validatorList.Add("String", new StringValidator<T>());
                        return validatorList["String"];
                    }
                case Type when typeof(DateData).IsAssignableFrom(attribute):
                    {
                        if (!validatorList.ContainsKey("Date"))
                            validatorList.Add("Date", new DateValidator<T>());
                        return validatorList["Date"];
                    }
                case Type when typeof(EncryptData).IsAssignableFrom(attribute):
                    {
                        if (!validatorList.ContainsKey("Encrypt"))
                            validatorList.Add("Encrypt", new EncryptValidator<T>());
                        return validatorList["Encrypt"];
                    }
                case Type when typeof(HashData).IsAssignableFrom(attribute):
                    {
                        if (!validatorList.ContainsKey("Hash"))
                            validatorList.Add("Hash", new HashValidator<T>());
                        return validatorList["Hash"];
                    }
                default:
                    {
                        if (!validatorList.ContainsKey("Default"))
                            validatorList.Add("Default", new DefaultValidator<T>());
                        return validatorList["Default"];
                    }
            }
            */
        }
    }
}
