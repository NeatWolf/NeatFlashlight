using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace NeatWolf.NeatFlashlight
{
    public static class PluginInfo
    {
        public const string PLUGIN_GUID = "com.neatwolf.lc.NeatFlashlight";
        public const string PLUGIN_NAME = "NeatFlashlight";
        public const string PLUGIN_VERSION = "0.0.1";

    }
    
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class ModBase : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
        private static ModBase Instance;
        private static ManualLogSource mls;

        private static void LogInfo(string info)
        {
            if (mls == null)
                mls = CreateLogger();
            
            mls.LogInfo(PluginInfo.PLUGIN_NAME+": "+info);
        }

        private static ManualLogSource CreateLogger()
        {
            mls = BepInEx.Logging.Logger.CreateLogSource(PluginInfo.PLUGIN_NAME);
            return mls;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            
            LogInfo("Awakened");
            harmony.PatchAll(typeof(ModBase));
        }
    }
}