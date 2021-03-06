using UnityEngine;
using UnboundLib.GameModes;
using System.Collections;

namespace SeniorProject.MonoBehaviours
{
    class GuardianPointAndCard : MonoBehaviour
    {
        // Framework for player & player objects
        private Player player;
        private Gun gun;
        private CharacterStatModifiers statModifiers;
        private Block block;

        // int to find point total for player
        private int point;

        // Used to tell if any of these should be added to player
        private int store_point = 0;
        private int store_numCards = 0;
        private int store_regen = 0;
        private int store_respawn = 0;
        private int store_health = 0;
        private int store_cd = 0;
        private int store_stdo = 0;

        // Used for class cards to increase
        public int addHealth = 0;
        public int numCards = 0;
        public int regeneration = 0;
        public int numRespawns = 0;
        public int block_cd = 0;
        public int stdo = 0;
        public float mSpeed = 0.9f;
        public float mGravity = 1.1f;

        public void Awake()
        {
            // Gets player data
            player = this.gameObject.GetComponentInParent<Player>();
            statModifiers = this.gameObject.GetComponentInParent<CharacterStatModifiers>();
            gun = this.player.GetComponent<Holding>().holdable.GetComponent<Gun>();
            block = this.gameObject.GetComponentInParent<Block>();

            // Sets listener for when card pick ends
            GameModeManager.AddHook(GameModeHooks.HookPickEnd, OnPickEnd);
        }

        public void Start()
        {
            // Get initial rounds a player has when class is chosen
            point = GameModeManager.CurrentHandler.GetTeamScore(player.teamID).rounds;

            // Set initial stats
            setStats();
        }

        // Referenced whenever card pick ends
        public IEnumerator OnPickEnd(IGameModeHandler gm)
        {
            // Get current amount of rounds the player has won
            point = GameModeManager.CurrentHandler.GetTeamScore(player.teamID).rounds;

            // Set new stats
            setStats();

            // Sets health if the player has won a new round or another class card
            if (point > store_point || numCards > store_numCards)
            {
                player.data.maxHealth += ((10 * (point + (numCards * 2))));

                if (player.data.maxHealth > 1000)
                {
                    player.data.stats.sizeMultiplier = 0.95f;
                }

                store_point = point + 1;
                store_numCards = numCards + 1;
            }

            yield break;
        }

        private void setStats()
        {
            gun.projectileColor = Color.green;

            // Seconds to take Damage Over
            if (stdo > store_stdo)
            {
                statModifiers.secondsToTakeDamageOver += 0.25f;
                store_stdo = stdo;
            }
            // Max Health
            if (addHealth > store_health)
            {
                player.data.maxHealth *= 2;
                player.data.stats.sizeMultiplier = 0.95f;
                store_health = addHealth;
            }
            // Respawns
            if (numRespawns > store_respawn)
            {
                statModifiers.respawns++;
                store_respawn = numRespawns;
            }
            // Regen
            if (regeneration > store_regen)
            {
                statModifiers.regen += 5;
                store_regen = regeneration;
            }
            // Block Cooldown
            if (block.cooldown != 0 && block.cooldown >= 0.5f && block_cd > store_cd)
            {
                block.cooldown -= 0.5f;
                store_cd = block_cd;
            }
            // Movement Speed
            if (statModifiers.movementSpeed > mSpeed)
            {
                statModifiers.movementSpeed = mSpeed;
            }
            // Gravity
            if (statModifiers.gravity > mGravity)
            {
                statModifiers.gravity = mGravity;
            }
        }
    }
}