using UnityEngine;
using UnboundLib.GameModes;
using System.Collections;

namespace SeniorProject.MonoBehaviours
{
    class PinballPointAndCard : MonoBehaviour
    {
        private Player player;
        private Gun gun;
        private GunAmmo gunAmmo;

        private int point;

        public int numCards = 0;
        public int bounce = 5;
        public int max_ammo = 1;
        public float minReload = 5;
        public float proj_speed = 0.5f;

        public void Awake()
        {
            player = this.gameObject.GetComponentInParent<Player>();
            gun = this.player.GetComponent<Holding>().holdable.GetComponent<Gun>();
            gunAmmo = GetComponent<Holding>().holdable.GetComponentInChildren<GunAmmo>();

            GameModeManager.AddHook(GameModeHooks.HookPickEnd, OnPickEnd);
        }

        public void Start()
        {
            point = GameModeManager.CurrentHandler.GetTeamScore(player.teamID).rounds;

            setStats();
        }

        public IEnumerator OnPickEnd(IGameModeHandler gm)
        {
            point = GameModeManager.CurrentHandler.GetTeamScore(player.teamID).rounds;

            setStats();

            gun.reflects += ((point + (numCards * 2)));

            yield break;
        }

        private void setStats()
        {
            gun.gravity = 0;
            gun.reflects = bounce;
            gunAmmo.maxAmmo = max_ammo;
            gun.numberOfProjectiles = 1;
            gun.projectileSize = 5;
            gun.projectileSpeed = proj_speed;
            gun.projectileColor = Color.white;

            if (gunAmmo.reloadTime < minReload)
            {
                gunAmmo.reloadTime = minReload;
            }
        }
    }
}
