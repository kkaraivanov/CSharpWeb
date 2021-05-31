namespace WebBasics.HttpServer
{
    public class Cookie
    {
        public Cookie(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public Cookie(string cookie)
        {
            var cookieSplit = cookie.Split("=", 2);
            Name = cookieSplit[0];
            Value = cookieSplit[1];
        }

        public string Name { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Name}={Value}";
        }
    }
}