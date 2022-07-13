using ClassesManagerReborn;
using System.Collections;
using SeniorProject;

namespace SeniorProject.Cards.GuardianClass
{
    class GuardianClass : ClassHandler
    {
        private bool debug_l = false;

        // Class Name
        public static string name = "Guardian";

        public override IEnumerator Init()
        {
            // Debugging
            if (debug_l || SeniorProject.debug_g || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log("Regestering: " + name);
            }

            // Registering class
            while (!(Guardian.Card && ExtraRegen.Card && GuardiansGuardian.Card && DoubleHealth.Card && GuardiansRebirth.Card && DamageOver.Card)) yield return null;
            ClassesRegistry.Register(Guardian.Card, CardType.Entry); //Entry class card (Max: 1)
            ClassesRegistry.Register(ExtraRegen.Card, CardType.Card, Guardian.Card, 5); //ExtraRegen Card (Max: 5)
            ClassesRegistry.Register(GuardiansGuardian.Card, CardType.Card, Guardian.Card); //GuardiansGuardian Card (Max: None)
            ClassesRegistry.Register(DoubleHealth.Card, CardType.Card, Guardian.Card); //DoubleHealth Card (Max: None)
            ClassesRegistry.Register(GuardiansRebirth.Card, CardType.Card, Guardian.Card, 3); //GuardiansRebirth Card (Max: 3)
            ClassesRegistry.Register(DamageOver.Card, CardType.Card, Guardian.Card); //DamageOver Card (Max: None)
        }
    }
}
