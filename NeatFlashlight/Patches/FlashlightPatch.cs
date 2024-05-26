using HarmonyLib;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using static NeatWolf.NeatFlashlight.ModBase;

namespace NeatWolf.NeatFlashlight.Patches
{
    [HarmonyPatch(typeof(FlashlightItem))]
    internal class FlashlightPatch
    {
        [HarmonyPatch(nameof(FlashlightItem.SwitchFlashlight))] //Update))]//))]
        [HarmonyPostfix]
        //static void Update(ref FlashlightItem __instance)
        static void SwitchFlashlight(bool on, ref float ___initialIntensity, ref FlashlightItem __instance)
        {
            try
            {
                LogInfo("initialIntensity was " + ___initialIntensity);
                LogInfo("Intensity was " + __instance.flashlightBulb.intensity);
                LogInfo("Range was " + __instance.flashlightBulb.range);
                LogInfo("LightConeAngle was " + __instance.flashlightBulb.spotAngle);
                LogInfo(
                    "Dimmer was " + __instance.flashlightBulb.GetComponent<HDAdditionalLightData>().volumetricDimmer);

                LogInfo("BIntensity was " + __instance.flashlightBulbGlow.intensity);
                LogInfo("BRange was " + __instance.flashlightBulbGlow.range);
                LogInfo("BLightConeAngle was " + __instance.flashlightBulbGlow.spotAngle);
                LogInfo("BDimmer was " + __instance.flashlightBulbGlow.GetComponent<HDAdditionalLightData>()
                    .volumetricDimmer);
            }
            finally
            {
                __instance.flashlightBulbGlow.intensity = Config.BulbGlowIntensity.Value;
                __instance.flashlightBulbGlow.range = Config.BulbGlowRange.Value;
                __instance.flashlightBulbGlow.spotAngle = Config.BulbGlowConeAngle.Value;
                __instance.flashlightBulbGlow.GetComponent<HDAdditionalLightData>().affectsVolumetric =
                    Config.BulbGlowVolumetricIntensity.Value > 0f;
                __instance.flashlightBulbGlow.GetComponent<HDAdditionalLightData>().volumetricDimmer =
                    Config.BulbGlowVolumetricIntensity.Value;
                __instance.flashlightBulbGlow.shadows = LightShadows.None;


                ___initialIntensity = Config.LightIntensity.Value;
                __instance.flashlightBulb.intensity = Config.LightIntensity.Value;
                __instance.flashlightBulb.range = Config.LightRange.Value;
                __instance.flashlightBulb.spotAngle = Config.LightConeAngle.Value;
                __instance.flashlightBulb.GetComponent<HDAdditionalLightData>().affectsVolumetric =
                    Config.LightVolumetricIntensity.Value > 0f;
                __instance.flashlightBulb.GetComponent<HDAdditionalLightData>().volumetricDimmer =
                    Config.LightVolumetricIntensity.Value;
            }
        }
    }
}