using ValidationFactory.Attributes;

namespace ValidationFactory.Models
{
    public class User
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        [StringData(max = 10,min =3)]
        public string UserName { get; set; }
        [HashData]
        public string Password { get; set; }
        [DateData(minYear =1900)]
        public DateTime BirthDate { get; set; }
        [EmailData(type = EmailValidateType.Syntax)]
        [EncryptData]                   
        public string Email { get; set; }
        [EmailData(type = EmailValidateType.Gmail)]
        public string Email2 { get; set; }
        [EncryptData]
        public string Gsm { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
