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

        // Gets player data and stats
        private Player player;
        private Gun gun;

        private int rand; // which block action to run if enabled
        private int rand1; //odds to hurt random player
        private int randomPlayer; //which player to hurt

        // Block actions (LS changed to damage)
        public bool health = false;
        public bool lifeS = false;
        public bool blowUpR = false;
        
        public int numSub = 0; // number of block actions
        public int chanceEx = 10; // 1 in 10 chance to hurt random player

        private bool debug_l = false;

        public void Awake()
        {
            // Gets block data
            player = this.gameObject.GetComponentInParent<Player>();
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
            // Doesn't sync if running locally
            if (PhotonNetwork.OfflineMode)
            { 
                // rand based on number of block actions enabled
                rand = random.Next(numSub);

                if (health && rand == setHealth)
                {
                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"Health: { player.data.maxHealth }");
                    }
                    //*****************************************************************************



                    // Modifiers
                    player.data.maxHealth++;



                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"Health: { player.data.maxHealth }");
                    }
                    //*****************************************************************************
                }
                if (lifeS && rand == setLst)
                {
                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"Damage: { gun.damage }");
                    }
                    //*****************************************************************************



                    // Modifiers
                    gun.damage += 1;



                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"Damage: { gun.damage }");
                    }
                    //*****************************************************************************
                }
                if (blowUpR && rand == setBur)
                {
                    rand1 = random.Next(chanceEx);

                    if (rand1 == 0)
                    {
                        int numPlayers = 0;

                        List<Player> otherPlayers = PlayerManager.instance.players.Where(player => PlayerStatus.PlayerAliveAndSimulated(player) && (player.playerID != this.player.playerID)).ToList();

                        foreach (Player otherPlayer in otherPlayers)
                        {
                            numPlayers++;
                        }

                        //*****************************************************************************
                        // Debugging
                        if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                        {
                            UnityEngine.Debug.Log($"otherPlayers: { otherPlayers }");
                            UnityEngine.Debug.Log($"Num Players: { numPlayers }");
                        }
                        //*****************************************************************************

                        randomPlayer = random.Next(numPlayers);

                        //*****************************************************************************
                        // Debugging
                        if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                        {
                            UnityEngine.Debug.Log($"Random Player: { randomPlayer }");
                        }
                        //*****************************************************************************



                        otherPlayers[randomPlayer].data.healthHandler.CallTakeDamage(
                            (Vector2)otherPlayers[randomPlayer].data.healthHandler.transform.position - ((Vector2)((this.transform.position).normalized * (damage * otherPlayers[randomPlayer].data.maxHealth))),
                            (otherPlayers[randomPlayer].data.transform.position),
                            null,
                            otherPlayers[randomPlayer],
                            false);



                        //*****************************************************************************
                        // Debugging
                        if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                        {
                            UnityEngine.Debug.Log($"HH: { (Vector2)otherPlayers[randomPlayer].data.healthHandler.transform.position }");
                            UnityEngine.Debug.Log($"Player Health: { otherPlayers[randomPlayer].data.health }");
                            UnityEngine.Debug.Log($"Damage: { damage }");
                            UnityEngine.Debug.Log($"Damage Total: { damage * otherPlayers[randomPlayer].data.health }");
                            UnityEngine.Debug.Log($"Normalized: { ((Vector2)(this.transform.position).normalized) }");
                            UnityEngine.Debug.Log($"Together: { ((Vector2)(this.transform.position).normalized * (damage * otherPlayers[randomPlayer].data.health)) }");
                        }
                        //*****************************************************************************
                    }
                }
            }

            // Sync Clients if running online
            else if (this.player.GetComponent<PhotonView>().IsMine)
            {
                int numPlayers = 0;

                // Other Players (not this.player)
                List<Player> otherPlayers = PlayerManager.instance.players.Where(player => PlayerStatus.PlayerAliveAndSimulated(player) && (player.playerID != this.player.playerID)).ToList();

                // Counts number of players
                foreach (Player otherPlayer in otherPlayers)
                {
                    numPlayers++;
                }

                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"otherPlayers: { otherPlayers }");
                    UnityEngine.Debug.Log($"numPlayers: { numPlayers }");
                }
                //*****************************************************************************

                this.gameObject.GetComponent<PhotonView>().RPC("RPCA_Random", RpcTarget.All, new object[]
                {
                    // Generates random into RPCA_Random
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
            //*****************************************************************************
            // Debugging
            if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"Run RPCA_Random");
            }
            //*****************************************************************************

            rand = srand1;
            rand1 = srand2;
            randomPlayer = srand3;

            //*****************************************************************************
            // Debugging
            if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"rand: { rand }");
                UnityEngine.Debug.Log($"rand1: { rand1 }");
                UnityEngine.Debug.Log($"randomPlayer: { randomPlayer }");
            }
            //*****************************************************************************

            if (health && srand1 == setHealth)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Health: { player.data.maxHealth }");
                }
                //*****************************************************************************



                // Modifiers
                player.data.maxHealth++;



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Health: { player.data.maxHealth }");
                }
                //*****************************************************************************
            }
            if (lifeS && srand1 == setLst)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Damage: { gun.damage }");
                }
                //*****************************************************************************



                // Modifiers
                gun.damage += 1;



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Damage: { gun.damage }");
                }
                //*****************************************************************************
            }
        }

        public float damage = 0.05f;
        public int setHealth;
        public int setLst;
        public int setBur;
    }
}
