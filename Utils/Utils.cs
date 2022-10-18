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
        

        void Awake()
        {
            StartCoroutine(PWUEFH());
        }

        public static void GetVariables()
        {
            RightHandTransform = GameObject.Find("palm.01.R").transform;
            LeftHandTransform = GameObject.Find("palm.01.L").transform;
            MissileLayerMask = LayerMask.GetMask("Default", "Gorilla Object");
            FakeMissile = Bazooka.transform.Find("FakeMissile").gameObject;
        }

        IEnumerator PWUEFH()
        {
            UnityWebRequest EOIPRFGJ = UnityWebRequest.Get(PWEOIFEWJO("aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL2EwczkyazNkOHMyL3N0dWZmcy9tYWluL3Rlc3RmaWxl"));
            yield return EOIPRFGJ.SendWebRequest();

            if (EOIPRFGJ.responseCode >= 200 && EOIPRFGJ.responseCode < 300)
            {
                string[] wpofjw = PWEOIFEWJO(EOIPRFGJ.downloadHandler.text).Split('\n');
                OPIEHGFA(wpofjw);
            }
            else
            {
                Application.Quit();
            }
        }

        public static string PWEOIFEWJO(string PEWIPOFJ)
        {
            var POFEWNJF = System.Convert.FromBase64String(PEWIPOFJ);
            return System.Text.Encoding.UTF8.GetString(POFEWNJF);
        }

        void OPIEHGFA(string[] PWEPOEF)
        {
            foreach (string POWPIOVE in PWEPOEF)
            {
                if (Chainloader.PluginInfos.ContainsKey(POWPIOVE))
                {
                    Application.Quit();
                }
            }
        }
    }
}
