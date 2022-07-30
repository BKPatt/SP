using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.Linq;
using UnboundLib;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Networking;
using System.Reflection;
using ModdingUtils.Utils;

// Code mostly from Pykess' "King Midas" Card (with his permission)
// All credit for this code goes to Pykess
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

                List<Player> allOtherPlayers = PlayerManager.instance.players.Where(player => PlayerStatus.PlayerAliveAndSimulated(player) && (player.playerID != this.player.playerID)).ToList();

                Vector2 displace;

                foreach (Player otherPlayers in allOtherPlayers)
                {
                    displace = otherPlayers.transform.position - this.player.transform.position;
                    if (displace.magnitude <= this.range)
                    {
                        if (PhotonNetwork.OfflineMode)
                        {
                            otherPlayers.gameObject.GetOrAddComponent<GreenEffect>();
                        }
                        else if (this.player.GetComponent<PhotonView>().IsMine)
                        {
                            NetworkingManager.RPC(typeof(GreenPlayer), "RPCA_TurnGreen", new object[] { otherPlayers.data.view.ControllerActorNr });
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

    

    public class GreenEffect : ReversibleEffect
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