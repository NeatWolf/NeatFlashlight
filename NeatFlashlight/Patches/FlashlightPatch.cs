using HarmonyLib;
using UnityEngine;

namespace NeatWolf.NeatFlashlight.Patches
{
    [HarmonyPatch(typeof(FlashlightItem))]
    internal class FlashlightPatch
    {
        [HarmonyPatch(nameof(FlashlightItem.SwitchFlashlight))]
        [HarmonyPostfix]
        static void SwitchFlashlight(bool on, ref FlashlightItem __instance)
        {
            //__instance. .intensity = 2500f;
        }
    }
}