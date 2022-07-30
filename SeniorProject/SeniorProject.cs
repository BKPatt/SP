using UnboundLib.Cards;
using BepInEx;
using HarmonyLib;
using ModdingUtils.GameModes;
using SeniorProject.Cards.PinballClass;
using SeniorProject.Cards.GuardianClass;
using SeniorProject.Cards.LotteryClass;
using SeniorProject.Cards.KingClass;
using SeniorProject.Cards.OneShotWonderClass;
using SeniorProject.Cards.WrathClass;

namespace SeniorProject
{
    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.classes.manager.reborn")]
    //[BepInDependency("root.classes.manager.reborn", BepInDependency.DependencyFlags.HardDependency)]
    
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]
    
    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    public class SeniorProject : BaseUnityPlugin
    {
        private const string ModId = "com.bkpatt.rounds.SeniorProject";
        private const string ModName = "SeniorProject";
        public const string Version = "1.0.2"; // What version are we on (major.minor.patch)?
        public const string ModInitials = "SP";

        public static SeniorProject instance { get; private set; }

        public static bool debug_g = false; // Debug cards
        public static bool debug_a = false; // Debug all
        public static bool debug_am = false; // Debug Mono


        void Awake()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }
        void Start()
        {
            instance = this;

            gameObject.AddComponent<InterfaceGameModeHooksManager>();

            //Pinball Class
            CustomCard.BuildCard<Pinball>((card) => Pinball.Card = card);
            CustomCard.BuildCard<ExtraPinball>((card) => ExtraPinball.Card = card);
            CustomCard.BuildCard<BunchoBounces>((card) => BunchoBounces.Card = card);
            CustomCard.BuildCard<PinballSpeed>((card) => PinballSpeed.Card = card);
            CustomCard.BuildCard<HighScore>((card) => HighScore.Card = card);

            //Guardian Class
            CustomCard.BuildCard<Guardian>((card) => Guardian.Card = card);
            CustomCard.BuildCard<DamageOver>((card) => DamageOver.Card = card);
            CustomCard.BuildCard<DoubleHealth>((card) => DoubleHealth.Card = card);
            CustomCard.BuildCard<ExtraRegen>((card) => ExtraRegen.Card = card);
            CustomCard.BuildCard<GuardiansGuardian>((card) => GuardiansGuardian.Card = card);
            CustomCard.BuildCard<GuardiansRebirth>((card) => GuardiansRebirth.Card = card);

            //Lottery Class
            CustomCard.BuildCard<Lottery>((card) => Lottery.Card = card);
            CustomCard.BuildCard<BetterOdds>((card) => BetterOdds.Card = card);
            CustomCard.BuildCard<CashStrapped>((card) => CashStrapped.Card = card);
            CustomCard.BuildCard<Chaos>((card) => Chaos.Card = card);
            CustomCard.BuildCard<Fireworks>((card) => Fireworks.Card = card);
            CustomCard.BuildCard<LittleChaos>((card) => LittleChaos.Card = card);
            CustomCard.BuildCard<LittleTime>((card) => LittleTime.Card = card);
            CustomCard.BuildCard<MoreChaos>((card) => MoreChaos.Card = card);
            CustomCard.BuildCard<MoreTime>((card) => MoreTime.Card = card);
            CustomCard.BuildCard<Pyrotechnics>((card) => Pyrotechnics.Card = card);

            //King Class
            CustomCard.BuildCard<King>((card) => King.Card = card);
            CustomCard.BuildCard<LilGunRandom>((card) => LilGunRandom.Card = card);
            CustomCard.BuildCard<LilMoreGunRandom>((card) => LilMoreGunRandom.Card = card);
            CustomCard.BuildCard<MoreRandom>((card) => MoreRandom.Card = card);
            CustomCard.BuildCard<PlayerRandom>((card) => PlayerRandom.Card = card);
            CustomCard.BuildCard<AmmoLuck>((card) => AmmoLuck.Card = card);
            CustomCard.BuildCard<HealthLuck>((card) => HealthLuck.Card = card);
            CustomCard.BuildCard<MoreGunStats>((card) => MoreGunStats.Card = card);
            CustomCard.BuildCard<MoreMovement>((card) => MoreMovement.Card = card);

            //OneShotWonder Class
            CustomCard.BuildCard<OneShotWonder>((card) => OneShotWonder.Card = card);
            CustomCard.BuildCard<IgnoreWalls>((card) => IgnoreWalls.Card = card);
            CustomCard.BuildCard<MoreAmmo>((card) => MoreAmmo.Card = card);
            CustomCard.BuildCard<MoreBulSpeed>((card) => MoreBulSpeed.Card = card);
            CustomCard.BuildCard<MoreDamage>((card) => MoreDamage.Card = card);
            CustomCard.BuildCard<Scope>((card) => Scope.Card = card);
            CustomCard.BuildCard<UnblockableBullet>((card) => UnblockableBullet.Card = card);

            //Wrath Class
            CustomCard.BuildCard<Wrath>((card) => Wrath.Card = card);
            CustomCard.BuildCard<FasterBlock>((card) => FasterBlock.Card = card);
            CustomCard.BuildCard<MoreBlocks>((card) => MoreBlocks.Card = card);
            CustomCard.BuildCard<BlockAction>((card) => BlockAction.Card = card);
            CustomCard.BuildCard<ShockAndAwe>((card) => ShockAndAwe.Card = card);
            CustomCard.BuildCard<BetterChance>((card) => BetterChance.Card = card);
            CustomCard.BuildCard<AddHealth>((card) => AddHealth.Card = card);
            CustomCard.BuildCard<AddLS>((card) => AddLS.Card = card);
        }
    }
}