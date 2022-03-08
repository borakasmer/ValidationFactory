namespace ValidationFactory.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class HashData : System.Attribute
    {
        public HashData()
        {
        }
        public int minYear{ get; set; }
    }
}
