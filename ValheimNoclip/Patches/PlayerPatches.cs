using HarmonyLib;
using UnityEngine;
using ValheimNoclip.Utils;

namespace ValheimNoclip.Patches
{
    [HarmonyPatch(typeof(Player))]
    internal class PlayerPatches
    {
        [HarmonyPatch("FixedUpdate")]
        [HarmonyPostfix]
        private static void Postfix_FixedUpdate(Player __instance)
        {
            Noclip.DoNoclip(ref __instance, Traverse.Create(__instance).Field<Rigidbody>("m_body"));
        }

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        private static void Postfix_Update(Player __instance)
        {
            if (ValheimNoclipPlugin.ToggleNoclipKeyShortcut.Value.IsDown())
            {
                Noclip.isNoclip = !Noclip.isNoclip;
                CharacterPatches.noFallDamage = !Noclip.isNoclip;
            }
        }
    }
}