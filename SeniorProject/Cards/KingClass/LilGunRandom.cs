using ClassesManagerReborn.Util;
using System.Collections.Generic;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using SeniorProject.MonoBehaviours;
using System.Linq;

namespace SeniorProject.Cards.KingClass
{
    class LilGunRandom : CustomCard
    {
        public static CardInfo Card = null;

        public override void Callback()
        {
            // Declares this card as part of the Pinball class
            gameObject.GetOrAddComponent<ClassNameMono>().className = KingClass.name;
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
            // Fix objects not bouncing off of out of bounds
            ObjectsToSpawn objectsToSpawn = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
            List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
            list.Add(
                objectsToSpawn
            );
            gun.objectsToSpawn = list.ToArray();

            // Sets new stats for Pinball class
            player.gameObject.GetComponent<KingPointAndCard>().numCards++;
            player.gameObject.GetComponent<KingPointAndCard>().enableBounce = true;
            player.gameObject.GetComponent<KingPointAndCard>().enableProj = true;
            player.gameObject.GetComponent<KingPointAndCard>().enableAmmo = true;

            // Debug
            //UnityEngine.Debug.Log($"[{SeniorProject.ModInitials}][Card] {GetTitle()} Added to Player {player.playerID}");
        }

        public override void OnRemoveCard()
        {
            // Sets stats back if the card is removed
            gameObject.GetComponent<KingPointAndCard>().numCards--;
            gameObject.GetComponent<KingPointAndCard>().enableBounce = false;
            gameObject.GetComponent<KingPointAndCard>().enableProj = false;
            gameObject.GetComponent<KingPointAndCard>().enableAmmo = false;
        }

        protected override string GetTitle()
        {
            return "Lil Gun Random";
        }

        protected override string GetDescription()
        {
            return "Enable some gun stats as semi-random.";
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bounce(s)",
                    amount = "0-X",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Number of Projectiles",
                    amount = "1-X",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            };
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DestructiveRed;
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
