using System;
using ModdingUtils.MonoBehaviours;
using UnboundLib.GameModes;
using System.Collections;
using ModdingUtils.Extensions;
using Photon.Pun;

//*****************************************************************************
// Test Reversible Effect for King class (works a bit differently from other one
// Not currently implemented
//*****************************************************************************

namespace SeniorProject.MonoBehaviours
{
    class KingPointAndCard : CounterReversibleEffect
    {
        // Used later to set a bunch of random stats
        private System.Random random = new System.Random();

        // Used to enable these features with class cards
        public bool enableSpeed = false;
        public bool enableGrav = false;
        public bool enableJump = false;
        public bool enableHealth = false;
        public bool enableDamageOver = false;
        public bool enableAtSpeed = false;
        public bool enableBounce = false;
        public bool enableProj = false;
        public bool enableAmmo = false;
        public bool enableDamage = false;
        public bool enableKnockback = false;
        public bool enableRelTime = false;

        // Used to set stats to a semi-random number if enabled
        private int rand1; //movement speed
        private double rand2; //gravity
        private int rand3; //gravity
        private int rand4; //jump
        private int rand5; //health
        private int rand6; //seconds to take damage over
        private double rand7; //attack speed
        private int rand8; //bounces
        private int rand11; //number of projectiles
        private int rand12; //amount of ammo
        private int rand13; //damage
        private int rand14; //knockback
        private double rand15; //reload time
        private int rand16; //reload time

        // Used for class cards to set max random
        public int mSpeed = 2;
        public int grav = 1;
        public int jump = 1;
        public int r_health = 350;
        public int sDamageOver = 0;
        public double aSpeed = 1.0f;
        public int bounce = 0;
        public int numProj = 1;
        public int ammo = 3;
        public int damage = 100;
        public int knockback = 1;
        public int reloadTime = 4;

        private bool stats = false;
        private int count = 0;

        private int point;

        public int numCards = 0;



        public override void OnApply()
        {
            UnityEngine.Debug.Log($"OnApply");
        }

        public override void Reset()
        {
            UnityEngine.Debug.Log($"Reset");
        }

        public override CounterStatus UpdateCounter()
        {
            UnityEngine.Debug.Log($"UpdateCounter");

            if (stats && count == 0)
            {
                count = 1;
                return CounterStatus.Apply;
            }
            else if (!stats && count == 1)
            {
                count = 0;
                return CounterStatus.Remove;
            }
            else
            {
                return CounterStatus.Wait;
            }
        }


        public override void OnAwake()
        {
            UnityEngine.Debug.Log($"Awake");
            // Sets listener for when card pick ends
            GameModeManager.AddHook(GameModeHooks.HookPickEnd, OnPickEnd);
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, OnBattleEnd);
        }

        public override void OnStart()
        {
            UnityEngine.Debug.Log($"Start");

            base.gunStatModifier = new GunStatModifier();
            base.gunAmmoStatModifier = new GunAmmoStatModifier();
            base.characterStatModifiersModifier = new CharacterStatModifiersModifier();
            base.gravityModifier = new GravityModifier();
            base.blockModifier = new BlockModifier();
            base.characterDataModifier = new CharacterDataModifier();
            base.characterStatModifiersModifier = new CharacterStatModifiersModifier();
        }

        public override void OnUpdate()
        {
            //UnityEngine.Debug.Log($"OnUpdate");
        }

