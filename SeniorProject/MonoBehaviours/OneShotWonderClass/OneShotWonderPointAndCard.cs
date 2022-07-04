using UnityEngine;
using UnboundLib.GameModes;
using System.Collections;

namespace SeniorProject.MonoBehaviours
{
    class OneShotWonderPointAndCard : MonoBehaviour
    {
        private Player player;
        private Gun gun;
        private GunAmmo gunAmmo;
        private CharacterStatModifiers statModifiers;
        private Block block;

        private int point;
        private int store_point = 0;
        private int store_numCards = 0;
        private int store_projSpeed = 0;
        private int store_damage = 0;
        private int store_ammo = 0;

        public bool unblock_bullet = false;
        public bool ignore_wall = false;
        public bool bullet_grav = false;

        public int numCards = 0;

        public int addDamage = 0;
        public int projSpeed = 0;
        public int ammo = 0;

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
        }

        public void Start()
        {
            // Get initial rounds a player has when class is chosen
            point = GameModeManager.CurrentHandler.GetTeamScore(player.teamID).rounds;

            // Set initial stats
            gunAmmo.maxAmmo = 1;
            setStats();
        }

        // Referenced whenever card pick ends
        public IEnumerator OnPickEnd(IGameModeHandler gm)
        {
            // Get current amount of rounds the player has won
            point = GameModeManager.CurrentHandler.GetTeamScore(player.teamID).rounds;

            // Set new stats
            setStats();

            // Sets health if the player has won a new round or another class card
            if (point > store_point || numCards > store_numCards)
            {
                gun.damage = (point + numCards);

                // Trys to ensure projectile size doesn't get insanely high
                if (gun.damage > 100)
                {
                    gun.projectileSize *= (float)((100)/(10 * (point + (numCards * 2))));
                }

                store_point = point + 1;
                store_numCards = numCards + 1;
            }

            yield break;
        }

        private void setStats()
        {
            // Modifiers
            gun.projectileColor = Color.red;

            if (unblock_bullet)
            {
                gun.unblockable = true;
            }
            if (ignore_wall)
            {
                gun.ignoreWalls = true;
            }

            if (addDamage > store_damage)
            {
                gun.damage += 1;
                gun.projectileSize *= 0.5f;

                store_damage = addDamage;
            }
            if (projSpeed > store_projSpeed)
            {
                gun.projectileSpeed *= 2;

                store_projSpeed = projSpeed;
            }
            if (ammo > store_ammo)
            {
                gunAmmo.maxAmmo++;

                store_ammo = ammo;
            }
            if (bullet_grav)
            {
                gun.gravity = 0;
            }

            // Makes sure reload time isn't explicitly set below 4s
            if (gun.reloadTime < 4)
            {
                gun.reloadTime = 4;
            }
        }
    }
}
