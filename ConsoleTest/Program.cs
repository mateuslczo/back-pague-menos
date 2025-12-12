using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using PagMenos.Domain.Entities;
using PagMenos.Domain.Interfaces;
using PagMenos.Infraestructure.Repositories;
using PagMenos.Infrastructure.Data;
using PagueMenos.Domain.Interfaces;
using PagueMenos.Infraestructure.Data;

namespace ConsoleTest
{
	internal class Program
	{
		private readonly ICollaboratorRepository prod;
		private readonly IUnitOfWork uow;

		public Program(ICollaboratorRepository _prod, IUnitOfWork _uow)
		{
			prod = _prod;
			uow = _uow;
		}

		static async Task Main(string[] args)
		{
			var ser = new ServiceCollection();

			ser.AddDbContext<EFDbContext>(options => options.UseInMemoryDatabase("PagMenosDataBase").ConfigureWarnings(
			x=>x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));

			ser.AddScoped<IUnitOfWork, UnitOfWork>();
			ser.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
			ser.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			var prov = ser.BuildServiceProvider();

			var context = prov.GetRequiredService<EFDbContext>();
			context.Database.EnsureCreated();

			var program = ActivatorUtilities.CreateInstance<Program>(prov);

			var add = new Collaborator { LastName = "João"
			, Name = "Souza"
			, Password = "aaaaa"
			, User = "jUser" };

			await program.uow.BeginTransactionAsync(); 

			await program.prod.AddAsync(add);

			await program.uow.SaveChangesAsync();

			await program.uow.CommitTransactionAsync();


			Console.WriteLine("Save the data");
		}
	}
}
