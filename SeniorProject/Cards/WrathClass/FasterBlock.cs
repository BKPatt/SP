using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using SeniorProject.MonoBehaviours;

namespace SeniorProject.Cards.WrathClass
{
    class FasterBlock : CustomCard
    {
        public static CardInfo Card = null;

        public override void Callback()
        {
            // Declares this card as part of the Pinball class
            gameObject.GetOrAddComponent<ClassNameMono>().className = WrathClass.name;
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
            player.gameObject.GetComponent<WrathPointAndCard>().numCards++;
            player.gameObject.GetComponent<WrathPointAndCard>().block_cd++;

            // Debug
            //UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} Added to Player {player.playerID}");
        }

        public override void OnRemoveCard()
        {
            // Sets stats back if the card is removed
            gameObject.GetComponent<WrathPointAndCard>().numCards--;
            gameObject.GetComponent<WrathPointAndCard>().block_cd--;
        }

        protected override string GetTitle()
        {
            return "Faster Blocks";
        }

        protected override string GetDescription()
        {
            return "Lower block cooldown.";
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Block cooldown",
                    amount = "-0.25s",
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
            return CardThemeColor.CardThemeColorType.ColdBlue;
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
