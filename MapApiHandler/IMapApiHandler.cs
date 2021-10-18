using blazor_demo.Data;
using System.Net.Http;

namespace blazor_demo.MapApiHandler
{
    public interface IMapApiHandler
    {
        HttpClient Client{ get; set; }
        void Process(PropertyData d);
    }
}