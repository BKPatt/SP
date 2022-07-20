using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using SeniorProject.MonoBehaviours;

namespace SeniorProject.Cards.KingClass
{
    class AmmoLuck : CustomCard
    {
        public static CardInfo Card = null;

        private bool debug_l = false;

        public override void Callback()
        {
            // Declares this card as part of the King class
            gameObject.GetOrAddComponent<ClassNameMono>().className = KingClass.name;
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            // Modifiers
            cardInfo.allowMultiple = true;

            // Debug
            if (debug_l || SeniorProject.debug_g || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} Built");
            }
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Mono(s) and adjustments
            player.gameObject.GetComponent<KingPointAndCard>().numCards++;
            player.gameObject.GetComponent<KingPointAndCard>().numProj += 2;
            player.gameObject.GetComponent<KingPointAndCard>().ammo += 2;

            if (player.gameObject.GetComponent<KingPointAndCard>().reloadTime > 0)
            {
                player.gameObject.GetComponent<KingPointAndCard>().reloadTime -= 1;
            }

            // Debugging
            if (debug_l || SeniorProject.debug_g || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} Added to Player {player.playerID}");
            }
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Remove Mono(s) and adjustments
            gameObject.GetComponent<KingPointAndCard>().numCards--;
            gameObject.GetComponent<KingPointAndCard>().numProj -= 2;
            gameObject.GetComponent<KingPointAndCard>().ammo -= 2;
            gameObject.GetComponent<KingPointAndCard>().reloadTime += 1;

            // Debugging
            if (debug_l || SeniorProject.debug_g || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} Removed from Player {player.playerID}");
            }
        }

        protected override string GetTitle()
        {
            return "Better Ammo";
        }

        protected override string GetDescription()
        {
            return "Higher max random for ammo stats.";
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Number of Projectiles",
                    amount = "0-X+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Ammo",
                    amount = "0-X+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Reload Time",
                    amount = "0-(X-1)",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            };
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DestructiveRed;
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
