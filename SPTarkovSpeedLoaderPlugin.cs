using BepInEx;
using BepInEx.Configuration;
using System;
using UnityEngine;

namespace SPTarkovSpeedLoader {
    [BepInPlugin("net.skydust.sptarkovspeedloader", "SPTarkovSpeedLoaderPlugin", "1.0.1")]
    [BepInProcess("EscapeFromTarkov")]
    public class SPTarkovSpeedLoaderPlugin:BaseUnityPlugin {
        public static ConfigEntry<Boolean>? Enable { get; private set; } = null;

        protected void Awake () {
            SPTarkovSpeedLoaderPlugin.Enable = this.Config.Bind<Boolean>("general","enable",true,"enable this plugin");
            this.Logger.LogDebug("plugin loaded");
        }

        protected void Start () {
            //this.InitPatch?.Enable();
            this.Logger.LogDebug("plugin actived");
        }

        protected void OnDestroy () {
            //this.InitPatch?.Disable();
            this.Logger.LogDebug("plugin deactived");
        }

    }
}
