using ClassesManagerReborn;
using System.Collections;
using SeniorProject;

namespace SeniorProject.Cards.LotteryClass
{
    class LotteryClass : ClassHandler
    {
        private bool debug_l = false;

        //Class Name
        public static string name = "Lottery";

        public override IEnumerator Init()
        {
            // Debugging
            if (debug_l || SeniorProject.debug_g || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log("Regestering: " + name);
            }

            // Register class
            while (!(Lottery.Card && BetterOdds.Card && CashStrapped.Card && Chaos.Card && Fireworks.Card && LittleChaos.Card && LittleTime.Card && MoreChaos.Card && MoreTime.Card && Pyrotechnics.Card)) yield return null;
            ClassesRegistry.Register(Lottery.Card, CardType.Entry); //Lottery entry card
            ClassesRegistry.Register(BetterOdds.Card, CardType.Card, Lottery.Card, 4); //BetterOdds Card (Max: 4)
            ClassesRegistry.Register(CashStrapped.Card, CardType.Card, Lottery.Card); //CashStrapped Card (Max: 1)
            ClassesRegistry.Register(Chaos.Card, CardType.Gate, Lottery.Card); //Chaos Gate Card, Unlocks Fireworks, LittleChaos, MoreChaos, and Pyrotechnics (Max: 1)
            ClassesRegistry.Register(Fireworks.Card, CardType.Card, new CardInfo[] { Chaos.Card }, 5); //Fireworks Card, Dependent on Chaos (Max: 5)
            ClassesRegistry.Register(LittleChaos.Card, CardType.Card, new CardInfo[] { Chaos.Card }, 5); //LittleChaos Card, Dependent on Chaos (Max: 5)
            ClassesRegistry.Register(LittleTime.Card, CardType.Card, Lottery.Card); //LittleTime Card (Max: None)
            ClassesRegistry.Register(MoreChaos.Card, CardType.Card, new CardInfo[] { Chaos.Card }, 2); //MoreChaos Card, Dependent on Chaos (Max: 2)
            ClassesRegistry.Register(MoreTime.Card, CardType.Card, Lottery.Card, 2); //MoreTime Card (Max: 2)
            ClassesRegistry.Register(Pyrotechnics.Card, CardType.Card, new CardInfo[] { Chaos.Card }); //Pyrotechnics Card, Dependent on Chaos (Max: 1)
        }
    }
}
