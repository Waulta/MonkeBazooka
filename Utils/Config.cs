using System.IO;

using BepInEx;
using BepInEx.Configuration;

namespace MonkeBazooka.Utils
{
    static internal class MBConfig
    {
        public static bool Enabled { get => modEnabled.Value; set => modEnabled.Value = value; }
        public static bool Left { get => isLeft.Value; set => isLeft.Value = value; }
        public static bool Modded { get => moddedRoom; set => moddedRoom = value; }
        public static float ExplosionForce { get => expForce.Value; set => expForce.Value = value; }

        static ConfigEntry<bool> modEnabled;
        static ConfigEntry<bool> isLeft;
        static ConfigEntry<float> expForce;
        static bool moddedRoom;

        static ConfigFile config;

        static MBConfig()
        {
            config = new ConfigFile(Path.Combine(Paths.ConfigPath, "MonkeBazooka.cfg"), true);
            modEnabled = config.Bind("General", "Enabled", true, "enable or disable the mod, can also be changed in the computer");
            isLeft = config.Bind("General", "Left Handed", true, "toggle this to switch hand that bazooka is held in, can also be changed in the computer");
            expForce = config.Bind("General", "Explosion Force", 4.0f, "change the explosion force used to propell the player, can also be changed in the computer");
        }
    }
}