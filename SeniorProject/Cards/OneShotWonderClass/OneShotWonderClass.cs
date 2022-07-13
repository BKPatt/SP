using ClassesManagerReborn;
using System.Collections;
using SeniorProject;

namespace SeniorProject.Cards.OneShotWonderClass
{
    class OneShotWonderClass : ClassHandler
    {
        private bool debug_l = false;

        //Class Name
        public static string name = "OSW";

        public override IEnumerator Init()
        {
            // Debugging
            if (debug_l || SeniorProject.debug_g || SeniorProject.debug_a)
            {
                UnityEngine.Debug.Log("Regestering: " + name);
            }

            // Register class
            while (!(OneShotWonder.Card && IgnoreWalls.Card && MoreAmmo.Card && MoreBulSpeed.Card && MoreDamage.Card && Scope.Card && UnblockableBullet.Card)) yield return null;
            ClassesRegistry.Register(OneShotWonder.Card, CardType.Entry); //OneShotWonder entry Card
            ClassesRegistry.Register(IgnoreWalls.Card, CardType.Card, OneShotWonder.Card); //IgnoreWalls Card (Max: 1)
            ClassesRegistry.Register(MoreAmmo.Card, CardType.Card, OneShotWonder.Card, 3); //MoreAmmo Card (Max: 3)
            ClassesRegistry.Register(MoreBulSpeed.Card, CardType.Card, OneShotWonder.Card, 10); //MoreBulSpeed Card (Max: 10)
            ClassesRegistry.Register(MoreDamage.Card, CardType.Card, OneShotWonder.Card); //MoreDamage Card (Max: None)
            ClassesRegistry.Register(Scope.Card, CardType.Card, OneShotWonder.Card); //Scope Card (Max: 1)
            ClassesRegistry.Register(UnblockableBullet.Card, CardType.Card, OneShotWonder.Card); //UnblockableBullet Card (Max: 1)
        }
    }
}
