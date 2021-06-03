namespace WebBasics.HttpServer
{
    using System.Threading.Tasks;

    public interface IHttpServer
    {
        void Run(int port, string ipAddress = null);
    }
}