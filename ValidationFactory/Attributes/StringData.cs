namespace ValidationFactory.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class StringData : System.Attribute
    {
        public StringData()
        {
        }
        public int max { get; set; }
        public int min { get; set; }
    }
}
