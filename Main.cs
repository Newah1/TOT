using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TheOregonTrailAI.Models;
using TheOregonTrailAI.Services;

public partial class Main : Node
{
	[Export]
	private string SecretKey { get; set; }
	[Export]
	private string ConnectionString { get; set; }

	private SqliteConnectionService _sqliteConnectionService;
	private SettingService _settingService;
	public IAiService AiService { get; set; }

	public Main()
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
	
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		

		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
