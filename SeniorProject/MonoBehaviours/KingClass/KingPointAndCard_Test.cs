using System;
using ModdingUtils.MonoBehaviours;
using UnboundLib.GameModes;
using System.Collections;

namespace SeniorProject.MonoBehaviours
{
    class KingPointAndCard_Test : ReversibleEffect
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
                characterStatModifiersModifier.movementSpeed_add = (float)(rand1);
                //UnityEngine.Debug.Log($"{ statModifiers.movementSpeed }");
                //UnityEngine.Debug.Log($"After MS { statModifiers.movementSpeed }");
            }
            if (enableHealth)
            {
                //UnityEngine.Debug.Log($"Before Health");
                //UnityEngine.Debug.Log($"{ player.data.maxHealth }");
                characterStatModifiersModifier.health_add = rand5;
                //UnityEngine.Debug.Log($"{ player.data.maxHealth }");
                //UnityEngine.Debug.Log($"After Health");
            }
            if (enableJump)
            {
                //UnityEngine.Debug.Log($"Before Jump");
                //UnityEngine.Debug.Log($"{ statModifiers.numberOfJumps }");
                characterStatModifiersModifier.numberOfJumps_add = rand4;
                //UnityEngine.Debug.Log($"{ statModifiers.numberOfJumps }");
                //UnityEngine.Debug.Log($"After Jump");
            }
            if (enableGrav)
            {
                //UnityEngine.Debug.Log($"Before Grav");
                //UnityEngine.Debug.Log($"{ statModifiers.gravity }");
                characterStatModifiersModifier.gravity_add = (float)(rand2 + rand3);
                //UnityEngine.Debug.Log($"{ statModifiers.gravity }");
                //UnityEngine.Debug.Log($"After Grav");
            }
            if (enableDamageOver)
            {
                //UnityEngine.Debug.Log($"Before STDO");
                //UnityEngine.Debug.Log($"{ statModifiers.secondsToTakeDamageOver }");
                characterStatModifiersModifier.secondsToTakeDamageOver_add = rand6;
                //UnityEngine.Debug.Log($"{ statModifiers.secondsToTakeDamageOver }");
                //UnityEngine.Debug.Log($"After STDO");
            }
            if (enableAtSpeed)
            {
                //UnityEngine.Debug.Log($"Before AS");
                //UnityEngine.Debug.Log($"{ player.data.stats.attackSpeedMultiplier }");
                gunStatModifier.attackSpeedMultiplier_add = (float)rand7;
                //UnityEngine.Debug.Log($"{ player.data.stats.attackSpeedMultiplier }");
                //UnityEngine.Debug.Log($"After AS");
                //gun.attackSpeed += (float)rand7;
            }
            if (enableBounce)
            {
                UnityEngine.Debug.Log($"Before Bounce");
                UnityEngine.Debug.Log($"{ gun.reflects }");
                gunStatModifier.reflects_add = rand8;
                UnityEngine.Debug.Log($"{ gun.reflects }");
                UnityEngine.Debug.Log($"After Bounce");
            }
            if (enableProj)
            {
                UnityEngine.Debug.Log($"Before #Proj");
                UnityEngine.Debug.Log($"{ gun.numberOfProjectiles }");
                gunStatModifier.numberOfProjectiles_add = rand11;
                UnityEngine.Debug.Log($"{ gun.numberOfProjectiles }");
                UnityEngine.Debug.Log($"After #Proj");
                UnityEngine.Debug.Log($"Proj Speed: { gun.projectileSpeed }");
            }
            if (enableAmmo)
            {
                //UnityEngine.Debug.Log($"Before Ammo");
                //UnityEngine.Debug.Log($"{ gunAmmo.maxAmmo }");
                gunAmmoStatModifier.maxAmmo_add = rand12;
                //UnityEngine.Debug.Log($"{ gunAmmo.maxAmmo }");
                //UnityEngine.Debug.Log($"After Ammo");
            }
            if (enableDamage)
            {
                //UnityEngine.Debug.Log($"Before Damage");
                //UnityEngine.Debug.Log($"{ gun.damage }");
                //gun.damage = (rand13);
                gunStatModifier.damage_add = (float)(rand13_1 + rand13);
                gunStatModifier.projectileSize_mult = 0.8f;
                //UnityEngine.Debug.Log($"{ gun.damage }");
                //UnityEngine.Debug.Log($"After Damage");
            }
            if (enableKnockback)
            {
                //UnityEngine.Debug.Log($"Before Knockback");
                //UnityEngine.Debug.Log($"{ gun.knockback }");
                gunStatModifier.knockback_add = rand14;
                //UnityEngine.Debug.Log($"{ gun.knockback }");
                //UnityEngine.Debug.Log($"After Knockback");
            }
            if (enableRelTime)
            {
                //UnityEngine.Debug.Log($"Before RelTime");
                //UnityEngine.Debug.Log($"{ gunAmmo.reloadTime }");
                gunAmmoStatModifier.reloadTimeAdd_add = (float)(rand15 + rand16);
                //UnityEngine.Debug.Log($"{ gunAmmo.reloadTime }");
                //UnityEngine.Debug.Log($"After RelTime");
            }
        }

        // Sets stats back to original when battle ends
        private void resetStats()
        {
            // Resets stats for each enabled random stat
            ClearModifiers();
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
            rand5 = random.Next(health + (point + numCards)*4); //health
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
            ApplyModifiers();

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
