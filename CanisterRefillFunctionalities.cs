using Il2Cpp;
using Il2CppNodeCanvas.Tasks.Actions;
using Il2CppProCore.Decals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static Il2CppMono.Security.X509.X520;

namespace CanisterRefill
{
    internal class CRFunctionalities
    {
        internal static string canisterRefillText;
        private static GameObject canisterRefillButton;
        internal static GearItem canisterItem;
        internal static string canisterName;

        internal static void InitializeMTB(ItemDescriptionPage itemDescriptionPage)
        {
            canisterRefillText = "Refill";

            GameObject equipButton = itemDescriptionPage.m_MouseButtonEquip;
            canisterRefillButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            canisterRefillButton.transform.Translate(0, -0.1f, 0);
            Utils.GetComponentInChildren<UILabel>(canisterRefillButton).text = canisterRefillText;

            AddAction(canisterRefillButton, new System.Action(OnCanisterRefill));

            SetCanisterRefillActive(false);

        }
        private static void AddAction(GameObject button, System.Action action)
        {
            Il2CppSystem.Collections.Generic.List<EventDelegate> placeHolderList = new Il2CppSystem.Collections.Generic.List<EventDelegate>();
            placeHolderList.Add(new EventDelegate(action));
            Utils.GetComponentInChildren<UIButton>(button).onClick = placeHolderList;
        }

        internal static void SetCanisterRefillActive(bool active)
        {
            NGUITools.SetActive(canisterRefillButton, active);
        }

        private static void OnCanisterRefill()
        {
            var thisGearItem = CRFunctionalities.canisterItem;
            GearItem canister = GameManager.GetInventoryComponent().GearInInventoryAtCondition("GEAR_Canister", 1 , 0);
            GearItem charcoal = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_Charcoal");
            GearItem cloth = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_Cloth");

                if (thisGearItem == null) return;
                if (charcoal == null)
                {
                    HUDMessage.AddMessage("This action requires charcoal and cloth");
                    GameAudioManager.PlayGUIError();
                    return;
                }
                if (cloth == null)
                {
                HUDMessage.AddMessage("This action requires charcoal and cloth");
                GameAudioManager.PlayGUIError();
                return;
                }
            if (thisGearItem.name == "GEAR_Canister")
                {
                if (charcoal.m_StackableItem.m_Units < 10 && cloth.m_StackableItem.m_Units < 2)
                {
                    HUDMessage.AddMessage("This action requires 10 charcoal and 2 cloth");
                    GameAudioManager.PlayGUIError();
                    return;
                }
                if (charcoal.m_StackableItem.m_Units >= 10 && cloth.m_StackableItem.m_Units >= 2)
                {
                    GameAudioManager.PlayGuiConfirm();
                    InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(("Refilling..."), 2f, 0f, 0f,
                                    "PLAY_CRAFTINGGENERIC", null, false, true, new System.Action<bool, bool, float>(OnCanisterRefillFinished));
                    GearItem.Destroy(thisGearItem);
                    GameManager.GetInventoryComponent().RemoveGearFromInventory(charcoal.name, 10);
                    GameManager.GetInventoryComponent().RemoveGearFromInventory(cloth.name, 2);
                }
            }
                else
                {
                    HUDMessage.AddMessage("This action requires charcoal and cloth");
                    GameAudioManager.PlayGUIError();
                }

        }
        private static void OnCanisterRefillFinished(bool success, bool playerCancel, float progress)
        {
            GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(CanisterRefillUtils.canister, 1);
        }

    }
}
