using System;
using System.Runtime.CompilerServices;
using HarmonyLib;

//*********************************************
// This code origininated in PFFC by Tess/Root
// https://github.com/Tess-y/FFC_Port
//*********************************************

namespace SeniorProject.Extensions
{
    [Serializable]
    public class CharacterStatModifiersAdditionalData
    {
        public bool RemoveSelfDamage;

        public CharacterStatModifiersAdditionalData()
        {
            RemoveSelfDamage = false;
        }
    }

    public static class CharacterStatModifiersExtension
    {
        public static readonly ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalData> Data =
                new ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalData>();

        public static CharacterStatModifiersAdditionalData GetAdditionalData(this CharacterStatModifiers statModifiers)
        {
            return Data.GetOrCreateValue(statModifiers);
        }

        public static void AddData(this CharacterStatModifiers statModifiers, CharacterStatModifiersAdditionalData value)
        {
            try
            {
                Data.Add(statModifiers, value);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(e);
            }
        }

        [HarmonyPatch(typeof(CharacterStatModifiers), "ResetStats")]
        private class CharacterStatModifiersPatchResetStats
        {
            private static void Prefix(CharacterStatModifiers __instance)
            {
                var additionalData = __instance.GetAdditionalData();
                additionalData.RemoveSelfDamage = false;
            }
        }
    }
}
