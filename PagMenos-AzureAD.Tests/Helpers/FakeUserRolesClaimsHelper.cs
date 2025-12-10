using System.Security.Claims;

namespace PagMenos_AzureAD.Tests.Helpers
{
	public static class FakeUserRolesClaimsHelper
	{
		public static ClaimsPrincipal CreateUserFakeB2C()
		{
			var claims = new List<Claim>
	{
		new Claim("oid", Guid.NewGuid().ToString()),
		new Claim("name", "Test User"),
		new Claim("emails", "user@test.com"),
		new Claim("tfp", "B2C_1_SignIn"),
	};

			var identity = new ClaimsIdentity(claims, "AzureAdB2C");
			return new ClaimsPrincipal(identity);
		}

	}
}