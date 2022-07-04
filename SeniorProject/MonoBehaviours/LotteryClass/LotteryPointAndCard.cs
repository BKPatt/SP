using System;
using System.Collections.Generic;
using UnityEngine;
using ModdingUtils.Utils;
using UnboundLib.GameModes;
using System.Collections;
using Photon.Pun;
using System.Linq;

namespace SeniorProject.MonoBehaviours
{
    class LotteryPointAndCard : MonoBehaviour
    {
        private DateTime currentTime;
        private DateTime endKs;
        private DateTime endElse;
        private Player player;
        private Gun gun;
        private GunAmmo gunAmmo;
        private CharacterStatModifiers statModifiers;
        private Block block;

        private int point;
        private int stored_point;
        private int stored_cards;

        private int num = 1;
        private int which;

        private int bus;
        private int bur;

        public int numCards = 0;

        public int blowUpS = 1;
        public int blowUpSelfTimer = 10;

        public int enableR = 0;
        public int blowUpR = 100;
        public int blowUpElseTimer = 60;

        public int blowUpAb = 0;

        private int start = 0;

        public void Awake()
        {
            // Gets player data
            player = this.gameObject.GetComponentInParent<Player>();
            statModifiers = this.gameObject.GetComponentInParent<CharacterStatModifiers>();
            gun = this.player.GetComponent<Holding>().holdable.GetComponent<Gun>();
            gunAmmo = GetComponent<Holding>().holdable.GetComponentInChildren<GunAmmo>();
            block = this.gameObject.GetComponentInParent<Block>();

            // Sets listener for when card pick ends
            GameModeManager.AddHook(GameModeHooks.HookPickEnd, OnPickEnd);
            GameModeManager.AddHook(GameModeHooks.HookPointStart, OnBattleStart);
            GameModeManager.AddHook(GameModeHooks.HookPointEnd, OnBattleEnd);
        }

        public void Start()
        {
            // Get initial rounds a player has when class is chosen
            point = GameModeManager.CurrentHandler.GetTeamScore(player.teamID).rounds;
        }

        public void Update()
        {
            System.Random rand = new System.Random();

            if (num == 1)
            {
                which = rand.Next(3);

                num = 0;
            }

            if (PlayerStatus.PlayerAliveAndSimulated(player))
            {
                currentTime = DateTime.Now;

                // Possible death after x time
                if (currentTime > endKs && start == 1)
                {
                    bus = rand.Next(blowUpS);

                    if (bus == 0)
                    {
                        killSelf();
                    }

                    setKsTimer();
                }
                // One of three conditions (likely win)
                if (currentTime > endElse && enableR == 1 && start == 1)
                {
                    bur = rand.Next(blowUpR);

                    // Kill random player
                    if (which == 0 && bur == 0)
                    {
                        killRand();
                    }
                    // Kill all but self
                    else if (which == 1)
                    {
                        killAbs();
                    }
                    // Kill all (random winner)
                    else if (which == 2)
                    {
                        killAll();
                    }

                    setOtherTimer();
                }
            }
        }

        private void killSelf()
        {
            player.data.view.RPC("RPCA_Die", RpcTarget.All, new object[]
                    {
                        new Vector2(0, 1f)
                    });
        }

        private void killRand()
        {
            List<Player> otherPlayers = PlayerManager.instance.players.Where(player => PlayerStatus.PlayerAliveAndSimulated(player) && (player.playerID != this.player.playerID)).ToList();

            System.Random rand = new System.Random();

            int numPlayers = 0;

            foreach (Player otherPlayer in otherPlayers)
            {
                numPlayers++;
            }

            int randomPlayer = rand.Next(numPlayers);

            otherPlayers[randomPlayer].data.view.RPC("RPCA_Die", RpcTarget.All, new object[]
                {
                        new Vector2(0, 1f)
                });
        }

        private void killAbs()
        {
            List<Player> otherPlayers = PlayerManager.instance.players.Where(player => PlayerStatus.PlayerAliveAndSimulated(player) && (player.playerID != this.player.playerID)).ToList();

            int numPlayers = 0;

            foreach (Player otherPlayer in otherPlayers)
            {
                otherPlayers[numPlayers].data.view.RPC("RPCA_Die", RpcTarget.All, new object[]
                {
                    new Vector2(0, 1f)
                });

                numPlayers++;
            }
        }

        private void killAll()
        {
            List<Player> otherPlayers = PlayerManager.instance.players.Where(player => PlayerStatus.PlayerAliveAndSimulated(player)).ToList();

            int numPlayers = 0;

            foreach (Player otherPlayer in otherPlayers)
            {
                otherPlayers[numPlayers].data.view.RPC("RPCA_Die", RpcTarget.All, new object[]
                {
                    new Vector2(0, 1f)
                });

                numPlayers++;
            }
        }

        private void setKsTimer()
        {
            endKs = DateTime.Now.AddSeconds(blowUpSelfTimer);
        }
        private void setOtherTimer()
        {
            endElse = DateTime.Now.AddSeconds(blowUpElseTimer);
        }

        // Referenced whenever card pick ends
        public IEnumerator OnPickEnd(IGameModeHandler gm)
        {
            // Get current amount of rounds the player has won
            point = GameModeManager.CurrentHandler.GetTeamScore(player.teamID).rounds;

            // Set new stats
            if (point > stored_point)
            {
                blowUpSelfTimer++;

                stored_point = point;
            }
            if (numCards > stored_cards)
            {
                blowUpSelfTimer += (numCards - stored_cards);

                stored_cards = numCards;
            }

            yield break;
        }

        public IEnumerator OnBattleStart(IGameModeHandler gm)
        {
            setOtherTimer();
            setKsTimer();

            start = 1;

            yield break;
        }

        public IEnumerator OnBattleEnd(IGameModeHandler gm)
        {
            start = 0;

            yield break;
        }
    }
}
