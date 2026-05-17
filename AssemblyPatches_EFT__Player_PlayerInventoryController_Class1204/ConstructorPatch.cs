using System;
using System.Collections.Generic;
using System.Reflection;
using SPT.Reflection.Patching;

namespace SPTarkovSpeedLoader.AssemblyPatches_EFT__Player_PlayerInventoryController_Class1204 {
    public class ConstructorPatch : ModulePatch {
        protected override MethodBase GetTargetMethod () {
            Type[] types = new Type[] {
                typeof(EFT.InventoryLogic.InventoryController),
                typeof(MagazineItemClass),
                typeof(AmmoItemClass),
                typeof(Int32),
                typeof(Boolean),
                typeof(Single)
            };
            return typeof(EFT.Player.PlayerInventoryController.Class1204)
                .GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, CallingConventions.HasThis, types, null);
        }

        [PatchPrefix]
        public static void Prefix (EFT.InventoryLogic.InventoryController inventoryController, ref Single loadOneAmmoSpeed) {
            if (SPTarkovSpeedLoaderPlugin.Enable?.Value != true) { return; }
            if (loadOneAmmoSpeed <= Constants.MinSpeed) { return; }
            if (SPTarkovSpeedLoaderPlugin.Debug?.Value == true) {
                SPTarkovSpeedLoaderPlugin.LogSource?.LogInfo(String.Concat("origin load speed: ", loadOneAmmoSpeed));
            }
            IEnumerable<EFT.InventoryLogic.Item> items = inventoryController.Inventory.GetItemsInSlots(new List<EFT.InventoryLogic.EquipmentSlot>() { EFT.InventoryLogic.EquipmentSlot.Pockets });
            foreach (EFT.InventoryLogic.Item item in items) {
                if (item.StringTemplateId != Constants.LeathermanMultitool) { continue; }
                loadOneAmmoSpeed *= Constants.Coef;
            }
            if (loadOneAmmoSpeed < Constants.MinSpeed) {
                loadOneAmmoSpeed = Constants.MinSpeed;
            }
            if (SPTarkovSpeedLoaderPlugin.Debug?.Value == true) {
                SPTarkovSpeedLoaderPlugin.LogSource?.LogInfo(String.Concat("final load speed: ", loadOneAmmoSpeed));
            }
        }
    }
}
