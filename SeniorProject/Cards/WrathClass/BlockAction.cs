using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using SeniorProject.MonoBehaviours;
using SeniorProject;

namespace SeniorProject.Cards.WrathClass
{
    class BlockAction : CustomCard
    {
        public static CardInfo Card = null;

        private bool debug_l = false;

        public override void Callback()
        {
            // Declares this card as part of the Wrath class
            gameObject.GetOrAddComponent<ClassNameMono>().className = WrathClass.name;
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            // Modifiers
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
            player.gameObject.GetComponent<WrathPointAndCard>().numCards++;
            player.gameObject.AddComponent<BlockActionMono>();

            // Debugging
            if (debug_l || SeniorProject.debug_g || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} Added to Player {player.playerID}");
            }
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Remove Mono(s) and adjustments
            gameObject.GetComponent<WrathPointAndCard>().numCards--;
            GameObject.Destroy(player.gameObject.GetOrAddComponent<BlockActionMono>());

            // Debugging
            if (debug_l || SeniorProject.debug_g || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} Removed from Player {player.playerID}");
            }
        }

        protected override string GetTitle()
        {
            return "Block Action";
        }

        protected override string GetDescription()
        {
            return "Unlock block action cards.";
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Block Actions",
                    amount = "Unlock",
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
