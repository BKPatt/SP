using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Cards;
using UnboundLib;
using ModdingUtils.Utils;
using SeniorProject.Cards;
using UnboundLib.GameModes;
using ModdingUtils.GameModes;
using SeniorProject.Interfaces;
using System.Collections;
using Photon.Pun;
using System.Linq;

namespace SeniorProject.MonoBehaviours
{
    class KingPointAndCard : MonoBehaviour
    {
        // Used later to set a bunch of random stats
        private System.Random random = new System.Random();

        // Disabled for later testing

        // Used to determine if random stats should be added or subtracted
        /*private int rand01; 
        private int rand03; 
        private int rand04; 
        private int rand05; 
        private int rand06;
        private int rand08; 
        private int rand011; 
        private int rand012; 
        private int rand013; 
        private int rand014; 
        private int rand016; 

        // Used to set likelihood that stats are set positive or negative
        public int setR1 = 2;
        public int setR3 = 2;
        public int setR4 = 2;
        public int setR5 = 2;
        public int setR6 = 2;
        public int setR8 = 2;
        public int setR11 = 2;
        public int setR12 = 2;
        public int setR13 = 2;
        public int setR14 = 2;
        public int setR16 = 2; */

        // Used to enable these features with class cards
        public bool enableSpeed = false;
        public bool enableGrav = false;
        public bool enableJump = false;
        public bool enableHealth = false;
        public bool enableDamageOver = false;
        public bool enableAtSpeed = false;
        public bool enableBounce = false;
        //public bool enableSpread = false;
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
        private double rand9; //spread
        private double rand10; //even spread
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
        private GunAmmo gunAmmo;
        private CharacterStatModifiers statModifiers;
        private Block block;
        private CharacterData data;

        private int point;

