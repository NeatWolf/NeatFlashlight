using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using NeatWolf.NeatFlashlight.Patches;

namespace NeatWolf.NeatFlashlight
{
    public static class PluginInfo
    {
        public const string PLUGIN_GUID = "com.neatwolf.lc.NeatFlashlight";
        public const string PLUGIN_NAME = "NeatFlashlight";
        public const string PLUGIN_VERSION = "0.0.1";
    }

    public class Config
    {
        public static ConfigEntry<float> LightIntensity;
        public static ConfigEntry<float> LightRange;
        public static ConfigEntry<float> LightConeAngle;
        public static ConfigEntry<float> LightVolumetricIntensity;

        public static ConfigEntry<float> BulbGlowIntensity;
        public static ConfigEntry<float> BulbGlowRange;
        public static ConfigEntry<float> BulbGlowConeAngle;
        public static ConfigEntry<float> BulbGlowVolumetricIntensity;

        public static ConfigEntry<float> NightVisionIntensity;
        public static ConfigEntry<float> NightVisionRange;

        public Config(ConfigFile config)
        {
            //const string sectionName = "Flashlight Settings";
            LightIntensity = config.Bind(
                section: "Flashlight",
                key: "LightIntensity",
                defaultValue: 500f);

            LightRange = config.Bind(
                section: "Flashlight",
                key: "LightRange",
                defaultValue: 55f);

            LightConeAngle = config.Bind(
                section: "Flashlight",
                key: "LightConeAngle",
                defaultValue: 73f);

            LightVolumetricIntensity = config.Bind(
                section: "Flashlight",
                key: "LightVolumetricIntensity",
                defaultValue: 0.6f);

            BulbGlowIntensity = config.Bind(
                section: "Bulb Glow",
                key: "BulbGlowIntensity",
                defaultValue: 2f);

            BulbGlowRange = config.Bind(
                section: "Bulb Glow",
                key: "BulbGlowRange",
                defaultValue: 8f);

            BulbGlowConeAngle = config.Bind(
                section: "Bulb Glow",
                key: "BulbGlowConeAngle",
                defaultValue: 73f);

            BulbGlowVolumetricIntensity = config.Bind(
                section: "Bulb Glow",
                key: "BulbGlowVolumetricIntensity",
                defaultValue: 0.9f);

            NightVisionIntensity = config.Bind(
                section: "NightVision",
                key: "NightVisionIntensity",
                defaultValue: 400f);

            NightVisionRange = config.Bind(
                section: "NightVision",
                key: "NightVisionRange",
                defaultValue: 12f);

            /*NightVisionConeAngle = config.Bind(
                section: "General.NightVision Settings",
                key: "NightVisionConeAngle",
                defaultValue: 73f);*/

            /*NightVisionVolumetricIntensity = config.Bind(
                section: "General.NightVision Settings",
                key: "NightVisionVolumetricIntensity",
                defaultValue: 0.9f);*/
        }
    }

    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class ModBase : BaseUnityPlugin
    {
        private static ModBase Instance;
        private static ManualLogSource mls;

        private readonly Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
        public static Config MyConfig { get; internal set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            MyConfig = new Config(base.Config);

            LogInfo("Starting...");
            harmony.PatchAll(typeof(FlashlightPatch));
            harmony.PatchAll(typeof(HelmetLightPatch));
            LogInfo("All patches applied");
        }

        internal static void LogInfo(string info)
        {
            if (mls == null)
                mls = CreateLogger();

            mls.LogInfo(PluginInfo.PLUGIN_NAME + ": " + info);
        }

        private static ManualLogSource CreateLogger()
        {
            mls = BepInEx.Logging.Logger.CreateLogSource(PluginInfo.PLUGIN_NAME);
            return mls;
        }
    }
}