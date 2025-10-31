using System.Reflection;
using DbUp;

namespace Otus.HA.Database.Migrations;

public static class Program
{
	public static void Main(string[] args)
	{
		var configuration = new ConfigurationBuilder()
			.AddEnvironmentVariables()
			.Build();
		
		var connection = configuration.GetConnectionString("otus-ha-service-db-connection");
		if (connection == null)
		{
			throw new Exception("Строка подключения к БД сервиса Otus.HA.Host не найдена в конфигурации.");
		}

		var builder = DeployChanges.To.
			PostgresqlDatabase(connection)
			.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
			.LogToConsole()
			.Build();

		var result = builder.PerformUpgrade();

		if (!result.Successful)
		{
			throw new Exception("Ошибка наката миграций БД сервиса Otus.HA.Host.", result.Error);
		}
		
		Console.WriteLine("Миграции БД сервиса Otus.HA.Host завершены успешно, приложение готово к работе.");
	}
}