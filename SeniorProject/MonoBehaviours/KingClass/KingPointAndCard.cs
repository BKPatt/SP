using UnityEngine;
using UnboundLib.GameModes;
using System.Collections;

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
        //private double rand9; //spread
        //private double rand10; //even spread
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

        private int point;

        public int numCards = 0;

        public void Awake()
        {
            // Gets player data
            player = this.gameObject.GetComponentInParent<Player>();
            statModifiers = this.gameObject.GetComponentInParent<CharacterStatModifiers>();
            gun = this.player.GetComponent<Holding>().holdable.GetComponent<Gun>();

            // Sets listener for when card pick ends
            GameModeManager.AddHook(GameModeHooks.HookPickEnd, OnPickEnd);
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, OnBattleEnd);
        }

        // Sets stats when card picking ends
        private void setStats()
        {
            // Calls on random stats to be generated
            setRandom();

            // Sets stats for each enable random stat
            if (enableSpeed)
            {
                statModifiers.movementSpeed += (float)(rand1);
            }
            if (enableHealth)
            {
                player.data.maxHealth += rand5;
            }
            if (enableJump)
            {
                statModifiers.numberOfJumps += rand4;
            }
            if (enableGrav)
            {
                statModifiers.gravity += (float)(rand2 + rand3);
            }
            if (enableDamageOver)
            {
                statModifiers.secondsToTakeDamageOver += rand6;
            }
            if (enableAtSpeed)
            {
                player.data.stats.attackSpeedMultiplier *= (float)rand7;
            }
            if (enableBounce)
            {
                gun.reflects += rand8;
            }
            if (enableProj)
            {
                gun.numberOfProjectiles += rand11;
            }
            if (enableAmmo)
            {
                gun.ammo += rand12;
            }
            if (enableDamage)
            {
                gun.damage = (float)(rand13_1 + rand13);
                gun.projectileSize *= 0.9f;
            }
            if (enableKnockback)
            {
                gun.knockback += rand14;
            }
            if (enableRelTime)
            {
                gun.reloadTime += (float)(rand15 + rand16);
            }
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
            if (enableProj)
            {
                gun.numberOfProjectiles -= (rand11 + point);
            }
            if (enableAmmo)
            {
                gun.ammo -= rand12;
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
                gun.reloadTime -= (float)(rand15);
            }
        }

        private void setRandom()
        {
            // Gets rounds that player has won to edit some random stats
            int point = GameModeManager.CurrentHandler.GetTeamScore(player.teamID).points;

            // Sets initial random numbers
            rand1 = random.Next(mSpeed); //movement speed
            rand2 = random.NextDouble(); //gravity
            rand3 = random.Next(grav); //gravity
            rand4 = random.Next(jump); //jump
            rand5 = random.Next(health * (point + numCards)); //health
            rand6 = random.Next(sDamageOver); //seconds to take damage over
            rand7 = random.NextDouble(); //attack speed
            rand8 = random.Next(bounce); //bounces
            //rand9 = random.NextDouble(); //spread
            //rand10 = random.NextDouble(); //even spread
            rand11 = random.Next(numProj); //number of projectiles
            rand12 = random.Next(ammo + (point + numCards)); //amount of ammo
            rand13 = random.Next(damage); //damage
            rand13_1 = random.NextDouble();
            rand14 = random.Next(knockback); //knockback
            rand15 = random.NextDouble(); //reload time
            rand16 = random.Next(reloadTime); //reload time
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
