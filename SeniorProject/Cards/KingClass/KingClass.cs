using ClassesManagerReborn;
using System.Collections;
using SeniorProject;

namespace SeniorProject.Cards.KingClass
{
    class KingClass : ClassHandler
    {
        private bool debug_l = false;

        // Class Name
        public static string name = "King";

        public override IEnumerator Init()
        {
            // Debugging
            if (debug_l || SeniorProject.debug_g || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log("Regestering: " + name);
            }

            // Register class
            while (!(King.Card && LilGunRandom.Card && LilMoreGunRandom.Card && MoreRandom.Card && PlayerRandom.Card && AmmoLuck.Card && HealthLuck.Card && MoreGunStats.Card && MoreMovement.Card)) yield return null;
            ClassesRegistry.Register(King.Card, CardType.Entry); //King, Entry Card (Max: 1)
            ClassesRegistry.Register(LilGunRandom.Card, CardType.Gate, King.Card); //LilGunRandom Gate Card: Unlocks AmmoLuck & MoreGunStats(With LilMoreGunRandom) (Max: 1)
            ClassesRegistry.Register(LilMoreGunRandom.Card, CardType.Gate, King.Card); //LilMoreGunRandom Gate Card: Unlocks MoreGunStats(With LilGunRandom)
            ClassesRegistry.Register(MoreRandom.Card, CardType.Gate, King.Card); //MoreRandom Gate Card: Unlocks HealthLuck (Max: 1)
            ClassesRegistry.Register(PlayerRandom.Card, CardType.Gate, King.Card); //PlayerRandom Gate Card: Unlocks MoreMovement (Max: 1)
            ClassesRegistry.Register(AmmoLuck.Card, CardType.Card, new CardInfo[] { LilGunRandom.Card }); //AmmoLuck Card, Dependent on LilGunRandom (Max: None)
            ClassesRegistry.Register(HealthLuck.Card, CardType.Card, new CardInfo[] { MoreRandom.Card }); //HealthLuck Card, Dependent on MoreRandom (Max: None)
            ClassesRegistry.Register(MoreGunStats.Card, CardType.Card, new CardInfo[] { LilGunRandom.Card, LilMoreGunRandom.Card }); //MoreGunStats Card, Dependent on LilGunRandom & LilMoreGunRandom (Max: None)
            ClassesRegistry.Register(MoreMovement.Card, CardType.Card, new CardInfo[] { PlayerRandom.Card }, 2); //MoreMovement Card, Dependent on PlayerRandom (Max: 2)
        }
    }
}
