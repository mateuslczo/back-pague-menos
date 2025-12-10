namespace PagMenos.Application.ConfigModels
{
	/// <summary>
	/// Essa classe tem o intuito de simular dados do Azure AD B2C
	/// </summary>
	public class ClaimMappingsConfig
	{
		public string UserId { get; set; } = "oid";
		public string Email { get; set; } = "emails";
		public string UserName { get; set; } = "preferred_username";
		public string Name { get; set; } = "name";
		public string GivenName { get; set; } = "given_name";
		public string Surname { get; set; } = "family_name";
		public string Roles { get; set; } = "roles";
	}
}