using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using ModdingUtils.Utils;

namespace SeniorProject.MonoBehaviours
{
    class BlockActionMono : MonoBehaviour
    {
        private System.Random random = new System.Random();

        private Block block;
        private Player player;
        private CharacterStatModifiers statModifiers;

        private int rand;

        public bool health = false;
        public bool lifeS = false;
        public bool blowUpR = false;
        
        public int numSub = 0;
        public int chanceEx = 10;

        public void Awake()
        {
            // Gets block data
            player = this.gameObject.GetComponentInParent<Player>();
            block = this.gameObject.GetComponentInParent<Block>();
        }

        public void Start()
        {
            player.data.block.BlockAction += OnBlock;
        }

        private void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            runBlock();
        }

        private void runBlock()
        {
            rand = random.Next(numSub);

            if (health && rand == setHealth)
            {
                player.data.maxHealth++;
            }
            if (lifeS && rand == setLst)
            {
                statModifiers.lifeSteal += 0.5f;
            }
            if (blowUpR && rand == setBur)
            {
                int rand1 = random.Next(chanceEx);

                if (rand1 == 0)
                {
                    //UnityEngine.Debug.Log($"Hurt");

                    List<Player> otherPlayers = PlayerManager.instance.players.Where(player => PlayerStatus.PlayerAliveAndSimulated(player) && (player.playerID != this.player.playerID)).ToList();

                    int numPlayers = 0;

                    foreach (Player otherPlayer in otherPlayers)
                    {
                        numPlayers++;
                    }

                    int randomPlayer = random.Next(numPlayers);

                    otherPlayers[randomPlayer].data.healthHandler.CallTakeDamage((Vector2)otherPlayers[randomPlayer].data.healthHandler.transform.position - ((Vector2)(this.transform.position).normalized * (damage * otherPlayers[randomPlayer].data.health)), 
                        (otherPlayers[randomPlayer].data.transform.position), 
                        null, 
                        otherPlayers[randomPlayer], 
                        false);

                    // Debugging
                    /*UnityEngine.Debug.Log($"HH: { (Vector2)otherPlayers[randomPlayer].data.healthHandler.transform.position }");
                    UnityEngine.Debug.Log($"Player Health: { otherPlayers[randomPlayer].data.health }");
                    UnityEngine.Debug.Log($"Damage: { damage }");
                    UnityEngine.Debug.Log($"Damage Total: { damage * otherPlayers[randomPlayer].data.health }");
                    UnityEngine.Debug.Log($"Normalized: { ((Vector2)(this.transform.position).normalized) }");
                    UnityEngine.Debug.Log($"Together: { ((Vector2)(this.transform.position).normalized * (damage * otherPlayers[randomPlayer].data.health)) }");*/
                }
            }
        }

        public float damage = 0.01f;
        public int setHealth;
        public int setLst;
        public int setBur;
    }
}
