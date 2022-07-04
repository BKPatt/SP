using ClassesManagerReborn;
using System.Collections;

namespace SeniorProject.Cards.PinballClass
{
    class PinballClass : ClassHandler
    {
        public static string name = "Pinball";

        public override IEnumerator Init()
        {
            // Debugging
            //UnityEngine.Debug.Log("Regestering: " + name);

            // Register class
            while (!(Pinball.Card && ExtraPinball.Card && BunchoBounces.Card && PinballSpeed.Card && HighScore.Card)) yield return null;
            ClassesRegistry.Register(Pinball.Card, CardType.Entry);
            ClassesRegistry.Register(ExtraPinball.Card, CardType.Card, Pinball.Card, 2);
            ClassesRegistry.Register(BunchoBounces.Card, CardType.Card, Pinball.Card, 4);
            ClassesRegistry.Register(PinballSpeed.Card, CardType.Card, Pinball.Card, 3);
            ClassesRegistry.Register(HighScore.Card, CardType.Card, Pinball.Card, 2);
        }
    }
}
