using ClassesManagerReborn;
using System.Collections;
using SeniorProject;

namespace SeniorProject.Cards.PinballClass
{
    class PinballClass : ClassHandler
    {
        private bool debug_l = false;

        //Class Name
        public static string name = "Pinball";

        public override IEnumerator Init()
        {
            // Debugging
            if (debug_l || SeniorProject.debug_g || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log("Regestering: " + name);
            }

            // Register class
            while (!(Pinball.Card && ExtraPinball.Card && BunchoBounces.Card && PinballSpeed.Card && HighScore.Card)) yield return null;
            ClassesRegistry.Register(Pinball.Card, CardType.Entry); //Pinball entry Card
            ClassesRegistry.Register(ExtraPinball.Card, CardType.Card, Pinball.Card, 2); //ExtraPinball Card (Max: 2)
            ClassesRegistry.Register(BunchoBounces.Card, CardType.Card, Pinball.Card, 4); //BunchoBounces Card (Max: 4)
            ClassesRegistry.Register(PinballSpeed.Card, CardType.Card, Pinball.Card, 3); //PinballSpeed Card (Max: 3)
            ClassesRegistry.Register(HighScore.Card, CardType.Card, Pinball.Card, 2); //HighScore Card (Max: 2)
        }
    }
}
