using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace GripItemTrade.Api.Tests
{
	public abstract class ApiBaseTest : IDisposable
	{
		protected readonly TestServer server;

		public HttpClient Client { get; }

		public ApiBaseTest()
		{
			server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
			Client = server.CreateClient();
		}

		public void Dispose()
		{
			Client.Dispose();
			server.Dispose();
		}
	}
}
