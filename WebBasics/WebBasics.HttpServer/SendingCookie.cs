namespace WebBasics.HttpServer
{
    using System.Text;

    public class SendingCookie : Cookie
    {
        public SendingCookie(string name, string value) 
            : base(name, value)
        {
            Url = "/";
        }

        public SendingCookie(string cookie) 
            : base(cookie)
        {
        }

        public int MaxAge { get; set; }

        public bool HttpOnly { get; set; }

        public string Url { get; set; }

        public override string ToString()
        {
            StringBuilder cookieBuilder = new StringBuilder();
            cookieBuilder.Append($"{Name}={Value}; Path={Url};");
            if (MaxAge != 0)
            {
                cookieBuilder.Append($" Max-Age={MaxAge};");
            }

            if (HttpOnly)
            {
                cookieBuilder.Append(" HttpOnly;");
            }

            return cookieBuilder.ToString();
        }
    }
}