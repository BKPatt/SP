using ClassesManagerReborn;
using System.Collections;

namespace SeniorProject.Cards.OneShotWonderClass
{
    class OneShotWonderClass : ClassHandler
    {
        public static string name = "OSW";

        public override IEnumerator Init()
        {
            // Debugging
            //UnityEngine.Debug.Log("Regestering: " + name);

            // Register class
            while (!(OneShotWonder.Card && IgnoreWalls.Card && MoreAmmo.Card && MoreBulSpeed.Card && MoreDamage.Card && Scope.Card && UnblockableBullet.Card)) yield return null;
            ClassesRegistry.Register(OneShotWonder.Card, CardType.Entry);
            ClassesRegistry.Register(IgnoreWalls.Card, CardType.Card, OneShotWonder.Card);
            ClassesRegistry.Register(MoreAmmo.Card, CardType.Card, OneShotWonder.Card);
            ClassesRegistry.Register(MoreBulSpeed.Card, CardType.Card, OneShotWonder.Card);
            ClassesRegistry.Register(MoreDamage.Card, CardType.Card, OneShotWonder.Card);
            ClassesRegistry.Register(Scope.Card, CardType.Card, OneShotWonder.Card);
            ClassesRegistry.Register(UnblockableBullet.Card, CardType.Card, OneShotWonder.Card);
        }
    }
}
