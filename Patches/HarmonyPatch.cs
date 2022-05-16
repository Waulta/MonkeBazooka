using HarmonyLib;
using MonkeBazooka.Utils;
using MonkeBazooka.Core;
namespace MonkeBazooka.Patches
{
    [HarmonyLib.HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyLib.HarmonyPatch("Awake", MethodType.Normal)]
    class HarmonyPatch
    {
        private static void Postfix(GorillaLocomotion.Player __instance)
        {
            __instance.gameObject.AddComponent<BazookaManager>();
        }
    }
}