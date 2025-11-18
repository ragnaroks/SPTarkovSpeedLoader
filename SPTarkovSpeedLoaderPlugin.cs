using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using System;

namespace SPTarkovSpeedLoader {
    [BepInPlugin("net.skydust.sptarkovspeedloader", "SPTarkovSpeedLoaderPlugin", "1.0.2")]
    [BepInProcess("EscapeFromTarkov")]
    public class SPTarkovSpeedLoaderPlugin:BaseUnityPlugin {
        public static ConfigEntry<Boolean>? Enable { get; private set; } = null;

        public static ConfigEntry<Boolean>? Debug { get; private set; } = null;

        public static ManualLogSource? LogSource{get;private set;} = null;

        private AssemblyPatches_EFT__Player_PlayerInventoryController_Class1204.ConstructorPatch Class1204ConstructorPatch {get;} = new AssemblyPatches_EFT__Player_PlayerInventoryController_Class1204.ConstructorPatch();

        private AssemblyPatches_EFT__Player_PlayerInventoryController_Class1207.ConstructorPatch Class1207ConstructorPatch {get;} = new AssemblyPatches_EFT__Player_PlayerInventoryController_Class1207.ConstructorPatch();

        protected void Awake () {
            SPTarkovSpeedLoaderPlugin.Enable = this.Config.Bind<Boolean>("general","enable",true,"enable this plugin");
            SPTarkovSpeedLoaderPlugin.Debug = this.Config.Bind<Boolean>("general","debug",false,"enable debug features");
            SPTarkovSpeedLoaderPlugin.LogSource = this.Logger;
            this.Logger.LogDebug("plugin loaded");
        }

        protected void Start () {
            this.Class1204ConstructorPatch.Enable();
            this.Class1207ConstructorPatch.Enable();
            this.Logger.LogDebug("plugin actived");
        }

        protected void OnDestroy () {
            this.Class1204ConstructorPatch.Disable();
            this.Class1207ConstructorPatch.Disable();
            this.Logger.LogDebug("plugin deactived");
        }

    }
}
