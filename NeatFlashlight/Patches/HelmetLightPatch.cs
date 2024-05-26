using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;
using static NeatWolf.NeatFlashlight.ModBase;

namespace NeatWolf.NeatFlashlight.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class HelmetLightPatch
    {
        [HarmonyPatch(nameof(PlayerControllerB.ChangeHelmetLight))]
        [HarmonyPostfix]
        public static void ChangeHelmetLight(ref Light ___nightVision, ref PlayerControllerB __instance)
        {
            try
            {
                LogInfo("NIntensity was " + ___nightVision.intensity);
                LogInfo("NRange was " + ___nightVision.range);
            }
            finally
            {
                ___nightVision.intensity = Config.NightVisionIntensity.Value;
                ___nightVision.range = Config.NightVisionRange.Value;
                //___nightVision.spotAngle = Config.NightVisionConeAngle.Value;
                //___nightVision.GetComponent<HDAdditionalLightData>().affectsVolumetric = Config.NightVisionVolumetricIntensity.Value > 0f;
                //___nightVision.GetComponent<HDAdditionalLightData>().volumetricDimmer = Config.NightVisionVolumetricIntensity.Value;
            }
        }
    }
}