        public int numCards = 0;

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
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, OnBattleEnd);
        }

        public void Start()
        {
            // Set OnBlock to run when player blocks
            block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(block.BlockAction, new Action<BlockTrigger.BlockTriggerType>(OnBlock));
        }

        // Sets stats when card picking ends
        private void setStats()
        {
            // Calls on random stats to be generated
            setRandom();

            // Sets stats for each enable random stat
            if (enableSpeed)
            {
                //UnityEngine.Debug.Log($"Before MS");
                //UnityEngine.Debug.Log($"{ statModifiers.movementSpeed }");
                statModifiers.movementSpeed += (float)(rand1);
                //UnityEngine.Debug.Log($"{ statModifiers.movementSpeed }");
                //UnityEngine.Debug.Log($"After MS { statModifiers.movementSpeed }");
            }
            if (enableHealth)
            {
                //UnityEngine.Debug.Log($"Before Health");
                //UnityEngine.Debug.Log($"{ player.data.maxHealth }");
                player.data.maxHealth += rand5;
                //UnityEngine.Debug.Log($"{ player.data.maxHealth }");
                //UnityEngine.Debug.Log($"After Health");
            }
            if (enableJump)
            {
                //UnityEngine.Debug.Log($"Before Jump");
                //UnityEngine.Debug.Log($"{ statModifiers.numberOfJumps }");
                statModifiers.numberOfJumps += rand4;
                //UnityEngine.Debug.Log($"{ statModifiers.numberOfJumps }");
                //UnityEngine.Debug.Log($"After Jump");
            }
            if (enableGrav)
            {
                //UnityEngine.Debug.Log($"Before Grav");
                //UnityEngine.Debug.Log($"{ statModifiers.gravity }");
                statModifiers.gravity += (float)(rand2 + rand3);
                //UnityEngine.Debug.Log($"{ statModifiers.gravity }");
                //UnityEngine.Debug.Log($"After Grav");
            }
            if (enableDamageOver)
            {
                //UnityEngine.Debug.Log($"Before STDO");
                //UnityEngine.Debug.Log($"{ statModifiers.secondsToTakeDamageOver }");
                statModifiers.secondsToTakeDamageOver += rand6;
                //UnityEngine.Debug.Log($"{ statModifiers.secondsToTakeDamageOver }");
                //UnityEngine.Debug.Log($"After STDO");
            }
            if (enableAtSpeed)
            {
                //UnityEngine.Debug.Log($"Before AS");
                //UnityEngine.Debug.Log($"{ player.data.stats.attackSpeedMultiplier }");
                player.data.stats.attackSpeedMultiplier *= (float)rand7;
                //UnityEngine.Debug.Log($"{ player.data.stats.attackSpeedMultiplier }");
                //UnityEngine.Debug.Log($"After AS");
                //gun.attackSpeed += (float)rand7;
            }
            if (enableBounce)
            {
                UnityEngine.Debug.Log($"Before Bounce");
                UnityEngine.Debug.Log($"{ gun.reflects }");
                gun.reflects += rand8;
                UnityEngine.Debug.Log($"{ gun.reflects }");
                UnityEngine.Debug.Log($"After Bounce");
                UnityEngine.Debug.Log($"Proj Speed: { gun.projectileSpeed }");
            }
            /*if (enableSpread)
            {
                UnityEngine.Debug.Log($"Before Spread");
                UnityEngine.Debug.Log($"{ gun.spread }");
                UnityEngine.Debug.Log($"{ gun.evenSpread }");
                gun.spread += (float)(rand9);
                gun.evenSpread += (float)(rand10);
                UnityEngine.Debug.Log($"{ gun.spread }");
                UnityEngine.Debug.Log($"{ gun.evenSpread }");
                UnityEngine.Debug.Log($"After Spread");
                UnityEngine.Debug.Log($"Proj Speed: { gun.projectileSpeed }");
            }*/
            if (enableProj)
            {
                UnityEngine.Debug.Log($"Before #Proj");
                UnityEngine.Debug.Log($"{ gun.numberOfProjectiles }");
                gun.numberOfProjectiles += rand11;
                UnityEngine.Debug.Log($"{ gun.numberOfProjectiles }");
                UnityEngine.Debug.Log($"After #Proj");
                UnityEngine.Debug.Log($"Proj Speed: { gun.projectileSpeed }");
            }
            if (enableAmmo)
            {
                //UnityEngine.Debug.Log($"Before Ammo");
                //UnityEngine.Debug.Log($"{ gunAmmo.maxAmmo }");
                gunAmmo.maxAmmo += rand12;
                //UnityEngine.Debug.Log($"{ gunAmmo.maxAmmo }");
                //UnityEngine.Debug.Log($"After Ammo");
            }
            if (enableDamage)
            {
                //UnityEngine.Debug.Log($"Before Damage");
                //UnityEngine.Debug.Log($"{ gun.damage }");
                //gun.damage = (rand13);
                gun.damage = (float)(rand13_1 + rand13);
                gun.projectileSize *= 0.9f;
                //UnityEngine.Debug.Log($"{ gun.damage }");
                //UnityEngine.Debug.Log($"After Damage");
            }
            if (enableKnockback)
            {
                //UnityEngine.Debug.Log($"Before Knockback");
                //UnityEngine.Debug.Log($"{ gun.knockback }");
                gun.knockback += rand14;
                //UnityEngine.Debug.Log($"{ gun.knockback }");
                //UnityEngine.Debug.Log($"After Knockback");
            }
            if (enableRelTime)
            {
                //UnityEngine.Debug.Log($"Before RelTime");
                //UnityEngine.Debug.Log($"{ gunAmmo.reloadTime }");
                gunAmmo.reloadTime += (float)(rand15 + rand16);
                //UnityEngine.Debug.Log($"{ gunAmmo.reloadTime }");
                //UnityEngine.Debug.Log($"After RelTime");
            }
            //gun.projectileSize = 1;
        }

        // Sets stats back to original when battle ends
        private void resetStats()
        {
            // Resets stats for each enabled random stat
            if (enableSpeed)
            {
                statModifiers.movementSpeed -= (float)(rand1);
            }
            if (enableHealth)
            {
                player.data.maxHealth -= rand5;
            }
            if (enableJump)
            {
                statModifiers.numberOfJumps -= rand4;
            }
            if (enableGrav)
            {
                statModifiers.gravity -= (float)(rand2 + rand3);
            }
            if (enableDamageOver)
            {
                statModifiers.secondsToTakeDamageOver -= rand6;
            }
            if (enableAtSpeed)
            {
                player.data.stats.attackSpeedMultiplier /= (float)rand7;
                //gun.attackSpeed -= (float)(rand7);
            }
            if (enableBounce)
            {
                gun.reflects -= rand8;
            }
            /*if (enableSpread)
            {
                gun.spread -= (float)(rand9);
                gun.evenSpread -= (float)(rand10);
            }*/
            if (enableProj)
            {
                gun.numberOfProjectiles -= (rand11 + point);
            }
            if (enableAmmo)
            {
                gunAmmo.maxAmmo -= rand12;
            }
            if (enableDamage)
            {
                //gun.damage = rand13;
                gun.damage -= (float)(rand13_1+rand13);
            }
            if (enableKnockback)
            {
                gun.knockback -= rand14;
            }
            if (enableRelTime)
            {
                gunAmmo.reloadTime -= (float)(rand15);
            }
        }

        private void setRandom()
        {
            // Gets rounds that player has won to edit some random stats
            int point = GameModeManager.CurrentHandler.GetTeamScore(player.teamID).points;

            // Disabled for later testing

            // Sets stats positive or negative based on number set by class cards
            /*rand01 = random.Next(setR1);
            rand03 = random.Next(setR3);
            rand04 = random.Next(setR4); 
            rand05 = random.Next(setR5); 
            rand06 = random.Next(setR6); 
            rand08 = random.Next(setR8); 
            rand011 = random.Next(setR11); 
            rand012 = random.Next(setR12); 
            rand013 = random.Next(setR13); 
            rand014 = random.Next(setR14); 
            rand016 = random.Next(setR16);*/

            // Sets initial random numbers
            rand1 = random.Next(mSpeed); //movement speed
            rand2 = random.NextDouble(); //gravity
            rand3 = random.Next(grav); //gravity
            rand4 = random.Next(jump); //jump
            rand5 = random.Next(health * (point + numCards)); //health
            rand6 = random.Next(sDamageOver); //seconds to take damage over
            rand7 = random.NextDouble(); //attack speed
            rand8 = random.Next(bounce); //bounces
            rand9 = random.NextDouble(); //spread
            rand10 = random.NextDouble(); //even spread
            rand11 = random.Next(numProj); //number of projectiles
            rand12 = random.Next(ammo + (point + numCards)); //amount of ammo
            rand13 = random.Next(damage); //damage
            rand13_1 = random.NextDouble();
            rand14 = random.Next(knockback); //knockback
            rand15 = random.NextDouble(); //reload time
            rand16 = random.Next(reloadTime); //reload time


            // Disbaled for later testing

            // Sees if the number should be subtracted, but just sets to 0 if it is trying to subtract more than the player has
            // Movement speed
            /*(if (rand01 == 0)
            {
                if (player.data.stats.movementSpeed > rand1)
                {
                    rand1 *= -1;
                }
                else
                {
                    rand1 = 0;
                }
            }
            // Gravity
            if (rand03 == 0)
            {
                if (player.data.stats.gravity > (rand2 + rand3))
                {
                    rand2 *= -1;
                    rand3 *= -1;
                }
                else
                {
                    rand2 = 0.0f;
                    rand3 = 0;
                }
            }
            // Jumps
            if (rand04 == 0)
            {
                if (player.data.stats.numberOfJumps > rand4)
                {
                    rand4 *= -1;
                }
                else
                {
                    rand4 = 0;
                }
            }
            // Health
            if (rand05 == 0)
            {
                if (player.data.maxHealth > rand05)
                {
                    rand5 *= -1;
                }
                else
                {
                    rand5 = 0;
                }
            }
            // Seconds to take damage over
            if (rand06 == 0)
            {
                if (player.data.stats.secondsToTakeDamageOver > rand6)
                {
                    rand6 *= -1;
                }
                else
                {
                    rand6 = 0;
                }
            }
            // Bounces
            if (rand08 == 0)
            {
                if (gun.reflects > rand8)
                {
                    rand8 *= -1;
                }
                else
                {
                    rand8 = 0;
                }
            }
            // Number of Projectiles
            if (rand011 == 0)
            {
                if (gun.numberOfProjectiles > rand11)
                {
                    rand11 *= -1;
                }
                else
                {
                    rand11 = 0;
                }
            }
            // Ammo
            if (rand012 == 0)
            {
                if (gun.ammo > rand12)
                {
                    rand12 *= -1;
                }
                else
                {
                    rand12 = 0;
                }
            }
            // Damage
            if (rand013 == 0)
            {
                if (gun.damage > rand13_1)
                {
                    rand13_1 *= -1;
                }
                else
                {
                    rand13_1 = 0;
                }
            }
            // Knockback
            if (rand014 == 0)
            {
                if (gun.knockback > rand14)
                {
                    rand14 *= -1;
                }
                else
                {
                    rand14 = 0;
                }
            }
            // Reload Time
            if (rand016 == 0)
            {
                if (gun.reloadTime > rand16)
                {
                    rand16 *= -1;
                }
                else
                {
                    rand16 = 0;
                }
            }*/

        }

        // Runs when player blocks
        private void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            UnityEngine.Debug.Log($"Block");
        }

        // Referenced whenever card pick ends
        public IEnumerator OnPickEnd(IGameModeHandler gm)
        {
            // Sets stats after all players have selected cards
            setStats();

            yield break;
        }

        // Referenced whenever battle ends
        public IEnumerator OnBattleEnd(IGameModeHandler gm)
        {
            // Resets stats on battle end
            resetStats();

            yield break;
        }
    }
}
