using HarmonyLib;
using System;
using UnboundLib;
using SeniorProject.Extensions;


//*********************************************
// This code origininated in PFFC by Tess/Root
// https://github.com/Tess-y/FFC_Port
//*********************************************


namespace SeniorProject.MonoBehaviours
{
    [Serializable]
    [HarmonyPatch(typeof(ProjectileHit), "Hit")]
    class ProjectileHitPatchHit
    {
        // disable friendly fire or self damage and hit effects if that setting is enabled
        private static bool Prefix(ProjectileHit __instance, HitInfo hit, bool forceCall)
        {
            HealthHandler healthHandler = null;
            if (hit.transform)
            {
                healthHandler = hit.transform.GetComponent<HealthHandler>();
            }
            if (healthHandler)
            {
                Player hitPlayer = healthHandler.GetComponent<Player>();
                // if the hit player is not null
                if (hitPlayer != null && __instance.ownPlayer.teamID == hitPlayer.teamID && __instance.ownPlayer.data.stats.GetAdditionalData().JokesOnYou)
                {
                    __instance.GetComponent<ProjectileHit>().RemoveOwnPlayerFromPlayersHit();
                    __instance.GetComponent<ProjectileHit>().AddPlayerToHeld(healthHandler);
                    __instance.GetComponent<MoveTransform>().velocity *= -1f;
                    __instance.transform.position += __instance.GetComponent<MoveTransform>().velocity * TimeHandler.deltaTime;
                    __instance.GetComponent<RayCastTrail>().WasBlocked();
                    if (__instance.destroyOnBlock)
                    {
                        __instance.InvokeMethod("DestroyMe");
                    }
                    __instance.sinceReflect = 0f;
                    return false;
                }
            }
            return true;
        }
    }
}
