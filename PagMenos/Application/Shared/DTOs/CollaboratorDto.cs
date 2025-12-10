namespace PagMenos.Application.Shared.ExceptionsDTOs
{
	public record CollaboratorDto 
	{
        public CollaboratorDto()
        {
            
        }

        public CollaboratorDto(string name, string lastName, string user, string password)
		{
			Name = name;
			LastName = lastName;
			User = user;
			Password = password;
		}

		public string Name { get; set; } = string.Empty;

		public string LastName { get; set; } = string.Empty;

		public string User { get; set; } = string.Empty;

		public string Password { get; set; } = string.Empty;

		//public IReadOnlyCollection<OrderDto> Orders { get; init; } = Array.Empty<OrderDto>();
	}
}