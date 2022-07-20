using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using ClassesManagerReborn.Util;
using SeniorProject.MonoBehaviours;
using SeniorProject.Extensions;

namespace SeniorProject.Cards.KingClass
{
    class King : CustomCard
    {
        public static CardInfo Card = null;

        private bool debug_l = false;

        public override void Callback()
        {
            // Instantiates King Class
            gameObject.GetOrAddComponent<ClassNameMono>();
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;

            // Debugging
            if (debug_l || SeniorProject.debug_g || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} Built");
            }
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Mono(s) and adjustments
            characterStats.GetAdditionalData().RemoveSelfDamage = true;

            player.gameObject.AddComponent<KingPointAndCard>();
            player.gameObject.GetComponent<KingPointAndCard>().numCards++;
            player.gameObject.GetComponent<KingPointAndCard>().enableHealth = true;
            player.gameObject.GetComponent<KingPointAndCard>().enableDamage = true;

            // Debugging
            if (debug_l || SeniorProject.debug_g || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} Added to Player {player.playerID}");
            }
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Removes Mono(s) and adjustments
            characterStats.GetAdditionalData().RemoveSelfDamage = false;

            gameObject.GetComponent<KingPointAndCard>().numCards--;
            gameObject.GetComponent<KingPointAndCard>().enableHealth = false;
            gameObject.GetComponent<KingPointAndCard>().enableDamage = false;
            GameObject.Destroy(player.gameObject.GetOrAddComponent<KingPointAndCard>());

            // Debugging
            if (debug_l || SeniorProject.debug_g || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} Removed from Player {player.playerID}");
            }
        }

        protected override string GetTitle()
        {
            return "King Class";
        }
        protected override string GetDescription()
        {
            return "Ability to add random stats per Round";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Health",
                    amount = "0-X",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Damage",
                    amount = "0-X",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DestructiveRed;
        }
        public override string GetModName()
        {
            return SeniorProject.ModInitials;
        }
    }
}
