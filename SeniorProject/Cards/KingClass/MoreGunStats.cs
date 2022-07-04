using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using SeniorProject.MonoBehaviours;

namespace SeniorProject.Cards.KingClass
{
    class MoreGunStats : CustomCard
    {
        public static CardInfo Card = null;

        public override void Callback()
        {
            // Declares this card as part of the Pinball class
            gameObject.GetOrAddComponent<ClassNameMono>().className = KingClass.name;
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            // Modifiers
            cardInfo.allowMultiple = true;

            // Debug
            //UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} Built");
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Sets new stats for Pinball class
            player.gameObject.GetComponent<KingPointAndCard>().numCards++;
            player.gameObject.GetComponent<KingPointAndCard>().bounce += 2;
            player.gameObject.GetComponent<KingPointAndCard>().damage++;
            player.gameObject.GetComponent<KingPointAndCard>().knockback += 1;

            

            // Debug
            //UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} Added to Player {player.playerID}");
        }

        public override void OnRemoveCard()
        {
            // Sets stats back if the card is removed
            gameObject.GetComponent<KingPointAndCard>().numCards--;
            gameObject.GetComponent<KingPointAndCard>().bounce -= 2;
            gameObject.GetComponent<KingPointAndCard>().damage--;
            gameObject.GetComponent<KingPointAndCard>().knockback -= 1;
        }

        protected override string GetTitle()
        {
            return "Better Gun";
        }

        protected override string GetDescription()
        {
            return "Higher max random for gun stats.";
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bounces",
                    amount = "0-X+2",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Damage",
                    amount = "1-X(Y+1)",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Knockback",
                    amount = "1-X+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            };
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
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
