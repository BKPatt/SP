using ClassesManagerReborn;
using System.Collections;

namespace SeniorProject.Cards.KingClass
{
    class KingClass : ClassHandler
    {
        public static string name = "King";

        public override IEnumerator Init()
        {
            // Debugging
            //UnityEngine.Debug.Log("Regestering: " + name);

            // Register class
            while (!(King.Card && LilGunRandom.Card && LilMoreGunRandom.Card && MoreRandom.Card && PlayerRandom.Card && AmmoLuck.Card && HealthLuck.Card && MoreGunStats.Card && MoreMovement.Card)) yield return null;
            ClassesRegistry.Register(King.Card, CardType.Entry);
            ClassesRegistry.Register(LilGunRandom.Card, CardType.Gate, King.Card);
            ClassesRegistry.Register(LilMoreGunRandom.Card, CardType.Card, new CardInfo[] { LilGunRandom.Card, MoreRandom.Card, PlayerRandom.Card});
            ClassesRegistry.Register(MoreRandom.Card, CardType.Gate, King.Card);
            ClassesRegistry.Register(PlayerRandom.Card, CardType.Gate, King.Card);
            ClassesRegistry.Register(AmmoLuck.Card, CardType.Card, new CardInfo[] { LilGunRandom.Card, MoreRandom.Card, PlayerRandom.Card });
            ClassesRegistry.Register(HealthLuck.Card, CardType.Card, new CardInfo[] { LilGunRandom.Card, MoreRandom.Card, PlayerRandom.Card });
            ClassesRegistry.Register(MoreGunStats.Card, CardType.Card, new CardInfo[] { LilGunRandom.Card, MoreRandom.Card, PlayerRandom.Card });
            ClassesRegistry.Register(MoreMovement.Card, CardType.Card, new CardInfo[] { LilGunRandom.Card, MoreRandom.Card, PlayerRandom.Card }, 2);
        }
    }
}
