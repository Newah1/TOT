using System.Collections.Generic;
using System.Linq;

namespace TheOregonTrailAI.Models;

public class SettingsModel
{
    public string SettingKey { get; set; }
    public string SettingValue { get; set; }
}

public static class SettingsModelExtensions
{
    public static Dictionary<string, string> ToDictionary(this IEnumerable<SettingsModel> settingsModels)
    {
        return settingsModels
            .ToDictionary(sm => sm.SettingKey, sm => sm.SettingValue);
    }
}