        public override void UpdateEffects()
        {
            UnityEngine.Debug.Log($"UpdateEffects");
            // Gets rounds that player has won to edit some random stats
            point = GameModeManager.CurrentHandler.GetTeamScore(player.teamID).points;

            // Doesn't have to sync if local play
            if (PhotonNetwork.OfflineMode)
            {
                // Sets initial random numbers
                rand1 = random.Next(mSpeed); //movement speed
                rand2 = random.NextDouble(); //gravity
                rand3 = random.Next(grav); //gravity
                rand4 = random.Next(jump); //jump
                rand5 = random.Next(r_health * (point + numCards)); //health
                rand6 = random.Next(sDamageOver); //seconds to take damage over
                rand7 = random.NextDouble(); //attack speed
                rand8 = random.Next(bounce); //bounces
                rand11 = random.Next(numProj); //number of projectiles
                rand12 = random.Next(ammo + (point + numCards)); //amount of ammo
                rand13 = random.Next(damage); //damage
                rand14 = random.Next(knockback); //knockback
                rand15 = random.NextDouble(); //reload time
                rand16 = random.Next(reloadTime); //reload time



                //******************************************
                // Sets stats for each enable random stat
                // Movement Speed
                if (enableSpeed)
                {
                    characterStatModifiersModifier.movementSpeed_add = (float)(rand1);
                }

                // Max Health
                if (enableHealth)
                {
                    characterDataModifier.maxHealth_add = rand5;
                }

                // Number of Jumps
                if (enableJump)
                {
                    characterStatModifiersModifier.numberOfJumps_add = rand4;
                }

                // Gravity
                if (enableGrav)
                {
                    characterStatModifiersModifier.gravity_add = (float)(rand2 + rand3);
                }

                // Seconds to take Damage Over
                if (enableDamageOver)
                {
                    characterStatModifiersModifier.secondsToTakeDamageOver_add = rand6;
                }

                // Attack Speed
                if (enableAtSpeed)
                {
                    gunStatModifier.attackSpeedMultiplier_mult = (float)rand7;
                }

                // Bounces
                if (enableBounce)
                {
                    gunStatModifier.reflects_add = rand8;
                }

                // Number of Projectiles
                if (enableProj)
                {
                    gunStatModifier.numberOfProjectiles_add = rand11;
                }

                // Ammo
                if (enableAmmo)
                {
                    gunAmmoStatModifier.currentAmmo_add = rand12;
                }

                // Damage
                if (enableDamage)
                {
                    gunStatModifier.damage_add = (float)(rand13);
                }

                // Knockback
                if (enableKnockback)
                {
                    gunStatModifier.knockback_add = rand14;
                }

                // Reload Time
                if (enableRelTime)
                {
                    gunAmmoStatModifier.reloadTimeAdd_add = (float)(rand15 + rand16);
                }
            }

            //Via Network (syncs random numbers across clients so that they don't differ)
            else if (this.player.GetComponent<PhotonView>().IsMine)
            {
                // Generate random numbers in client and transfer them to all
                this.gameObject.GetComponent<PhotonView>().RPC("RPCA_RandomStats", RpcTarget.All, new object[]
                {
                    random.Next(mSpeed), //movement speed
                    random.NextDouble(), //gravity
                    random.Next(grav), //gravity
                    random.Next(jump), //jump
                    random.Next(r_health * (point + numCards)), //health
                    random.Next(sDamageOver), //seconds to take damage over
                    random.NextDouble(), //attack speed
                    random.Next(bounce), //bounces
                    random.Next(numProj), //number of projectiles
                    random.Next(ammo + (point + numCards)), //amount of ammo
                    random.Next(damage), //damage
                    random.NextDouble(),
                    random.Next(knockback), //knockback
                    random.NextDouble(), //reload time
                    random.Next(reloadTime) //reload time
                });
            }
        }

        public IEnumerator OnPickEnd(IGameModeHandler gm)
        {
            UnityEngine.Debug.Log($"OnPickEnd");
            // Sets stats after all players have selected cards
            stats = true;

            yield break;
        }

        // Referenced whenever battle ends
        public IEnumerator OnBattleEnd(IGameModeHandler gm)
        {
            UnityEngine.Debug.Log($"OnBattleEnd");
            // Resets stats on battle end
            stats = false;

            yield break;
        }



        [PunRPC]
        private void RPCA_RandomStats(int srand1, double srand2, int srand3, int srand4, int srand5, int srand6, double srand7, int srand8, int srand11, int srand12, int srand13, int srand14, double srand15, int srand16)
        {
            UnityEngine.Debug.Log($"RPCA");

            // Bookkeeping random stats to revert them on round end
            rand1 = srand1;
            rand2 = srand2;
            rand3 = srand3;
            rand4 = srand4;
            rand5 = srand5;
            rand6 = srand6;
            rand7 = srand7;
            rand8 = srand8;
            rand11 = srand11;
            rand12 = srand12;
            rand13 = srand13;
            rand14 = srand14;
            rand15 = srand15;
            rand16 = srand16;



            //******************************************
            // Sets stats for each enable random stat

            if (enableSpeed)
            {
                characterStatModifiersModifier.movementSpeed_add = (float)(rand1);
            }

            // Max Health
            if (enableHealth)
            {
                characterDataModifier.maxHealth_add = rand5;
            }

            // Number of Jumps
            if (enableJump)
            {
                characterStatModifiersModifier.numberOfJumps_add = rand4;
            }

            // Gravity
            if (enableGrav)
            {
                characterStatModifiersModifier.gravity_add = (float)(rand2 + rand3);
            }

            // Seconds to take Damage Over
            if (enableDamageOver)
            {
                characterStatModifiersModifier.secondsToTakeDamageOver_add = rand6;
            }

            // Attack Speed
            if (enableAtSpeed)
            {
                gunStatModifier.attackSpeedMultiplier_mult = (float)rand7;
            }

            // Bounces
            if (enableBounce)
            {
                gunStatModifier.reflects_add = rand8;
            }

            // Number of Projectiles
            if (enableProj)
            {
                gunStatModifier.numberOfProjectiles_add = rand11;
            }

            // Ammo
            if (enableAmmo)
            {
                gunAmmoStatModifier.currentAmmo_add = rand12;
            }

            // Damage
            if (enableDamage)
            {
                gunStatModifier.damage_add = (float)(rand13);
            }

            // Knockback
            if (enableKnockback)
            {
                gunStatModifier.knockback_add = rand14;
            }

            // Reload Time
            if (enableRelTime)
            {
                gunAmmoStatModifier.reloadTimeAdd_add = (float)(rand15 + rand16);
            }
        }
    }
}
