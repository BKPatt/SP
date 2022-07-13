using ClassesManagerReborn;
using System.Collections;
using SeniorProject;

namespace SeniorProject.Cards.WrathClass
{
    class WrathClass : ClassHandler
    {
        private bool debug_l = false;

        //Class Name
        public static string name = "Wrath";

        public override IEnumerator Init()
        {
            // Debugging
            if (debug_l || SeniorProject.debug_g || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log("Regestering: " + name);
            }

            // Register class
            while (!(Wrath.Card && FasterBlock.Card && MoreBlocks.Card && BlockAction.Card && ShockAndAwe.Card && BetterChance.Card && AddHealth.Card && AddLS.Card)) yield return null;
            ClassesRegistry.Register(Wrath.Card, CardType.Entry); //Wrath entry Card
            ClassesRegistry.Register(FasterBlock.Card, CardType.Card, Wrath.Card); //FasterBlock Card (Max: None)
            ClassesRegistry.Register(MoreBlocks.Card, CardType.Card, Wrath.Card); //MoreBlocks Card (Max: None)
            ClassesRegistry.Register(BlockAction.Card, CardType.Gate, Wrath.Card); //BlockAction Gate Card, Unlocks ShockAndAwe, AddHealth, and AddLs (Also BetterChance Indirectly) (Max: 1)
            ClassesRegistry.Register(ShockAndAwe.Card, CardType.Gate, new CardInfo[] { BlockAction.Card }); //ShockAndAwe Gate Card, Dependent on Block Action and Unlocks BetterChance (Max: 1)
            ClassesRegistry.Register(BetterChance.Card, CardType.Card, new CardInfo[] { ShockAndAwe.Card }, 5); //BetterChance Card, Dependent on ShockAndAwe (Max: 5)
            ClassesRegistry.Register(AddHealth.Card, CardType.Card, new CardInfo[] { BlockAction.Card }); //AddHealth Card, Dependent on BlockAction (Max: 1)
            ClassesRegistry.Register(AddLS.Card, CardType.Card, new CardInfo[] { BlockAction.Card }); //AddLS Card, Dependent on BlockAction (Max: 1)
        }
    }
}
