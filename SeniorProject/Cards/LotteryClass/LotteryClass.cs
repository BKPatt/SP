using ClassesManagerReborn;
using System.Collections;

namespace SeniorProject.Cards.LotteryClass
{
    class LotteryClass : ClassHandler
    {
        public static string name = "Lottery";

        public override IEnumerator Init()
        {
            // Debugging
            //UnityEngine.Debug.Log("Regestering: " + name);

            // Register class
            while (!(Lottery.Card && BetterOdds.Card && CashStrapped.Card && Chaos.Card && Fireworks.Card && LittleChaos.Card && LittleTime.Card && MoreChaos.Card && MoreTime.Card && Pyrotechnics.Card)) yield return null;
            ClassesRegistry.Register(Lottery.Card, CardType.Entry);
            ClassesRegistry.Register(BetterOdds.Card, CardType.Card, Lottery.Card, 4);
            ClassesRegistry.Register(CashStrapped.Card, CardType.Card, Lottery.Card);
            ClassesRegistry.Register(Chaos.Card, CardType.Card, Lottery.Card);
            ClassesRegistry.Register(Fireworks.Card, CardType.Card, Lottery.Card, 5);
            ClassesRegistry.Register(LittleChaos.Card, CardType.Card, Lottery.Card, 5);
            ClassesRegistry.Register(LittleTime.Card, CardType.Card, Lottery.Card);
            ClassesRegistry.Register(MoreChaos.Card, CardType.Card, Lottery.Card, 2);
            ClassesRegistry.Register(MoreTime.Card, CardType.Card, Lottery.Card, 2);
            ClassesRegistry.Register(Pyrotechnics.Card, CardType.Card, Lottery.Card);
        }
    }
}
