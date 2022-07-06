using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using ModdingUtils.Utils;
using Photon.Pun;

namespace SeniorProject.MonoBehaviours
{
    class BlockActionMono : MonoBehaviour
    {
        private System.Random random = new System.Random();

        private Block block;
        private Player player;
        private Gun gun;

        private int rand;
        private int rand1;
        private int randomPlayer;

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
            gun = this.player.GetComponent<Holding>().holdable.GetComponent<Gun>();
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
            if (PhotonNetwork.OfflineMode)
            { 
                rand = random.Next(numSub);

                if (health && rand == setHealth)
                {
                    player.data.maxHealth++;
                }
                if (lifeS && rand == setLst)
                {
                    gun.damage += 1;
                }
                if (blowUpR && rand == setBur)
                {
                    rand1 = random.Next(chanceEx);

                    if (rand1 == 0)
                    {
                        //UnityEngine.Debug.Log($"Hurt");

                        int numPlayers = 0;

                        List<Player> otherPlayers = PlayerManager.instance.players.Where(player => PlayerStatus.PlayerAliveAndSimulated(player) && (player.playerID != this.player.playerID)).ToList();

                        foreach (Player otherPlayer in otherPlayers)
                        {
                            numPlayers++;
                        }

                        randomPlayer = random.Next(numPlayers);

                        otherPlayers[randomPlayer].data.healthHandler.CallTakeDamage(
                            (Vector2)otherPlayers[randomPlayer].data.healthHandler.transform.position - ((Vector2)((this.transform.position).normalized * (damage * otherPlayers[randomPlayer].data.maxHealth))),
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
            else if (this.player.GetComponent<PhotonView>().IsMine)
            {
                int numPlayers = 0;

                List<Player> otherPlayers = PlayerManager.instance.players.Where(player => PlayerStatus.PlayerAliveAndSimulated(player) && (player.playerID != this.player.playerID)).ToList();

                foreach (Player otherPlayer in otherPlayers)
                {
                    numPlayers++;
                }

                this.gameObject.GetComponent<PhotonView>().RPC("RPCA_Random", RpcTarget.All, new object[]
                {
                    random.Next(numSub),
                    random.Next(chanceEx),
                    random.Next(numPlayers)
                });

                if (blowUpR && rand == setBur)
                {
                    if (rand1 == 0)
                    {
                        otherPlayers[randomPlayer].data.healthHandler.CallTakeDamage(
                                (Vector2)(otherPlayers[randomPlayer].data.healthHandler.transform.position) - ((Vector2)((this.transform.position).normalized * (damage * otherPlayers[randomPlayer].data.maxHealth))),
                                (otherPlayers[randomPlayer].data.transform.position),
                                null,
                                otherPlayers[randomPlayer],
                                false);
                    }
                }
            }
        }

        [PunRPC]
        private void RPCA_Random(int srand1, int srand2, int srand3)
        {
            rand = srand1;
            rand1 = srand2;
            randomPlayer = srand3;

            if (health && srand1 == setHealth)
            {
                player.data.maxHealth++;
            }
            if (lifeS && srand1 == setLst)
            {
                gun.damage += 1;
            }
        }

        public float damage = 0.05f;
        public int setHealth;
        public int setLst;
        public int setBur;
    }
}
