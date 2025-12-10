using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using PagMenos;


using PagMenos_AzureAD.Tests.Moq;


namespace PagMenos_AzureAD.Tests.Factories
{
	public class TestWebApplicationFactory :WebApplicationFactory<Program>
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{
				services.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = "FakeAzureB2C";
					options.DefaultChallengeScheme = "FakeAzureB2C";
				})
				.AddScheme<AuthenticationSchemeOptions, FakeAuthB2CHandler>(
					"FakeAzureB2C", options => { });
			});
		}
	}
}
