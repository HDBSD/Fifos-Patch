using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HDMods
{
    public class SettingPatch
    {
        public static MelonLoader.MelonPreferences_Category GameSettings;
        internal static Fifo.Core.Settings settingsInst;

        public static void Init(Fifo.Core.Settings Inst)
        {
            GameSettings = MelonLoader.MelonPreferences.CreateCategory("GameSettings");
            GameSettings.SetFilePath("Conf/Settings.cfg", autoload: false);


            GameSettings.CreateEntry<bool>("invertedControls", true);
            GameSettings.CreateEntry<float>("horizonatalSensivilityFactor", 1f);
            GameSettings.CreateEntry<float>("verticalSensivilityFactor", 1f);
            GameSettings.CreateEntry<float>("generalSound", 1f);
            GameSettings.CreateEntry<float>("fxSound", 0.5f);
            GameSettings.CreateEntry<float>("musicSound", 0.5f);
            GameSettings.CreateEntry<int>("graficalSetting", 3);
            GameSettings.CreateEntry<bool>("fullScreen", true);
            GameSettings.CreateEntry<Resolution>("resolutionScreen", Screen.currentResolution);
            GameSettings.CreateEntry<int>("language", 0);
            GameSettings.CreateEntry<int>("vSyncCount", 1);

            GameSettings.LoadFromFile();

            settingsInst = Inst;
        }

        public static void saveSettings()
        {
            GameSettings.GetEntry<bool>("invertedControls").Value = settingsInst.invertedControls; ;
            GameSettings.GetEntry<float>("horizonatalSensivilityFactor").Value = settingsInst.horizonatalSensivilityFactor;
            GameSettings.GetEntry<float>("verticalSensivilityFactor").Value = settingsInst.verticalSensivilityFactor;
            GameSettings.GetEntry<float>("generalSound").Value = settingsInst.generalSound;
            GameSettings.GetEntry<float>("fxSound").Value = settingsInst.fxSound;
            GameSettings.GetEntry<float>("musicSound").Value = settingsInst.musicSound;
            GameSettings.GetEntry<int>("graficalSetting").Value = settingsInst.graficalSetting;
            GameSettings.GetEntry<bool>("fullScreen").Value = settingsInst.fullScreen;
            GameSettings.GetEntry<Resolution>("resolutionScreen").Value = settingsInst.resolutionScreen;
            GameSettings.GetEntry<int>("language").Value = settingsInst.language;
            GameSettings.GetEntry<int>("vSyncCount").Value = settingsInst.vSyncCount;

            GameSettings.SaveToFile();
        }

        public static void RestoreSettings()
        {
            settingsInst.invertedControls = GameSettings.GetEntry<bool>("invertedControls").Value;
            settingsInst.horizonatalSensivilityFactor = GameSettings.GetEntry<float>("horizonatalSensivilityFactor").Value;
            settingsInst.verticalSensivilityFactor = GameSettings.GetEntry<float>("verticalSensivilityFactor").Value;
            settingsInst.generalSound = GameSettings.GetEntry<float>("generalSound").Value;
            settingsInst.fxSound = GameSettings.GetEntry<float>("fxSound").Value;
            settingsInst.musicSound = GameSettings.GetEntry<float>("musicSound").Value;
            settingsInst.graficalSetting = GameSettings.GetEntry<int>("graficalSetting").Value;
            settingsInst.fullScreen = GameSettings.GetEntry<bool>("fullScreen").Value;
            settingsInst.resolutionScreen = GameSettings.GetEntry<Resolution>("resolutionScreen").Value;
            settingsInst.language = GameSettings.GetEntry<int>("language").Value;
            settingsInst.vSyncCount = GameSettings.GetEntry<int>("vSyncCount").Value;
        }

    }

    [HarmonyPatch(typeof(Fifo.Core.Settings), "Initialize")]
    class Initialize_Patch
    {
        [HarmonyPrefix]
        internal static bool Prefix(Fifo.Core.Settings __instance)
        {

            SettingPatch.Init(__instance);
            SettingPatch.RestoreSettings();

            return true;
        }
    }

    [HarmonyPatch(typeof(Fifo.Core.Settings), "ApplyGraphics")]
    class ApplyGraphics_Patch
    {
        [HarmonyPrefix]
        internal static bool Prefix(Fifo.Core.Settings __instance)
        {

            SettingPatch.saveSettings();

            return true;
        }
    }

    [HarmonyPatch(typeof(Fifo.Core.Settings), "ApplyAudio")]
    class ApplyAudio_Patch
    {
        [HarmonyPrefix]
        internal static bool Prefix(Fifo.Core.Settings __instance)
        {

            SettingPatch.saveSettings();

            return true;
        }
    }

    [HarmonyPatch(typeof(Fifo.Core.Settings), "ApplyGame")]
    class ApplyGame_Patch
    {
        [HarmonyPrefix]
        internal static bool Prefix(Fifo.Core.Settings __instance)
        {

            SettingPatch.saveSettings();

            return true;
        }
    }

}
