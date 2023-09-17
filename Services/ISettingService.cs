using System.Collections.Generic;
using TheOregonTrailAI.Models;

namespace TheOregonTrailAI.Services;

public interface ISettingService
{
   SettingsModel? GetSetting(string settingKey);
   bool SetSetting(SettingsModel settingsModel);
   bool SetSetting(string key, string value);
   bool DeleteSetting(string settingKey);
   IEnumerable<SettingsModel> GetManySettings(IEnumerable<string> settingKeys);
}
