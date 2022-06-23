using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using ClassesManagerReborn.Util;
using SeniorProject.MonoBehaviours;

namespace SeniorProject.Cards.LotteryClass
{
    class Lottery : CustomCard
    {
        public static CardInfo Card = null;

        public override void Callback()
        {
            // Instantiates Lottery Class
            gameObject.GetOrAddComponent<ClassNameMono>();
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            // Modifiers
            statModifiers.movementSpeed *= 2.0f;
            statModifiers.health *= 0.2f;
            statModifiers.sizeMultiplier *= 0.75f;
            block.cooldown = 0.15f;
            statModifiers.gravity = 2.0f;
            statModifiers.jump = 1.45f;
            gun.reloadTimeAdd = 1.5f;
            gun.attackSpeed = 0.5f;

            cardInfo.allowMultiple = false;

            // Debugging
            //UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} has been setup.");
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Add lottery and add to number of lottery cards
            player.gameObject.AddComponent<LotteryPointAndCard>();
            player.gameObject.GetComponent<LotteryPointAndCard>().numCards++;

            // Debugging
            //UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gameObject.GetComponent<LotteryPointAndCard>().numCards--;
            GameObject.Destroy(player.gameObject.GetOrAddComponent<LotteryPointAndCard>());

            // Debugging
            //UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
        }

        protected override string GetTitle()
        {
            return "Lottery Class";
        }
        protected override string GetDescription()
        {
            return "Random chance to blow up every 10 seconds, may pay off... The true class of chaos.";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Time per Class Card",
                    amount = "Increased",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Time per Point",
                    amount = "Increased",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Self Destruct",
                    amount = "100%",
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