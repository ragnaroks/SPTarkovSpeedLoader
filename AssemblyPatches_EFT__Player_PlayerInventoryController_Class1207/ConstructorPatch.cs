using SPT.Reflection.Patching;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SPTarkovSpeedLoader.AssemblyPatches_EFT__Player_PlayerInventoryController_Class1207 {
    public class ConstructorPatch : ModulePatch {
        private const String LeathermanMultitool = "544fb5454bdc2df8738b456a";
        private const Single Coef = 0.75F;

        protected override MethodBase GetTargetMethod () {
            Type[] types = new Type[] {
                typeof(EFT.InventoryLogic.InventoryController),
                typeof(MagazineItemClass),
                typeof(Single),
                typeof(Boolean)
            };
            return typeof(EFT.Player.PlayerInventoryController.Class1207)
                .GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, CallingConventions.HasThis, types, null);
        }

        [PatchPrefix]
        public static void Prefix (EFT.InventoryLogic.InventoryController inventoryController, ref Single loadOneAmmoSpeed) {
            if (SPTarkovSpeedLoaderPlugin.Enable?.Value != true) { return; }
            if (SPTarkovSpeedLoaderPlugin.Debug?.Value == true) {
                SPTarkovSpeedLoaderPlugin.LogSource?.LogInfo(String.Concat("final unload speed: ",loadOneAmmoSpeed));
            }
            IEnumerable<EFT.InventoryLogic.Item> items = inventoryController.Inventory.GetItemsInSlots(new List<EFT.InventoryLogic.EquipmentSlot>() { EFT.InventoryLogic.EquipmentSlot.Pockets });
            foreach (EFT.InventoryLogic.Item item in items) {
                if (item.StringTemplateId != ConstructorPatch.LeathermanMultitool) { continue; }
                loadOneAmmoSpeed *= ConstructorPatch.Coef;
            }                        
            if (SPTarkovSpeedLoaderPlugin.Debug?.Value == true) {
                SPTarkovSpeedLoaderPlugin.LogSource?.LogInfo(String.Concat("final unload speed: ",loadOneAmmoSpeed));
            }
        }
    }
}
