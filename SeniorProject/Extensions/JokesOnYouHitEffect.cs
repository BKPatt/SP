using ModdingUtils.RoundsEffects;
using UnityEngine;

namespace SeniorProject.Extensions
{
    public class JokesOnYouHitEffect : HitEffect
    {
        public override void DealtDamage(
            Vector2 damage,
            bool selfDamage,
            Player damagedPlayer = null
        )
        {
            if (!selfDamage || damagedPlayer == null || damagedPlayer != null && damagedPlayer.teamID != gameObject.GetComponent<Player>().teamID)
            {
                return;
            }

            damagedPlayer.data.healthHandler.Heal(damage.magnitude);
        }
    }
}