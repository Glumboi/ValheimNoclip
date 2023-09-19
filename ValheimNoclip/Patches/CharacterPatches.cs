using System;
using HarmonyLib;
using ValheimNoclip.Utils;

namespace ValheimNoclip.Patches
{
    [HarmonyPatch(typeof(Character))]
    internal class CharacterPatches
    {
        public static bool noFallDamage = false;

        [HarmonyPatch("ApplyDamage", new Type[] {
            typeof(HitData),
            typeof(bool),
            typeof(bool),
            typeof(HitData.DamageModifier)})]

        [HarmonyPrefix]
        private static void Prefix_ApplyDamage(Character __instance,
            HitData hit,
            bool showDamageText,
            bool triggerEffects,
            HitData.DamageModifier mod = HitData.DamageModifier.Normal)
        {
            if (hit.m_hitType == HitData.HitType.Fall && noFallDamage)
            {
#if DEBUG
                ValheimNoclipPlugin.Log.LogInfo($"Hit type: {hit.m_hitType.ToString()} noFallDamage: {noFallDamage.ToString()}");
                ValheimNoclipPlugin.Log.LogInfo("Avoiding falldamage!");
#endif
                hit.m_damage.m_damage = 0f;
            }
        }
    }
}