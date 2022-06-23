using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using ClassesManagerReborn.Util;
using SeniorProject.MonoBehaviours;

namespace SeniorProject.Cards.LotteryClass
{
    class LittleChaos : CustomCard
    {
        public static CardInfo Card = null;

        public override void Callback()
        {
            // Instantiates Lottery Class
            gameObject.GetOrAddComponent<ClassNameMono>().className = LotteryClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            // Modifiers
            cardInfo.allowMultiple = true;

            // Debugging
            //UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} has been setup.");
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Add lottery and add to number of lottery cards
            player.gameObject.GetComponent<LotteryPointAndCard>().blowUpElseTimer = (int)(player.gameObject.GetComponent<LotteryPointAndCard>().blowUpElseTimer * 0.9f);
            player.gameObject.GetComponent<LotteryPointAndCard>().numCards++;

            // Debugging
            //UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gameObject.GetComponent<LotteryPointAndCard>().blowUpElseTimer = (int)(gameObject.GetComponent<LotteryPointAndCard>().blowUpElseTimer * 1.1f);
            gameObject.GetComponent<LotteryPointAndCard>().numCards--;

            // Debugging
            //UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
        }

        protected override string GetTitle()
        {
            return "Lil Bit O Chaos";
        }
        protected override string GetDescription()
        {
            return "Lower timer on everyone else";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Blow up Timer",
                    amount = "-10%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }
        public override string GetModName()
        {
            return SeniorProject.ModInitials;
        }
    }
}
