using ClassesManagerReborn;
using System.Collections;

namespace SeniorProject.Cards.WrathClass
{
    class WrathClass : ClassHandler
    {
        public static string name = "Wrath";

        public override IEnumerator Init()
        {
            // Debugging
            //UnityEngine.Debug.Log("Regestering: " + name);

            // Register class
            while (!(Wrath.Card && FasterBlock.Card && MoreBlocks.Card && BlockAction.Card && ShockAndAwe.Card && BetterChance.Card && AddHealth.Card && AddLS.Card)) yield return null;
            ClassesRegistry.Register(Wrath.Card, CardType.Entry);
            ClassesRegistry.Register(FasterBlock.Card, CardType.Card, Wrath.Card);
            ClassesRegistry.Register(MoreBlocks.Card, CardType.Card, Wrath.Card);
            ClassesRegistry.Register(BlockAction.Card, CardType.Gate, Wrath.Card);
            ClassesRegistry.Register(ShockAndAwe.Card, CardType.Gate, new CardInfo[] { BlockAction.Card });
            ClassesRegistry.Register(BetterChance.Card, CardType.Card, new CardInfo[] { ShockAndAwe.Card });
            ClassesRegistry.Register(AddHealth.Card, CardType.Card, new CardInfo[] { BlockAction.Card });
            ClassesRegistry.Register(AddLS.Card, CardType.Card, new CardInfo[] { BlockAction.Card });
        }
    }
}
