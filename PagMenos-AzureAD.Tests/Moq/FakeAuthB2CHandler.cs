using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PagMenos_AzureAD.Tests.Helpers;
using System.Text.Encodings.Web;

namespace PagMenos_AzureAD.Tests.Moq
{
	[Obsolete]
	public class FakeAuthB2CHandler(
			IOptionsMonitor<AuthenticationSchemeOptions> options,
	ILoggerFactory logger,
	UrlEncoder encoder,
	ISystemClock clock) :AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder, clock)
	{
		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			var claimsPrincipal = FakeUserRolesClaimsHelper.CreateUserFakeB2C();
			var ticket = new AuthenticationTicket(claimsPrincipal, "FakeAzureB2C");

			return Task.FromResult(AuthenticateResult.Success(ticket));
		}
	}
}