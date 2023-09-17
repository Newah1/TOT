using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using TheOregonTrailAI.Models;

namespace TheOregonTrailAI.Services;

public class SettingService : ISettingService
{
	private readonly SqliteConnectionService _sqliteConnectionService;
	private const string _tableName = "GameSettings";
	
	public SettingService(SqliteConnectionService sqliteConnectionService)
	{
		_sqliteConnectionService = sqliteConnectionService;
	}
	
	public SettingsModel? GetSetting(string settingKey)
	{
		try
		{
			const string query = $"SELECT * FROM {_tableName} WHERE SettingKey=@SettingKey LIMIT 1";

			using var connection = _sqliteConnectionService.GetConnection();

			var setting = connection.Query<SettingsModel>(query, new { SettingKey = settingKey }).SingleOrDefault();

			return setting;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public bool SetSetting(string key, string value)
	{
		var settingsModel = new SettingsModel()
		{
			SettingKey = key,
			SettingValue = value
		};

		return _SetSetting(settingsModel);
	}

	public bool SetSetting(SettingsModel settingsModel) => _SetSetting(settingsModel);

	private bool _SetSetting(SettingsModel settingsModel)
	{
		const string query = $"INSERT OR REPLACE INTO {_tableName} (SettingKey, SettingValue) VALUES (@SettingKey, @SettingValue)";

		try
		{
			using var connection = _sqliteConnectionService.GetConnection();

			var execution = connection.Execute(query, settingsModel);

			if (execution > 0)
			{
				return true;
			}

			return false;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public bool DeleteSetting(string settingKey)
	{
		const string query = $"DELETE FROM {_tableName} WHERE SettingKey=@SettingKey";

		try
		{
			using var connection = _sqliteConnectionService.GetConnection();

			var execution = connection.Execute(query, new { SettingKey = settingKey });

			return execution > 0;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public IEnumerable<SettingsModel> GetManySettings(IEnumerable<string> settingKeys)
	{
		const string query = $"SELECT * FROM {_tableName} WHERE SettingKey IN @SettingKeys";

		try
		{
			using var connection = _sqliteConnectionService.GetConnection();

			var settings = connection.Query<SettingsModel>(query, new { SettingKeys = settingKeys });
			return settings;
			
		}
		catch (Exception)
		{
			return new List<SettingsModel>();
		}
	}
}
