using System.Collections;
using UnityEngine.Networking;
using BepInEx.Bootstrap;
using UnityEngine;
using MonkeBazooka.Core;

namespace MonkeBazooka.Utils
{
    public class MBUtils : MonoBehaviour
    {
        public static Transform RightHandTransform;
        public static Transform LeftHandTransform;

        public static GameObject ExplosionPrefab;
        public static GameObject MissilePrefab;
        public static GameObject BazookaPrefab;

        public static GameObject Bazooka;
        public static GameObject FakeMissile;
        public static BazookaController BazookaController;

        public static LayerMask MissileLayerMask;

        public static void GetVariables()
        {
            LeftHandTransform = GorillaTagger.Instance.offlineVRRig.leftHandTransform.parent.Find("palm.01.L");
            RightHandTransform = GorillaTagger.Instance.offlineVRRig.rightHandTransform.parent.Find("palm.01.R");
            MissileLayerMask = LayerMask.GetMask("Default", "Gorilla Object");
            FakeMissile = Bazooka.transform.Find("FakeMissile").gameObject;
        }
    }
}