namespace ValidationFactory.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class DateData : System.Attribute
    {
        public DateData()
        {
        }
        public int minYear{ get; set; }
    }
}
