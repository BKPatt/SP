using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using ClassesManagerReborn.Util;
using SeniorProject.MonoBehaviours;

namespace SeniorProject.Cards.OneShotWonderClass
{
    class Scope : CustomCard
    {
        public static CardInfo Card = null;

        public override void Callback()
        {
            // Instantiates OneShotWonder Class
            gameObject.GetOrAddComponent<ClassNameMono>().className = OneShotWonderClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            // Modifiers
            cardInfo.allowMultiple = false;

            // Debugging
            //UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} has been setup.");
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Add OneShotWonder calculations
            player.gameObject.GetComponent<OneShotWonderPointAndCard>().numCards++;
            player.gameObject.GetComponent<OneShotWonderPointAndCard>().bullet_grav = true;

            // Debugging
            //UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gameObject.GetComponent<OneShotWonderPointAndCard>().numCards--;
            gameObject.GetComponent<OneShotWonderPointAndCard>().bullet_grav = false;

            // Debugging
            //UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
        }

        protected override string GetTitle()
        {
            return "Scope";
        }
        protected override string GetDescription()
        {
            return "Better aim fixes your bullet drop problem.";
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
                    stat = "Bullet Drop",
                    amount = "None",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.FirepowerYellow;
        }
        public override string GetModName()
        {
            return SeniorProject.ModInitials;
        }
    }
}
