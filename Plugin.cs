using BepInEx;
using System.ComponentModel;
using Utilla;
using HarmonyLib;
using Bepinject;
using System.Reflection;
using MonkeBazooka.Utils;
using MonkeBazooka.Core;

namespace MonkeBazooka
{
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
	[Description("HauntedModMenu")]
	[ModdedGamemode]

    public class MonkeBazookaPlugin : BaseUnityPlugin
	{
        public static MonkeBazookaPlugin Instance;
        
        private void Awake()
        {
            Instance = this;
            new Harmony(PluginInfo.GUID).PatchAll(Assembly.GetExecutingAssembly());
            Zenjector.Install<ComputerInterface.MainInstaller>().OnProject();
        }
        
        void OnEnable()
        {
            try
            {
                if (MBConfig.Modded)
                    MBConfig.Enabled = true;
                    BazookaManager.Instance.Initialize();
                ComputerInterface.MonkeBazookaView.instance.UpdateScreen();
            }
            catch { }
        }

        void Update()
        {
            if (gameObject.GetComponent<MBUtils>() == null)
            {
                gameObject.AddComponent<MBUtils>();
            }
        }
        
        void OnDisable()
        {
            try
            {
                MBConfig.Enabled = false;
                if (MBUtils.Bazooka != null)
                    BazookaManager.Instance.Toggle(false);
                ComputerInterface.MonkeBazookaView.instance.UpdateScreen();
            }
            catch { }
        }

        [ModdedGamemodeJoin]
        void JoinModded(string gamemode)
        {
            MBConfig.Modded = true;
            BazookaManager.Instance.Initialize();
            if (ComputerInterface.MonkeBazookaView.instance != null)
                ComputerInterface.MonkeBazookaView.instance.UpdateScreen();
        }

        [ModdedGamemodeLeave]
        void LeaveModded()
        {
            MBConfig.Modded = false;
            if(BazookaManager.Instance.initialized)
                BazookaManager.Instance.Toggle(false);

            if (ComputerInterface.MonkeBazookaView.instance != null)
                ComputerInterface.MonkeBazookaView.instance.UpdateScreen();
        }
    }
}