using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using SeniorProject.MonoBehaviours;

namespace SeniorProject.Cards.GuardianClass
{
    class DoubleHealth : CustomCard
    {
        public static CardInfo Card = null;

        private bool debug_l = false;

        public override void Callback()
        {
            // Declares this card as part of the Guardian class
            gameObject.GetOrAddComponent<ClassNameMono>().className = GuardianClass.name;
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            // Modifiers
            cardInfo.allowMultiple = true;

            // Debugging
            if (debug_l || SeniorProject.debug_g || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} Built");
            }
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Mono(s) and adjustments
            player.gameObject.GetComponent<GuardianPointAndCard>().numCards++;
            player.gameObject.GetComponent<GuardianPointAndCard>().addHealth++;
            player.gameObject.GetComponent<GuardianPointAndCard>().mSpeed -= 0.01f;
            player.gameObject.GetComponent<GuardianPointAndCard>().mGravity += 0.01f;

            // Debugging
            if (debug_l || SeniorProject.debug_g || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} Added to Player {player.playerID}");
            }
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Removes Mono(s) and adjustments
            gameObject.GetComponent<GuardianPointAndCard>().numCards--;
            gameObject.GetComponent<GuardianPointAndCard>().addHealth--;
            gameObject.GetComponent<GuardianPointAndCard>().mSpeed += 0.01f;
            gameObject.GetComponent<GuardianPointAndCard>().mGravity -= 0.01f;

            // Debugging
            if (debug_l || SeniorProject.debug_g || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} Removed from Player {player.playerID}");
            }
        }

        protected override string GetTitle()
        {
            return "Guardians Armor";
        }

        protected override string GetDescription()
        {
            return "Add double health to your Guardian.";
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Health",
                    amount = "+200%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Movement Speed",
                    amount = "-1%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Gravity",
                    amount = "+1%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DefensiveBlue;
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        public override string GetModName()
        {
            return SeniorProject.ModInitials;
        }
    }
}
