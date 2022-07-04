using ClassesManagerReborn.Util;
using System.Collections.Generic;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using SeniorProject.MonoBehaviours;
using System.Linq;

namespace SeniorProject.Cards.WrathClass
{
    class ShockAndAwe : CustomCard
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
            cardInfo.allowMultiple = false;

            // Debug
            //UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} Built");
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            block.forceToAdd = 5.0f;

            // Sets new stats for Pinball class
            player.gameObject.GetComponent<WrathPointAndCard>().numCards++;
            player.gameObject.GetComponent<BlockActionMono>().blowUpR = true;
            if (player.gameObject.GetComponent<BlockActionMono>().numSub == 0)
            {
                player.gameObject.GetComponent<BlockActionMono>().setBur = player.gameObject.GetComponent<BlockActionMono>().numSub;
            }
            else
            {
                player.gameObject.GetComponent<BlockActionMono>().numSub++;
                player.gameObject.GetComponent<BlockActionMono>().setBur = player.gameObject.GetComponent<BlockActionMono>().numSub;
            }

            // Debug
            //UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} Added to Player {player.playerID}");
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Sets stats back if the card is removed
            gameObject.GetComponent<WrathPointAndCard>().numCards--;
            gameObject.GetComponent<BlockActionMono>().blowUpR = false;
            gameObject.GetComponent<BlockActionMono>().numSub--;
            gameObject.GetComponent<BlockActionMono>().setBur = default;
        }

        protected override string GetTitle()
        {
            return "Shock and Awe";
        }

        protected override string GetDescription()
        {
            return "Small chance to hurt a random player every time you block.";
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Hurt player",
                    amount = "On Block",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Block Force",
                    amount = "+5",
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
