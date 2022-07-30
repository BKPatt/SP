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
        private System.Random random = new System.Random();

        // Get player data
        private Player player;

        // Time variables to run every X seconds
        private DateTime currentTime;
        private DateTime endKs;
        private DateTime endElse;

        // Number of points and class cards
        private int point;
        private int stored_point;
        private int stored_cards;

        private int which; //which random explosion to run (All Players, All but this.player, One Random)
        private int randomPlayer; //which player to blow up if which == One Random

        private int bus; // bookkeeping for self-destruct random
        private int bur; // bookkeeping for blowing up random

        public int numCards = 0; // Number of class cards player has

        public int blowUpS = 1; // 100% chance to self-destruct
        public int blowUpSelfTimer = 10; // Run every 10 seconds

        public int enableR = 0;
        public int blowUpR = 50; // 1 in 50 chance to blow up random
        public int blowUpElseTimer = 30; // Run every 30 seconds

        private int start = 0; // Tell if the game is running

        public void Awake()
        {
            // Gets player data
            player = this.gameObject.GetComponentInParent<Player>();

            // Sets listener for when card pick ends, battle starts, and point ends
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
            // Don't sync random in local play
            if (PhotonNetwork.OfflineMode)
            {
                // Only runs if player is alive and in the game
                if (PlayerStatus.PlayerAliveAndSimulated(player))
                {
                    // Refresh current time every frame
                    currentTime = DateTime.Now;

                    // Possible death after x time
                    if (currentTime > endKs && start == 1)
                    {
                        bus = random.Next(blowUpS);

                        if (bus == 0)
                        {
                            killSelf();
                        }

                        setKsTimer();
                    }
                    // One of three conditions (likely win)
                    if (currentTime > endElse && enableR == 1 && start == 1)
                    {
                        which = random.Next(3);
                        bur = random.Next(blowUpR);

                        // Kill random player
                        if (which == 0 && bur == 0)
                        {
                            List<Player> otherPlayers = PlayerManager.instance.players.Where(player => PlayerStatus.PlayerAliveAndSimulated(player) && (player.playerID != this.player.playerID)).ToList();

                            int numPlayers = 0;

                            foreach (Player otherPlayer in otherPlayers)
                            {
                                numPlayers++;
                            }

                            otherPlayers[randomPlayer].data.view.RPC("RPCA_Die", RpcTarget.All, new object[]
                                {
                                    new Vector2(0, 1f)
                                });
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

                        setOtherTimer(); // Reset other explosion after running
                    }
                }
            }

            // Sync random across clients if online play
            else if (this.player.GetComponent<PhotonView>().IsMine)
            {
                // Only runs if player is alive and in game
                if (PlayerStatus.PlayerAliveAndSimulated(player))
                {
                    List<Player> otherPlayers = PlayerManager.instance.players.Where(player => PlayerStatus.PlayerAliveAndSimulated(player) && (player.playerID != this.player.playerID)).ToList();

                    int numPlayers = 0;

                    foreach (Player otherPlayer in otherPlayers)
                    {
                        numPlayers++;
                    }

                    this.gameObject.GetComponent<PhotonView>().RPC("RPCA_Chance", RpcTarget.All, new object[]
                    {
                        //Pass randoms to RPCA_Chance
                        which,
                        random.Next(blowUpS),
                        random.Next(blowUpR),
                        random.Next(numPlayers)
                    });

                    // Kill random player
                    if (currentTime > endElse && enableR == 1 && start == 1)
                    {
                        if (which == 0 && bur == 0)
                        {
                            otherPlayers[randomPlayer].data.view.RPC("RPCA_Die", RpcTarget.All, new object[]
                                {
                                    new Vector2(0, 1f)
                                });
                        }
                    }
                    // Other chances in RPCA_Chance
                }
            }
        }

        [PunRPC]
        private void RPCA_Chance(int srand1, int srand2, int srand3, int srand4)
        {
            which = srand1;
            bus = srand2;
            bur = srand3;
            randomPlayer = srand4;
            currentTime = DateTime.Now;

            // Possible death after x time
            if (currentTime > endKs && start == 1)
            {
                if (srand2 == 0)
                {
                    killSelf();
                }

                setKsTimer();
            }
            // One of three conditions (likely win)
            if (currentTime > endElse && enableR == 1 && start == 1)
            {
                // Kill all but self
                if (srand1 == 1)
                {
                    killAbs();
                }
                // Kill all (random winner)
                else if (srand1 == 2)
                {
                    killAll();
                }

                setOtherTimer();
            }
        }

        // Self-destruct
        private void killSelf()
        {
            player.data.view.RPC("RPCA_Die", RpcTarget.All, new object[]
                    {
                        new Vector2(0, 1f)
                    });
        }

        // Kill all except this.player
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

        // Kill everyone on screen for random win
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

        // Self-destruct timer OnBattleStart
        private void setKsTimer()
        {
            endKs = DateTime.Now.AddSeconds(blowUpSelfTimer);
        }
        // Explosion timer OnBattleStart
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

        // Referenced when battle starts and countdown ends
        public IEnumerator OnBattleStart(IGameModeHandler gm)
        {
            setOtherTimer();
            setKsTimer();

            start = 1;

            yield break;
        }

        // Referenced when a player wins the round
        public IEnumerator OnBattleEnd(IGameModeHandler gm)
        {
            start = 0;

            yield break;
        }
    }
}