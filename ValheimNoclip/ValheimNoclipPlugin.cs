using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace ValheimNoclip
{
    // TODO Review this file and update to your own requirements.

    [BepInPlugin(MyGUID, PluginName, VersionString)]
    public class ValheimNoclipPlugin : BaseUnityPlugin
    {
        private const string MyGUID = "com.glumboi.ValheimNoclip";

        private const string PluginName = "ValheimNoclip";
        private const string VersionString = "1.0.0";

        public static string ToggleNoclipKeyShortcutKey = "Key for toggling noclip";

        public static ConfigEntry<KeyboardShortcut> ToggleNoclipKeyShortcut;

        private static readonly Harmony Harmony = new Harmony(MyGUID);
        public static ManualLogSource Log = new ManualLogSource(PluginName);

        private void Awake()
        {
            ToggleNoclipKeyShortcut = Config.Bind("General",
                ToggleNoclipKeyShortcutKey,
                new KeyboardShortcut(KeyCode.F5),
                new ConfigDescription("Key for toggling noclip"));

            ToggleNoclipKeyShortcut.SettingChanged += ConfigSettingChanged;

            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} is loading...");
            Harmony.PatchAll();
            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} is loaded.");

            Log = Logger;
        }

        private void ConfigSettingChanged(object sender, System.EventArgs e)
        {
            SettingChangedEventArgs settingChangedEventArgs = e as SettingChangedEventArgs;

            // Check if null and return
            if (settingChangedEventArgs == null)
            {
                return;
            }

            if (settingChangedEventArgs.ChangedSetting.Definition.Key == ToggleNoclipKeyShortcutKey)
            {
                KeyboardShortcut newValue = (KeyboardShortcut)settingChangedEventArgs.ChangedSetting.BoxedValue;
            }
        }
    }
}