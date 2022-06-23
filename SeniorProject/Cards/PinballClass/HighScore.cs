using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using SeniorProject.MonoBehaviours;

namespace SeniorProject.Cards.PinballClass
{
    class HighScore : CustomCard
    {
        public static CardInfo Card = null;

        public override void Callback()
        {
            // Declares this card as part of the Pinball class
            gameObject.GetOrAddComponent<ClassNameMono>().className = PinballClass.name;
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
            player.gameObject.GetComponent<PinballPointAndCard>().numCards++;
            player.gameObject.GetComponent<PinballPointAndCard>().max_ammo++;
            player.gameObject.GetComponent<PinballPointAndCard>().proj_speed *= 1.05f;
            player.gameObject.GetComponent<PinballPointAndCard>().bounce += 5;
            player.gameObject.GetComponent<PinballPointAndCard>().minReload -= 1;

            // Debug
            //UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} Added to Player {player.playerID}");
        }

        public override void OnRemoveCard()
        {
            // Sets stats back if the card is removed
            gameObject.GetComponent<PinballPointAndCard>().numCards--;
            gameObject.GetComponent<PinballPointAndCard>().max_ammo--;
            gameObject.GetComponent<PinballPointAndCard>().proj_speed /= 1.05f;
            gameObject.GetComponent<PinballPointAndCard>().bounce -= 5;
            gameObject.GetComponent<PinballPointAndCard>().minReload += 1;
        }

        protected override string GetTitle()
        {
            return "High Score";
        }

        protected override string GetDescription()
        {
            return "Positive stats for everything Pinball related.";
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Better Pinball",
                    amount = "10%",
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
            return CardThemeColor.CardThemeColorType.FirepowerYellow;
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
