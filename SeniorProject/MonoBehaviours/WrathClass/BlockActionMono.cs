using ClassesManagerReborn.Util;
using System.Collections.Generic;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using SeniorProject.MonoBehaviours;
using System.Linq;
using ModdingUtils.MonoBehaviours;
using System;
using ModdingUtils.Utils;
using Photon.Pun;

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
                statModifiers.health++;
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
                    List<Player> otherPlayers = PlayerManager.instance.players.Where(player => PlayerStatus.PlayerAliveAndSimulated(player) && (player.playerID != this.player.playerID)).ToList();

                    int numPlayers = 0;

                    foreach (Player otherPlayer in otherPlayers)
                    {
                        numPlayers++;
                    }

                    int randomPlayer = random.Next(numPlayers);

                    otherPlayers[randomPlayer].data.view.RPC("RPCA_Die", RpcTarget.All, new object[]
                        {
                            new Vector2(0, 1f)
                        });
                }
            }
        }

        public int setHealth;
        public int setLst;
        public int setBur;
    }
}
