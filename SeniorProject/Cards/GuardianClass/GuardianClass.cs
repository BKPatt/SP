using ClassesManagerReborn;
using System.Collections;

namespace SeniorProject.Cards.GuardianClass
{
    class GuardianClass : ClassHandler
    {
        public static string name = "Guardian";

        public override IEnumerator Init()
        {
            // Debugging
            //UnityEngine.Debug.Log("Regestering: " + name);

            // Registering class
            while (!(Guardian.Card && ExtraRegen.Card && GuardiansGuardian.Card && DoubleHealth.Card && GuardiansRebirth.Card && DamageOver.Card)) yield return null;
            ClassesRegistry.Register(Guardian.Card, CardType.Entry);
            ClassesRegistry.Register(ExtraRegen.Card, CardType.Card, Guardian.Card, 5);
            ClassesRegistry.Register(GuardiansGuardian.Card, CardType.Card, Guardian.Card);
            ClassesRegistry.Register(DoubleHealth.Card, CardType.Card, Guardian.Card);
            ClassesRegistry.Register(GuardiansRebirth.Card, CardType.Card, Guardian.Card, 3);
            ClassesRegistry.Register(DamageOver.Card, CardType.Card, Guardian.Card);
        }
    }
}
