using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using SeniorProject.Extensions;
using System.Linq;
using UnboundLib;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Networking;
using System.Reflection;

namespace SeniorProject.MonoBehaviours
{
    class GreenPlayer : MonoBehaviour
    {
        private Player player;

        private readonly float range = 1.75f;

        private void Start()
        {
            player = this.gameObject.GetComponentInParent<Player>();
        }


        private void Update()
        {            
            if (PlayerStatus.PlayerAliveAndSimulated(this.player))
            {
                player.gameObject.GetOrAddComponent<GreenEffect>();
                // get all alive players that are not this player
                List<Player> otherPlayers = PlayerManager.instance.players.Where(player => PlayerStatus.PlayerAliveAndSimulated(player) && (player.playerID != this.player.playerID)).ToList();

                Vector2 displacement;

                foreach (Player otherPlayer in otherPlayers)
                {
                    displacement = otherPlayer.transform.position - this.player.transform.position;
                    if (displacement.magnitude <= this.range)
                    {
                        // if the other player is within range, then add the Green effect to them

                        // locally
                        if (PhotonNetwork.OfflineMode)
                        {
                            otherPlayer.gameObject.GetOrAddComponent<GreenEffect>();
                        }
                        // via network
                        else if (this.player.GetComponent<PhotonView>().IsMine)
                        {
                            NetworkingManager.RPC(typeof(GreenPlayer), "RPCA_TurnGreen", new object[] { otherPlayer.data.view.ControllerActorNr });
                        }
                    }

                }
            }
        }

        [UnboundRPC]
        private static void RPCA_TurnGreen(int actorID)
        {
            Player playerToEffect = (Player)typeof(PlayerManager).InvokeMember("GetPlayerWithActorID",
                    BindingFlags.Instance | BindingFlags.InvokeMethod |
                    BindingFlags.NonPublic, null, PlayerManager.instance, new object[] { actorID });

            playerToEffect.gameObject.GetOrAddComponent<GreenEffect>();
        }
    }

    

    // Separate class for bookkeeping
    public class GreenEffect : ReversibleEffect //this is a separate effect just for bookkeeping which players are Green
    {
        private readonly float movementSpeedReduction = 0.5f;
        private readonly float jumpReduction = 0.25f;
        private readonly Color color = Color.green;
        private ReversibleColorEffect colorEffect = null;

        public override void OnOnEnable()
        {
            if (this.colorEffect != null) { this.colorEffect.Destroy(); }
        }
        public override void OnStart()
        {
            base.characterStatModifiersModifier.movementSpeed_mult = (1f - this.movementSpeedReduction);
            base.characterStatModifiersModifier.jump_mult = (1f - this.jumpReduction);

            this.colorEffect = base.player.gameObject.AddComponent<ReversibleColorEffect>();
            this.colorEffect.SetColor(this.color);
            this.colorEffect.SetLivesToEffect(1);
        }
        public override void OnOnDisable()
        {
            if (this.colorEffect != null) { this.colorEffect.Destroy(); }
        }
        public override void OnOnDestroy()
        {
            if (this.colorEffect != null) { this.colorEffect.Destroy(); }
        }
    }
}
