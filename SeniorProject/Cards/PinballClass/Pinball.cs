using System.Collections.Generic;
using System.Linq;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using ClassesManagerReborn.Util;
using SeniorProject.MonoBehaviours;
using EasierExtension;

namespace SeniorProject.Cards.PinballClass
{
    class Pinball : CustomCard
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
            gun.damage = 0.70f;

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
            player.gameObject.GetOrAddComponent<JokesOnYou>();
            player.gameObject.AddComponent<PinballPointAndCard>();
            player.transform.gameObject.GetComponent<PinballPointAndCard>().numCards++;

            //Add MonoBehaviours and Extensions
            //characterStats.GetAdditionalData().JokesOnYou = true;
            //player.gameObject.GetOrAddComponent<JokesOnYouHitEffect>();

            // For debugging
            //UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Removes monobehaviours on card removal
            GameObject.Destroy(player.gameObject.GetOrAddComponent<PinballPointAndCard>());
            GameObject.Destroy(player.gameObject.GetOrAddComponent<JokesOnYou>());

            //characterStats.GetAdditionalData().JokesOnYou = false;
            //GameObject.Destroy(player.gameObject.GetOrAddComponent<JokesOnYouHitEffect>());
        }

        protected override string GetTitle()
        {
            return "Pinball Class";
        }
        protected override string GetDescription()
        {
            return "Locks some stats into a slow, upgradable pinball.";
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
                    amount = "More Bounces",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bounces",
                    amount = "+10",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Minimum Reload",
                    amount = "5s",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Max Ammo",
                    amount = "1",
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
        public override bool GetEnabled()
        {
            return true;
        }
    }
}
