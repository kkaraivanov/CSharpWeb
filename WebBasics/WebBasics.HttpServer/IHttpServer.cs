namespace WebBasics.HttpServer
{
    using System.Threading.Tasks;

    public interface IHttpServer
    {
        Task Run(int port, string ipAddress);
    }
}