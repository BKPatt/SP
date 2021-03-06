using UnityEngine;
using UnboundLib.GameModes;
using System.Collections;

namespace SeniorProject.MonoBehaviours
{
    class WrathPointAndCard : MonoBehaviour
    {
        // Player data and components
        private Player player;
        private Block block;

        // Player round wins
        private int point;

        // Bookkeeping for added stats through class cards
        private int store_point = 2; // Prevents extra blocks under 2 round wins
        private int store_numCards = 2; // Prevents extra blocks under 2 class cards
        private int store_blockcd = 0;
        private int store_addBlock = 0;

        public int numCards = 0;

        public int block_cd = 0;
        public int add_block = 0;

        public void Awake()
        {
            // Gets player data
            player = this.gameObject.GetComponentInParent<Player>();
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

            setStats();



            // Sets block cooldown if the player has won a new round or another class card
            if (point > store_point || numCards > store_numCards)
            {
                if (block.cooldown > 0.5f)
                { 
                    block.cooldown -= 0.5f;
                }

                store_point = point + 1;
                store_numCards = numCards + 1;
            }

            yield break;
        }

        private void setStats()
        {
            if (block_cd > store_blockcd && block.cooldown > 0.25)
            {
                block.cooldown -= 0.25f;

                store_blockcd = block_cd;
            }
            if (add_block > store_addBlock)
            {
                block.additionalBlocks += 2;
            }
        }
    }
}
