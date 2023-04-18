using MonkeBazooka.Core;
using MonkeBazooka.Utils;
using System.Collections;

// https://github.com/legoandmars/Utilla/blob/78c9ef1f18537e48fa7e27c601c118155f463769/Utilla/HarmonyPatches/Patches/PostInitializedPatch.cs

namespace MonkeBazooka.Patches
{
    [HarmonyLib.HarmonyPatch(typeof(GorillaTagger), "Awake")]
    public class HarmonyPatch
    {
        public static void Postfix(GorillaTagger __instance) => __instance.StartCoroutine(Delay(__instance));

        internal static IEnumerator Delay(GorillaTagger __instance)
        {
            yield return 0;

            __instance.gameObject.AddComponent<MBUtils>();
            __instance.gameObject.AddComponent<BazookaManager>();
        }
    }
}