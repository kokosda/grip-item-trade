using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.IO;
using System.Net.Http;

namespace GripItemTrade.Api.Tests
{
	public abstract class ApiBaseTest : IDisposable
	{
		protected TestServer server;

		public HttpClient Client { get; private set; }

		public void OnSetUp()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json");
			server = new TestServer(new WebHostBuilder().UseConfiguration(builder.Build()).UseStartup<Startup>());
			Client = server.CreateClient();
		}

		public void Dispose()
		{
			Client.Dispose();
			server.Dispose();
		}
	}
}
