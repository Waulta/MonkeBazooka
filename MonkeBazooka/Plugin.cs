using BepInEx;
using System.ComponentModel;
using Utilla;
using HarmonyLib;
using Bepinject;
using System.Reflection;
using MonkeBazooka.Utils;
using MonkeBazooka.Core;
using System;


namespace MonkeBazooka
{
    [ModdedGamemode]
    [Description("HauntedModMenu")]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.6.7")]
    [BepInDependency("tonimacaroni.computerinterface", "1.5.4")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class MonkeBazookaPlugin : BaseUnityPlugin
    {
        public static MonkeBazookaPlugin Instance;

        internal void Awake()
        {
            Instance = this;

            new Harmony(PluginInfo.GUID).PatchAll(Assembly.GetExecutingAssembly());
            Zenjector.Install<ComputerInterface.MainInstaller>().OnProject();
        }

        internal void OnEnable()
        {
            if (BazookaManager.Instance == null) return;
            try
            {
                if (MBConfig.Modded)
                {
                    MBConfig.Enabled = true;
                    BazookaManager.Instance.Initialize();
                }

                ComputerInterface.MonkeBazookaView.instance.UpdateScreen();
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Join(": ", "MonkeBazooka error", e));
            }
        }

        internal void LateUpdate()
        {
            if (GetComponent<MBUtils>() == null) gameObject.AddComponent<MBUtils>();
        }

        internal void OnDisable()
        {
            if (BazookaManager.Instance == null) return;
            try
            {
                MBConfig.Enabled = false;
                if (MBUtils.Bazooka != null) BazookaManager.Instance.Toggle(false);
                ComputerInterface.MonkeBazookaView.instance.UpdateScreen();
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Join(": ", "MonkeBazooka error", e));
            }
        }


        [ModdedGamemodeJoin]
        internal void JoinModded(string gamemode)
        {
            MBConfig.Modded = true;
            BazookaManager.Instance.Initialize();
            ComputerInterface.MonkeBazookaView.instance?.UpdateScreen();
        }

        [ModdedGamemodeLeave]
        internal void LeaveModded()
        {
            MBConfig.Modded = false;
            if (BazookaManager.Instance.initialized) BazookaManager.Instance.Toggle(false);

            ComputerInterface.MonkeBazookaView.instance?.UpdateScreen();
        }
    }
}