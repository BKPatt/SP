using System;
using ModdingUtils.MonoBehaviours;
using UnboundLib.GameModes;
using System.Collections;
using Photon.Pun;

namespace SeniorProject.MonoBehaviours
{
    class KingPointAndCard : CounterReversibleEffect
    {
        // Used later to set a bunch of random stats
        private Random random = new Random();

        // Used to enable these features with class cards
        public bool enableSpeed = false;
        public bool enableGrav = false;
        public bool enableJump = false;
        public bool enableHealth = false;
        public bool enableDamageOver = false;
        public bool enableAtSpeed = false;
        public bool enableBounce = false;
        //public bool enableProj = false;
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
        //private int rand11; //number of projectiles
        private int rand12; //amount of ammo
        private int rand13; //damage
        private double rand13_1;
        private int rand14; //knockback
        private double rand15; //reload time
        private int rand16; //reload time

        // Used for class cards to set max random
        public int mSpeed = 2;
        public int grav = 1;
        public int jump = 5;
        public int r_health = 150;
        public int sDamageOver = 1;
        public double aSpeed = 1.0f;
        public int bounce = 5;
        //public int numProj = 5;
        public int ammo = 5;
        public int damage = 2;
        public int knockback = 2;
        public int reloadTime = 4;

        // Variables that make sure UpdateCounter only runs at the beginning
        private bool stats = false;
        private int count = 0;

        // Variables for additional stats per point and card
        private int point;
        public int numCards = 0;

        public override void OnApply()
        {
            //Heals player for amount of health added after applying
            this.player.data.healthHandler.Heal(rand5);
        }

        public override void Reset()
        {
            
        }

        public override CounterStatus UpdateCounter()
        {
            // Runs at the beginning of the round
            if (stats && count == 0)
            {
                count = 1;
                return CounterStatus.Apply;
            }
            else
            {
                return CounterStatus.Wait;
            }
        }

        public override void OnAwake()
        {
            // Sets listener for when battle begins and ends
            GameModeManager.AddHook(GameModeHooks.HookBattleStart, OnPointStart);
            GameModeManager.AddHook(GameModeHooks.HookPointEnd, OnPointEnd);
        }

        public override void UpdateEffects()
        {
            // Gets rounds that player has won to edit some random stats
            point = GameModeManager.CurrentHandler.GetTeamScore(player.teamID).points;

            // Doesn't have to sync if local play
            if (PhotonNetwork.OfflineMode)
            {
                setRandom();
                setStats();
            }
            //Via Network (syncs random numbers across clients so that they don't differ)
            else if (this.player.GetComponent<PhotonView>().IsMine)
            {
                setRandom();

                // Generate random numbers in client and transfer them to all
                this.gameObject.GetComponent<PhotonView>().RPC("RPCA_RandomStats", RpcTarget.All, new object[]
                {
                    rand1, //movement speed
                    rand2, //gravity
                    rand3, //gravity
                    rand4, //jump
                    rand5, //health
                    rand6, //seconds to take damage over
                    rand7, //attack speed
                    rand8, //bounces
                    //rand11, //number of projectiles
                    rand12, //amount of ammo
                    rand13, //damage
                    rand13_1, //floating point damage
                    rand14, //knockback
                    rand15, //reload time
                    rand16 //reload time
            });
            }
        }

        [PunRPC]
        private void RPCA_RandomStats(int srand1, double srand2, int srand3, int srand4, int srand5, int srand6, double srand7, int srand8, /*int srand11,*/ int srand12, int srand13, double srand13_1, int srand14, double srand15, int srand16)
        {
            // Sets it to global variable so that local play and network play don't repeat setStats code
            rand1 = srand1;
            rand2 = srand2;
            rand3 = srand3;
            rand4 = srand4;
            rand5 = srand5;
            rand6 = srand6;
            rand7 = srand7;
            rand8 = srand8;
            //rand11 = srand11;
            rand12 = srand12;
            rand13 = srand13;
            rand13_1 = srand13_1;
            rand14 = srand14;
            rand15 = srand15;
            rand16 = srand16;


            // Sets stats for each enable random stat
            setStats();
        }

        private void setRandom()
        {
            // Sets initial random numbers
            rand1 = random.Next(mSpeed); //movement speed
            rand2 = random.NextDouble(); //gravity
            rand3 = random.Next(grav); //gravity
            rand4 = random.Next(jump); //jump
            rand5 = random.Next(r_health * ((point + numCards) / 2)); //health
            rand6 = random.Next(sDamageOver); //seconds to take damage over
            rand7 = random.NextDouble(); //attack speed
            rand8 = random.Next(bounce); //bounces
            //rand11 = random.Next(numProj); //number of projectiles
            rand12 = random.Next(ammo); //amount of ammo
            rand13 = random.Next(damage + ((point + numCards) / 2)); //damage
            rand14 = random.Next(knockback); //knockback
            rand15 = random.NextDouble(); //reload time
            rand16 = random.Next(reloadTime); //reload time
        }

        private void setStats()
        {
            // Movement Speed
            if (enableSpeed)
            {
                characterStatModifiersModifier.movementSpeed_add += (float)(rand1);
            }
            // Max Health
            if (enableHealth)
            {
                characterDataModifier.maxHealth_add = rand5;
            }
            // Number of Jumps
            if (enableJump)
            {
                characterStatModifiersModifier.numberOfJumps_add += rand4;
            }
            // Gravity
            if (enableGrav)
            {
                characterStatModifiersModifier.gravity_add += (float)(rand2 + rand3);
            }
            // Seconds to take Damage Over
            if (enableDamageOver)
            {
                characterStatModifiersModifier.secondsToTakeDamageOver_add += rand6;
            }
            // Attack Speed
            if (enableAtSpeed)
            {
                gunStatModifier.attackSpeedMultiplier_mult *= (float)rand7;
            }
            // Bounces
            if (enableBounce)
            {
                gunStatModifier.reflects_add += rand8;
            }
            // Number of Projectiles
            /*if (enableProj)
            {
                gunStatModifier.numberOfProjectiles_add += rand11;
            }*/
            // Ammo
            if (enableAmmo)
            {
                gunAmmoStatModifier.maxAmmo_add += rand12;
            }
            // Damage
            if (enableDamage)
            {
                gunStatModifier.damage_add += (float)(rand13 + rand13_1);
            }
            // Knockback
            if (enableKnockback)
            {
                gunStatModifier.knockback_add += rand14;
            }
            // Reload Time
            if (enableRelTime)
            {
                gunAmmoStatModifier.reloadTimeAdd_add += (float)(rand15 + rand16);
            }
        }

        public IEnumerator OnPointStart(IGameModeHandler gm)
        {
            // Sets stats after all players have selected cards
            stats = true;

            yield break;
        }

        // Referenced whenever battle ends
        public IEnumerator OnPointEnd(IGameModeHandler gm)
        {
            // Resets stats on battle end
            stats = false;
            count = 0;

            yield break;
        }
    }
}