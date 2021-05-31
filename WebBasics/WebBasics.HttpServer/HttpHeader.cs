namespace WebBasics.HttpServer
{
    public class HttpHeader
    {
        public HttpHeader(string line)
        {
            var lineSplit = line.Split(": ",2);
            this.Name = lineSplit[0];
            this.Value = lineSplit[1];
        }

        public HttpHeader(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }

        public string Value { get; private set; }

        public override string ToString()
        {
            return $"{Name}: {Value}";
        }
    }
}