using Il2Cpp;
using Il2CppHoloville.HOTween.Core.Easing;
using CanisterRefill;
using MelonLoader;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Il2CppTLD.Gear;
using Il2CppTLD.Gameplay.Condition;
using Il2CppNodeCanvas.BehaviourTrees;
using JetBrains.Annotations;

namespace ModNamespace
{
    internal sealed class CanisterRefillMain : MelonMod
    {

        public override void OnInitializeMelon()
        {
            MelonLoader.MelonLogger.Msg(System.ConsoleColor.Yellow, "I've heard tales of singing pipes");
            MelonLoader.MelonLogger.Msg(System.ConsoleColor.Yellow, "Maybe breathing these fumes ain't the best Bourbon");
            MelonLoader.MelonLogger.Msg(System.ConsoleColor.Green, "Canister Refill Loaded!");
        }

    }
}