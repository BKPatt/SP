using UnityEngine;
using UnboundLib.GameModes;
using System.Collections;
using Photon.Pun;

namespace SeniorProject.MonoBehaviours
{
    class KingPointAndCard : MonoBehaviour
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
        private double rand13_1;
        private int rand14; //knockback
        private double rand15; //reload time
        private int rand16; //reload time

        // Used for class cards to set max random
        public int mSpeed = 2;
        public int grav = 1;
        public int jump = 1;
        public int health = 100;
        public int sDamageOver = 0;
        public double aSpeed = 1.0f;
        public int bounce = 0;
        public int numProj = 1;
        public int ammo = 3;
        public int damage = 1;
        public int knockback = 1;
        public int reloadTime = 4;

        // Get player stats to edit them
        private Player player;
        private Gun gun;
        private CharacterStatModifiers statModifiers;

        // Used to tell how many Rounds the player has won
        private int point;

        // Used to tell the number of class cards the player has
        public int numCards = 0;

        private bool debug_l = false;


        public void Awake()
        {
            //*****************************************************************************
            // Debugging
            if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"Begin Awake");
            }
            //*****************************************************************************



            // Gets player data
            player = this.gameObject.GetComponentInParent<Player>();
            statModifiers = this.gameObject.GetComponentInParent<CharacterStatModifiers>();
            gun = this.player.GetComponent<Holding>().holdable.GetComponent<Gun>();

            // Sets listener for when card pick ends and Round ends
            GameModeManager.AddHook(GameModeHooks.HookPickEnd, OnPickEnd);
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, OnBattleEnd);



            //*****************************************************************************
            // Debugging
            if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"End Awake");
            }
            //*****************************************************************************
        }

        // Sets stats when card picking ends
        private void setStats()
        {
            //*****************************************************************************
            // Debugging
            if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"Begin setStats");
            }
            //*****************************************************************************



            // Calls on random stats to be generated
            setRandom();



            //*****************************************************************************
            // Debugging
            if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"End setStats");
            }
            //*****************************************************************************
        }

        // Sets stats back to original when battle ends
        private void resetStats()
        {
            //*****************************************************************************
            // Debugging
            if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"Begin resetStats");
            }
            //*****************************************************************************



            // Resets stats for each enabled random stat
            // Movement Speed
            if (enableSpeed)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before Movement Speed Reset: { statModifiers.movementSpeed }");
                }
                //*****************************************************************************



                // Modifiers
                statModifiers.movementSpeed -= (float)(rand1);



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After Movement Speed Reset: { statModifiers.movementSpeed }");
                }
                //*****************************************************************************
            }

            // Max Health
            if (enableHealth)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before Max Health Reset: { player.data.maxHealth }");
                }
                //*****************************************************************************



                // Modifiers
                player.data.maxHealth -= rand5;



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After Max Health Reset: { player.data.maxHealth }");
                }
                //*****************************************************************************
            }

            // Number of Jumps
            if (enableJump)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before Num Jump Reset: { statModifiers.numberOfJumps }");
                }
                //*****************************************************************************



                // Modifiers
                statModifiers.numberOfJumps -= rand4;



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After Num Jump Reset: { statModifiers.numberOfJumps }");
                }
                //*****************************************************************************
            }

            // Gravity
            if (enableGrav)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before Gravity Reset: { statModifiers.gravity }");
                }
                //*****************************************************************************



                // Modifiers
                statModifiers.gravity -= (float)(rand2 + rand3);



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After Gravity Reset: { statModifiers.gravity }");
                }
                //*****************************************************************************
            }

            // Seconds to take Damage Over
            if (enableDamageOver)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before S Damage Over Reset: { statModifiers.secondsToTakeDamageOver }");
                }
                //*****************************************************************************



                // Modifiers
                statModifiers.secondsToTakeDamageOver -= rand6;
                
                
                
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After S Damage Over Reset: { statModifiers.secondsToTakeDamageOver }");
                }
                //*****************************************************************************
            }

            // Attack Speed
            if (enableAtSpeed)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before At Speed Mult: { player.data.stats.attackSpeedMultiplier }");
                }
                //*****************************************************************************



                // Modifiers
                player.data.stats.attackSpeedMultiplier /= (float)rand7;



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After At Speed Mult: { player.data.stats.attackSpeedMultiplier }");
                }
                //*****************************************************************************
            }

            // Bounces
            if (enableBounce)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before Bounce Reset: { gun.reflects }");
                }
                //*****************************************************************************



                // Modifiers
                gun.reflects -= rand8;



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After Bounce Reset: { gun.reflects }");
                }
                //*****************************************************************************
            }

            // Number of Projectiles
            if (enableProj)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before Num Proj Reset: { gun.numberOfProjectiles }");
                }
                //*****************************************************************************



                // Modifiers
                gun.numberOfProjectiles -= (rand11 + point);



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After Num Proj Reset: { gun.numberOfProjectiles }");
                }
                //*****************************************************************************
            }

            // Ammo
            if (enableAmmo)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before Ammo Reset: { gun.ammo }");
                }
                //*****************************************************************************



                // Modifiers
                gun.ammo -= rand12;



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After Ammo Reset: { gun.ammo }");
                }
                //*****************************************************************************
            }

            // Damage
            if (enableDamage)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before Damage Reset: { gun.damage }");
                }
                //*****************************************************************************



                // Modifiers
                gun.damage -= (float)(rand13_1+rand13);



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After Damage Reset: { gun.damage }");
                }
                //*****************************************************************************
            }

            // Knockback
            if (enableKnockback)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before Knockback Reset: { gun.knockback }");
                }
                //*****************************************************************************



                // Modifiers
                gun.knockback -= rand14;



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After Knockback Reset: { gun.knockback }");
                }
                //*****************************************************************************
            }

            // Reload Time
            if (enableRelTime)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before Reload Time Reset: { gun.reloadTime }");
                }
                //*****************************************************************************



                // Modifiers
                gun.reloadTime -= (float)(rand15);



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After Reload Time Reset: { gun.reloadTime }");
                }
                //*****************************************************************************
            }



            //*****************************************************************************
            // Debugging
            if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"End resetStats");
            }
            //*****************************************************************************
        }

        private void setRandom()
        {
            //*****************************************************************************
            // Debugging
            if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"Begin setRandom");
            }
            //*****************************************************************************



            // Gets rounds that player has won to edit some random stats
            int point = GameModeManager.CurrentHandler.GetTeamScore(player.teamID).points;

            // Doesn't have to sync if local play
            if (PhotonNetwork.OfflineMode)
            {
                // Sets initial random numbers
                rand1 = random.Next(mSpeed); //movement speed
                rand2 = random.NextDouble(); //gravity
                rand3 = random.Next(grav); //gravity
                rand4 = random.Next(jump); //jump
                rand5 = random.Next(health * (point + numCards)); //health
                rand6 = random.Next(sDamageOver); //seconds to take damage over
                rand7 = random.NextDouble(); //attack speed
                rand8 = random.Next(bounce); //bounces
                rand11 = random.Next(numProj); //number of projectiles
                rand12 = random.Next(ammo + (point + numCards)); //amount of ammo
                rand13 = random.Next(damage); //damage
                rand13_1 = random.NextDouble();
                rand14 = random.Next(knockback); //knockback
                rand15 = random.NextDouble(); //reload time
                rand16 = random.Next(reloadTime); //reload time



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"rand1: { rand1 }");
                    UnityEngine.Debug.Log($"rand2: { rand2 }");
                    UnityEngine.Debug.Log($"rand3: { rand3 }");
                    UnityEngine.Debug.Log($"rand4: { rand4 }");
                    UnityEngine.Debug.Log($"rand5: { rand5 }");
                    UnityEngine.Debug.Log($"rand6: { rand6 }");
                    UnityEngine.Debug.Log($"rand7: { rand7 }");
                    UnityEngine.Debug.Log($"rand8: { rand8 }");
                    UnityEngine.Debug.Log($"rand11: { rand11 }");
                    UnityEngine.Debug.Log($"rand12: { rand12 }");
                    UnityEngine.Debug.Log($"rand13: { rand13 }");
                    UnityEngine.Debug.Log($"rand13_1: { rand13_1 }");
                    UnityEngine.Debug.Log($"rand14: { rand14 }");
                    UnityEngine.Debug.Log($"rand15: { rand15 }");
                    UnityEngine.Debug.Log($"rand16: { rand16 }");
                }
                //*****************************************************************************



                // Sets stats for each enable random stat

                // Movement Speed
                if (enableSpeed)
                {
                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"Before Movement Speed: { statModifiers.movementSpeed }");
                    }
                    //*****************************************************************************



                    // Modifiers
                    statModifiers.movementSpeed += (float)(rand1);



                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"After Movement Speed: { statModifiers.movementSpeed }");
                    }
                    //*****************************************************************************
                }

                // Max Health
                if (enableHealth)
                {
                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"Before maxHealth: { player.data.maxHealth }");
                    }
                    //*****************************************************************************



                    // Modifiers
                    player.data.maxHealth += rand5;



                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"After maxHealth { player.data.maxHealth }");
                    }
                }

                // Number of Jumps
                if (enableJump)
                {
                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"Before num jumps: { statModifiers.numberOfJumps }");
                    }
                    //*****************************************************************************



                    // Modifiers
                    statModifiers.numberOfJumps += rand4;



                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"After num jumps { statModifiers.numberOfJumps }");
                    }
                    //*****************************************************************************
                }

                // Gravity
                if (enableGrav)
                {
                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"Before Gravity: { statModifiers.gravity }");
                    }
                    //*****************************************************************************



                    // Modifiers
                    statModifiers.gravity += (float)(rand2 + rand3);



                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"After Gravity: { statModifiers.gravity }");
                    }
                    //*****************************************************************************
                }

                // Seconds to take Damage Over
                if (enableDamageOver)
                {
                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"Before sec Damage Over: { statModifiers.secondsToTakeDamageOver }");
                    }
                    //*****************************************************************************



                    // Modifiers
                    statModifiers.secondsToTakeDamageOver += rand6;



                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"After sec Damage Over: { statModifiers.secondsToTakeDamageOver }");
                    }
                    //*****************************************************************************
                }

                // Attack Speed
                if (enableAtSpeed)
                {
                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"Before AS Multiplier: { player.data.stats.attackSpeedMultiplier }");
                    }
                    //*****************************************************************************



                    // Modifiers
                    player.data.stats.attackSpeedMultiplier *= (float)rand7;



                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"After AS Multiplier: { player.data.stats.attackSpeedMultiplier }");
                    }
                    //*****************************************************************************
                }

                // Bounces
                if (enableBounce)
                {
                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"Before Bounces: { gun.reflects }");
                    }
                    //*****************************************************************************



                    // Modifiers
                    gun.reflects += rand8;



                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"After Bounces: { gun.reflects }");
                    }
                    //*****************************************************************************
                }

                // Number of Projectiles
                if (enableProj)
                {
                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"Before num proj: { gun.numberOfProjectiles }");
                    }
                    //*****************************************************************************



                    // Modifiers
                    gun.numberOfProjectiles += rand11;



                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"After num proj: { gun.numberOfProjectiles }");
                    }
                    //*****************************************************************************
                }

                // Ammo
                if (enableAmmo)
                {
                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"Before Ammo: { gun.ammo }");
                    }
                    //*****************************************************************************



                    // Modifiers
                    gun.ammo += rand12;



                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"After Ammo: { gun.ammo }");
                    }
                    //*****************************************************************************
                }

                // Damage
                if (enableDamage)
                {
                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"Before Damage: { gun.damage }");
                    }
                    //*****************************************************************************



                    // Modifiers
                    gun.damage = (float)(rand13_1 + rand13);
                    gun.projectileSize *= 0.9f;



                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"After Damage: { gun.damage }");
                    }
                    //*****************************************************************************
                }

                // Knockback
                if (enableKnockback)
                {
                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"Before knockback: { gun.knockback }");
                    }
                    //*****************************************************************************



                    // Modifiers
                    gun.knockback += rand14;



                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"After knockback: { gun.knockback }");
                    }
                    //*****************************************************************************
                }

                // Reload Time
                if (enableRelTime)
                {
                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"Before Reload Time: { gun.reloadTime }");
                    }
                    //*****************************************************************************



                    // Modifiers
                    gun.reloadTime += (float)(rand15 + rand16);



                    //*****************************************************************************
                    // Debugging
                    if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                    {
                        UnityEngine.Debug.Log($"After Reload Time: { gun.reloadTime }");
                    }
                    //*****************************************************************************
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
                    random.Next(health * (point + numCards)), //health
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
            //*****************************************************************************
            // Debugging
            if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"End setRandom");
            }
            //*****************************************************************************
        }

        [PunRPC]
        private void RPCA_RandomStats(int srand1, double srand2, int srand3, int srand4, int srand5, int srand6, double srand7, int srand8, int srand11, int srand12, int srand13, double srand13_1, int srand14, double srand15, int srand16)
        {
            //*****************************************************************************
            // Debugging
            if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"Begin RPCA_RandomStats");
            }
            //*****************************************************************************



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
            rand13_1 = srand13_1;
            rand14 = srand14;
            rand15 = srand15;
            rand16 = srand16;

            // Debugging
            if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"rand1: { rand1 }");
                UnityEngine.Debug.Log($"rand2: { rand2 }");
                UnityEngine.Debug.Log($"rand3: { rand3 }");
                UnityEngine.Debug.Log($"rand4: { rand4 }");
                UnityEngine.Debug.Log($"rand5: { rand5 }");
                UnityEngine.Debug.Log($"rand6: { rand6 }");
                UnityEngine.Debug.Log($"rand7: { rand7 }");
                UnityEngine.Debug.Log($"rand8: { rand8 }");
                UnityEngine.Debug.Log($"rand11: { rand11 }");
                UnityEngine.Debug.Log($"rand12: { rand12 }");
                UnityEngine.Debug.Log($"rand13: { rand13 }");
                UnityEngine.Debug.Log($"rand13_1: { rand13_1 }");
                UnityEngine.Debug.Log($"rand14: { rand14 }");
                UnityEngine.Debug.Log($"rand15: { rand15 }");
                UnityEngine.Debug.Log($"rand16: { rand16 }");
            }

            // Sets stats for each enable random stat
            // Movement Speed
            if (enableSpeed)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before Movement Speed: { statModifiers.movementSpeed }");
                }
                //*****************************************************************************



                // Modifiers
                statModifiers.movementSpeed += (float)(srand1);



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After Movement Speed: { statModifiers.movementSpeed }");
                }
                //*****************************************************************************
            }
            // Max Health
            if (enableHealth)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before maxHealth: { player.data.maxHealth }");
                }
                //*****************************************************************************



                // Modifiers
                player.data.maxHealth += srand5;



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After maxHealth { player.data.maxHealth }");
                }
                //*****************************************************************************
            }
            // Number of Jumps
            if (enableJump)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before num jumps: { statModifiers.numberOfJumps }");
                }
                //*****************************************************************************



                // Modifiers
                statModifiers.numberOfJumps += srand4;



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After num jumps { statModifiers.numberOfJumps }");
                }
                //*****************************************************************************
            }
            // Gravity
            if (enableGrav)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before Gravity: { statModifiers.gravity }");
                }
                //*****************************************************************************



                // Modifiers
                statModifiers.gravity += (float)(srand2 + srand3);



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After Gravity: { statModifiers.gravity }");
                }
                //*****************************************************************************
            }
            // Seconds to take Damage Over
            if (enableDamageOver)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before sec Damage Over: { statModifiers.secondsToTakeDamageOver }");
                }
                //*****************************************************************************



                // Modifiers
                statModifiers.secondsToTakeDamageOver += srand6;



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After sec Damage Over: { statModifiers.secondsToTakeDamageOver }");
                }
                //*****************************************************************************
            }
            // Attack Speed
            if (enableAtSpeed)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before AS Multiplier: { player.data.stats.attackSpeedMultiplier }");
                }
                //*****************************************************************************



                // Modifiers
                player.data.stats.attackSpeedMultiplier *= (float)srand7;



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After AS Multiplier: { player.data.stats.attackSpeedMultiplier }");
                }
                //*****************************************************************************
            }
            // Bounces
            if (enableBounce)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before Bounces: { gun.reflects }");
                }
                //*****************************************************************************



                // Modifiers
                gun.reflects += srand8;



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After Bounces: { gun.reflects }");
                }
                //*****************************************************************************
            }
            // Number of Projectiles
            if (enableProj)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before num proj: { gun.numberOfProjectiles }");
                }
                //*****************************************************************************



                // Modifiers
                gun.numberOfProjectiles += srand11;



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After num proj: { gun.numberOfProjectiles }");
                }
                //*****************************************************************************
            }
            // Ammo
            if (enableAmmo)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before Ammo: { gun.ammo }");
                }
                //*****************************************************************************



                // Modifiers
                gun.ammo += srand12;



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After Ammo: { gun.ammo }");
                }
                //*****************************************************************************
            }
            // Damage
            if (enableDamage)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before Damage: { gun.damage }");
                }
                //*****************************************************************************



                // Modifiers
                gun.damage = (float)(srand13_1 + srand13);
                gun.projectileSize *= 0.9f;



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After Damage: { gun.damage }");
                }
                //*****************************************************************************
            }
            // Knockback
            if (enableKnockback)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before knockback: { gun.knockback }");
                }
                //*****************************************************************************



                // Modifiers
                gun.knockback += srand14;



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After knockback: { gun.knockback }");
                }
                //*****************************************************************************
            }
            // Reload Time
            if (enableRelTime)
            {
                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"Before Reload Time: { gun.reloadTime }");
                }
                //*****************************************************************************



                // Modifiers
                gun.reloadTime += (float)(srand15 + srand16);



                //*****************************************************************************
                // Debugging
                if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
                {
                    UnityEngine.Debug.Log($"After Reload Time: { gun.reloadTime }");
                }
                //*****************************************************************************
            }
        }

        // Referenced whenever card pick ends
        public IEnumerator OnPickEnd(IGameModeHandler gm)
        {
            //*****************************************************************************
            // Debugging
            if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"Run OnPickEnd");
            }
            //*****************************************************************************



            // Sets stats after all players have selected cards
            setStats();



            //*****************************************************************************
            // Debugging
            if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"After OnPickEnd");
            }
            //*****************************************************************************

            yield break;
        }

        // Referenced whenever battle ends
        public IEnumerator OnBattleEnd(IGameModeHandler gm)
        {
            //*****************************************************************************
            // Debugging
            if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"Run OnBattleEnd");
            }
            //*****************************************************************************



            // Resets stats on battle end
            resetStats();



            //*****************************************************************************
            // Debugging
            if (debug_l || SeniorProject.debug_am || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log($"End OnBattleEnd");
            }
            //*****************************************************************************

            yield break;
        }
    }
}
