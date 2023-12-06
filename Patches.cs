using Il2Cpp;
using HarmonyLib;
using UnityEngine;
using Il2CppSteamworks;

namespace CanisterRefill
{
    internal class Patches
    {
        [HarmonyPatch(typeof(Panel_Inventory), nameof(Panel_Inventory.Initialize))]
        internal class CanisterRefillInitialization
        {
            private static void Postfix(Panel_Inventory __instance)
            {
                CanisterRefillUtils.inventory = __instance;
                CRFunctionalities.InitializeMTB(__instance.m_ItemDescriptionPage);
            }
        }
        [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
        internal class UpdateInventoryButton
        {
            private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
            {
                if (__instance != InterfaceManager.GetPanel<Panel_Inventory>()?.m_ItemDescriptionPage) return;
                CRFunctionalities.canisterItem = gi?.GetComponent<GearItem>();
               

                if (gi.name == "GEAR_Canister" && gi.m_CurrentHP == 0)
                {
                    CRFunctionalities.SetCanisterRefillActive(true);
                }
                else
                {
                    CRFunctionalities.SetCanisterRefillActive(false);
                }

            }
        }
    }
}
    