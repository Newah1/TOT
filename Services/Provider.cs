using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using TheOregonTrailAI.Models;
using TheOregonTrailAI.Services;

public partial class Provider : Node
{
	[Export]
	private static string SecretKey { get; set; }
	[Export]
	private static string ConnectionString { get; set; }

	private static SqliteConnectionService _sqliteConnectionService;
	private static SettingService _settingService;
	public static IAiService AiService { get; set; }
	
	
	static Provider()
	{
		AppContext.SetSwitch("Switch.System.Reflection.Assembly.SimulatedLocationInBaseDirectory", true);
		IConfigurationRoot configuration = new ConfigurationBuilder()
			.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
			.AddJsonFile("appsettings.json")
			.Build();
		
		OpenAiSettings openAiSettings = new OpenAiSettings();
		configuration.GetSection("OpenAiSettings").Bind(openAiSettings);

		ConnectionString = configuration["ConnectionStrings:Sqlite"];
		var currentDirectory = Directory.GetCurrentDirectory();
		var executingAssembly = Assembly.GetExecutingAssembly();

		SecretKey = openAiSettings.SecretKey;

		AiService = new TOTOpenAiService(SecretKey);
		var connectionString = "Data Source=" + Directory.GetCurrentDirectory() + "\\tot.db;";
		var sqliteConnection = new SqliteConnectionService(connectionString);
		_sqliteConnectionService = sqliteConnection;

		var settingService = new SettingService(_sqliteConnectionService);
		_settingService = settingService;

		var settingsDictionary = _settingService
			.GetManySettings(new List<string>() { "foo", "bar" })
			.ToDictionary();
		
		GD.Print(settingsDictionary["foo"]);
		GD.Print(settingsDictionary["bar"]);
	}
	
}
