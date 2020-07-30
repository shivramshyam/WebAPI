using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using DemoAPI;
public class ProviderTest:IDisposable
{
    private TestServer server;
    public HttpClient client{get;set;}
    public ProviderTest()
    {
         server= new TestServer(new WebHostBuilder().UseStartup<Startup>());
       client= server.CreateClient();
    }    
    public void Dispose()
    {
       server.Dispose();
       client.Dispose(); 
    }
}