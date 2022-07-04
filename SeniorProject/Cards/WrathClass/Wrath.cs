using System.Collections.Generic;
using System.Linq;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using ClassesManagerReborn.Util;
using SeniorProject.MonoBehaviours;
using Ported_FFC.Extensions;

namespace SeniorProject.Cards.WrathClass
{
    class Wrath : CustomCard
    {
        public static CardInfo Card = null;

        public override void Callback()
        {
            // Instantiates Pinball Class
            gameObject.GetOrAddComponent<ClassNameMono>();
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
            // Fix objects not bouncing off of out of bounds
            ObjectsToSpawn objectsToSpawn = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
            List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
            list.Add(
                objectsToSpawn
            );
            gun.objectsToSpawn = list.ToArray();

            // Modifiers
            statModifiers.automaticReload = false;

            // Number of cards of this class a player has
            player.gameObject.AddComponent<WrathPointAndCard>();
            player.gameObject.GetComponent<WrathPointAndCard>().numCards++;

            //Add MonoBehaviours and Extensions
            characterStats.GetAdditionalData().JokesOnYou = true;

            // For debugging
            //UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Removes monobehaviours on card removal
            GameObject.Destroy(player.gameObject.GetOrAddComponent<WrathPointAndCard>());

            characterStats.GetAdditionalData().JokesOnYou = false;
            //GameObject.Destroy(player.gameObject.GetOrAddComponent<JokesOnYouHitEffect>());
        }

        protected override string GetTitle()
        {
            return "Wrath Class";
        }
        protected override string GetDescription()
        {
            return "A class that focuses all anger on blocking.";
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
                    stat = "Per Win + Card",
                    amount = "Less Block Cooldown",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.ColdBlue;
        }
        public override string GetModName()
        {
            return SeniorProject.ModInitials;
        }
        public override bool GetEnabled()
        {
            return true;
        }
    }
}
