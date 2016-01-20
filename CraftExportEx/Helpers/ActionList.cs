using CraftExportEx;

namespace CraftExport.Helpers
{
    /// <summary>
    /// Base lists which holds all the required data for each class that requires an action list.
    /// </summary>
    public class ActionList
    {
        /// <summary>
        /// ResourceLibrary which holds the images for the craft actions.
        /// </summary>
        public ResourceLibrary ResourceLibrary { get; set; }

        /// <summary>
        /// Generic CraftAction for Basic Synthesis.
        /// </summary>
        public CraftAction BasicSynthesis { get; set; }

        /// <summary>
        /// Generic CraftAction for Standard Synthesis.
        /// </summary>
        public CraftAction StandardSynthesis { get; set; }

        /// <summary>
        /// Generic CraftAction for Basic Touch.
        /// </summary>
        public CraftAction BasicTouch { get; set; }

        /// <summary>
        /// Generic CraftAction for Standard Touch.
        /// </summary>
        public CraftAction StandardTouch { get; set; }

        /// <summary>
        /// Generic CraftAction for Advanced Touch.
        /// </summary>
        public CraftAction AdvancedTouch { get; set; }

        /// <summary>
        /// Generic CraftAction for Master's Mend.
        /// </summary>
        public CraftAction MastersMend { get; set; }

        /// <summary>
        /// Generic CraftAction for Master's Mend II.
        /// </summary>
        public CraftAction MastersMendII { get; set; }

        /// <summary>
        /// Generic CraftAction for Steady Hand.
        /// </summary>
        public CraftAction SteadyHand { get; set; }

        /// <summary>
        /// Generic CraftAction for Inner Quiet.
        /// </summary>
        public CraftAction InnerQuiet { get; set; }

        /// <summary>
        /// Generic CraftAction for Great Strides.
        /// </summary>
        public CraftAction GreatStrides { get; set; }

        /// <summary>
        /// Generic CraftAction for Observe.
        /// </summary>
        public CraftAction Observe { get; set; }


        /// <summary>
        /// Generic CraftAction for Collectable Synthesis
        /// </summary>
        public CraftAction CollectableSynthesis { get; set; }

        /// <summary>
        /// Generic CraftAction for Byregot's Brow
        /// </summary>
        public CraftAction ByregotsBrow { get; set; }

        /// <summary>
        /// Generic CraftAction for Precise Touch
        /// </summary>
        public CraftAction PreciseTouch { get; set; }
    }

    /// <summary>
    /// Extends the Actionlist and adds Carpenter specific Craft Actions.
    /// </summary>
    public class CarpenterActions : ActionList
    {
        // Add Carpenter specific CraftActions to the list.
        public CraftAction Rumination = new Rumination();
        public CraftAction BrandOfWind = new BrandOfWind();
        public CraftAction ByregotsBlessing = new ByregotsBlessing();
        public CraftAction NameofWind = new NameofWind();
        // Construct the CarpenterActions list.
        public CarpenterActions()
        {
            ResourceLibrary = new ResourceLibrary(Settings.ResourceLocation, ClassJobType.Carpenter);
            BasicSynthesis = new BasicSynthesis(CraftActionId.crp_BasicSynthesis, ResourceLibrary.ClassSkillImages["crp_BasicSynthesis"]);
            StandardSynthesis = new StandardSynthesis(CraftActionId.crp_StandardSynthesis, ResourceLibrary.ClassSkillImages["crp_StandardSynthesis"]);
            BasicTouch = new BasicTouch(CraftActionId.crp_BasicTouch, ResourceLibrary.ClassSkillImages["crp_BasicTouch"]);
            StandardTouch = new StandardTouch(CraftActionId.crp_StandardTouch, ResourceLibrary.ClassSkillImages["crp_StandardTouch"]);
            AdvancedTouch = new AdvancedTouch(CraftActionId.crp_AdvancedTouch, ResourceLibrary.ClassSkillImages["crp_AdvancedTouch"]);
            PreciseTouch = new PreciseTouch(CraftActionId.crp_PreciseTouch, ResourceLibrary.ClassSkillImages["crp_PreciseTouch"]);
            MastersMend = new MastersMend(CraftActionId.crp_MastersMend);
            MastersMendII = new MastersMendII(CraftActionId.crp_MastersMendII);
            SteadyHand = new SteadyHand(CraftActionId.crp_SteadyHand);
            InnerQuiet = new InnerQuiet(CraftActionId.crp_InnerQuiet);
            GreatStrides = new GreatStrides(CraftActionId.crp_GreatStrides);
            Observe = new Observe(CraftActionId.crp_Observe);
            CollectableSynthesis = new CollectableSynthesis(CraftActionId.crp_CollectableSynthesis);
            ByregotsBrow = new ByregotsBrow(CraftActionId.crp_ByregotsBrow);
        }
    }
    /// <summary>
    /// Extends the Actionlist and adds Blacksmith specific Craft Actions.
    /// </summary>
    public class BlacksmithActions : ActionList
    {
        // Add Blacksmith specific CraftActions to the list.
        public CraftAction Ingenuity = new Ingenuity();
        public CraftAction BrandOfFire = new BrandOfFire();
        public CraftAction IngenuityII = new IngenuityII();
        public CraftAction NameofFire = new NameofFire();
        // Construct the BlacksmithActions list.
        public BlacksmithActions()
        {
            ResourceLibrary = new ResourceLibrary(Settings.ResourceLocation, ClassJobType.Blacksmith);
            BasicSynthesis = new BasicSynthesis(CraftActionId.bsm_BasicSynthesis, ResourceLibrary.ClassSkillImages["bsm_BasicSynthesis"]);
            StandardSynthesis = new StandardSynthesis(CraftActionId.bsm_StandardSynthesis, ResourceLibrary.ClassSkillImages["bsm_StandardSynthesis"]);
            BasicTouch = new BasicTouch(CraftActionId.bsm_BasicTouch, ResourceLibrary.ClassSkillImages["bsm_BasicTouch"]);
            StandardTouch = new StandardTouch(CraftActionId.bsm_StandardTouch, ResourceLibrary.ClassSkillImages["bsm_StandardTouch"]);
            AdvancedTouch = new AdvancedTouch(CraftActionId.bsm_AdvancedTouch, ResourceLibrary.ClassSkillImages["bsm_AdvancedTouch"]);
            PreciseTouch = new PreciseTouch(CraftActionId.bsm_PreciseTouch, ResourceLibrary.ClassSkillImages["bsm_PreciseTouch"]);
            MastersMend = new MastersMend(CraftActionId.bsm_MastersMend);
            MastersMendII = new MastersMendII(CraftActionId.bsm_MastersMendII);
            SteadyHand = new SteadyHand(CraftActionId.bsm_SteadyHand);
            InnerQuiet = new InnerQuiet(CraftActionId.bsm_InnerQuiet);
            GreatStrides = new GreatStrides(CraftActionId.bsm_GreatStrides);
            Observe = new Observe(CraftActionId.bsm_Observe);
            CollectableSynthesis = new CollectableSynthesis(CraftActionId.bsm_CollectableSynthesis);
            ByregotsBrow = new ByregotsBrow(CraftActionId.bsm_ByregotsBrow);
        }
    }
    /// <summary>
    /// Extends the Actionlist and adds Armorer specific Craft Actions.
    /// </summary>
    public class ArmorerActions : ActionList
    {
        // Add Armorer specific CraftActions to the list.
        public CraftAction RapidSynthesis = new RapidSynthesis();
        public CraftAction BrandOfIce = new BrandOfIce();
        public CraftAction PieceByPiece = new PieceByPiece();
        public CraftAction NameofIce = new NameofIce();
        // Construct the ArmorerActions list.
        public ArmorerActions()
        {
            ResourceLibrary = new ResourceLibrary(Settings.ResourceLocation, ClassJobType.Armorer);
            BasicSynthesis = new BasicSynthesis(CraftActionId.arm_BasicSynthesis, ResourceLibrary.ClassSkillImages["arm_BasicSynthesis"]);
            StandardSynthesis = new StandardSynthesis(CraftActionId.arm_StandardSynthesis, ResourceLibrary.ClassSkillImages["arm_StandardSynthesis"]);
            BasicTouch = new BasicTouch(CraftActionId.arm_BasicTouch, ResourceLibrary.ClassSkillImages["arm_BasicTouch"]);
            StandardTouch = new StandardTouch(CraftActionId.arm_StandardTouch, ResourceLibrary.ClassSkillImages["arm_StandardTouch"]);
            AdvancedTouch = new AdvancedTouch(CraftActionId.arm_AdvancedTouch, ResourceLibrary.ClassSkillImages["arm_AdvancedTouch"]);
            PreciseTouch = new PreciseTouch(CraftActionId.arm_PreciseTouch, ResourceLibrary.ClassSkillImages["arm_PreciseTouch"]);
            MastersMend = new MastersMend(CraftActionId.arm_MastersMend);
            MastersMendII = new MastersMendII(CraftActionId.arm_MastersMendII);
            SteadyHand = new SteadyHand(CraftActionId.arm_SteadyHand);
            InnerQuiet = new InnerQuiet(CraftActionId.arm_InnerQuiet);
            GreatStrides = new GreatStrides(CraftActionId.arm_GreatStrides);
            Observe = new Observe(CraftActionId.arm_Observe);
            CollectableSynthesis = new CollectableSynthesis(CraftActionId.arm_CollectableSynthesis);
            ByregotsBrow = new ByregotsBrow(CraftActionId.arm_ByregotsBrow);
        }
    }
    /// <summary>
    /// Extends the Actionlist and adds Goldsmith specific Craft Actions.
    /// </summary>
    public class GoldsmithActions : ActionList
    {
        // Add Goldsmith specific CraftActions to the list.
        public CraftAction Manipulation = new Manipulation();
        public CraftAction FlawlessSynthesis = new FlawlessSynthesis();
        public CraftAction Innovation = new Innovation();
        public CraftAction MakersMark = new MakersMark();
        // Construct the GoldsmithActions list.
        public GoldsmithActions()
        {
            ResourceLibrary = new ResourceLibrary(Settings.ResourceLocation, ClassJobType.Goldsmith);
            BasicSynthesis = new BasicSynthesis(CraftActionId.gsm_BasicSynthesis, ResourceLibrary.ClassSkillImages["gsm_BasicSynthesis"]);
            StandardSynthesis = new StandardSynthesis(CraftActionId.gsm_StandardSynthesis, ResourceLibrary.ClassSkillImages["gsm_StandardSynthesis"]);
            BasicTouch = new BasicTouch(CraftActionId.gsm_BasicTouch, ResourceLibrary.ClassSkillImages["gsm_BasicTouch"]);
            StandardTouch = new StandardTouch(CraftActionId.gsm_StandardTouch, ResourceLibrary.ClassSkillImages["gsm_StandardTouch"]);
            AdvancedTouch = new AdvancedTouch(CraftActionId.gsm_AdvancedTouch, ResourceLibrary.ClassSkillImages["gsm_AdvancedTouch"]);
            PreciseTouch = new PreciseTouch(CraftActionId.gsm_PreciseTouch, ResourceLibrary.ClassSkillImages["gsm_PreciseTouch"]);
            MastersMend = new MastersMend(CraftActionId.gsm_MastersMend);
            MastersMendII = new MastersMendII(CraftActionId.gsm_MastersMendII);
            SteadyHand = new SteadyHand(CraftActionId.gsm_SteadyHand);
            InnerQuiet = new InnerQuiet(CraftActionId.gsm_InnerQuiet);
            GreatStrides = new GreatStrides(CraftActionId.gsm_GreatStrides);
            Observe = new Observe(CraftActionId.gsm_Observe);
            CollectableSynthesis = new CollectableSynthesis(CraftActionId.gsm_CollectableSynthesis);
            ByregotsBrow = new ByregotsBrow(CraftActionId.gsm_ByregotsBrow);
        }
    }
    /// <summary>
    /// Extends the Actionlist and adds Leatherworker specific Craft Actions.
    /// </summary>
    public class LeatherworkerActions : ActionList
    {
        // Add Leatherworker specific CraftActions to the list.
        public CraftAction WasteNot = new WasteNot();
        public CraftAction BrandOfEarth = new BrandOfEarth();
        public CraftAction WasteNotII = new WasteNotII();
        public CraftAction NameofEarth = new NameofEarth();
        // Construct the LeatherworkerActions list.
        public LeatherworkerActions()
        {
            ResourceLibrary = new ResourceLibrary(Settings.ResourceLocation, ClassJobType.Leatherworker);
            BasicSynthesis = new BasicSynthesis(CraftActionId.ltw_BasicSynthesis, ResourceLibrary.ClassSkillImages["ltw_BasicSynthesis"]);
            StandardSynthesis = new StandardSynthesis(CraftActionId.ltw_StandardSynthesis, ResourceLibrary.ClassSkillImages["ltw_StandardSynthesis"]);
            BasicTouch = new BasicTouch(CraftActionId.ltw_BasicTouch, ResourceLibrary.ClassSkillImages["ltw_BasicTouch"]);
            StandardTouch = new StandardTouch(CraftActionId.ltw_StandardTouch, ResourceLibrary.ClassSkillImages["ltw_StandardTouch"]);
            AdvancedTouch = new AdvancedTouch(CraftActionId.ltw_AdvancedTouch, ResourceLibrary.ClassSkillImages["ltw_AdvancedTouch"]);
            PreciseTouch = new PreciseTouch(CraftActionId.ltw_PreciseTouch, ResourceLibrary.ClassSkillImages["ltw_PreciseTouch"]);
            MastersMend = new MastersMend(CraftActionId.ltw_MastersMend);
            MastersMendII = new MastersMendII(CraftActionId.ltw_MastersMendII);
            SteadyHand = new SteadyHand(CraftActionId.ltw_SteadyHand);
            InnerQuiet = new InnerQuiet(CraftActionId.ltw_InnerQuiet);
            GreatStrides = new GreatStrides(CraftActionId.ltw_GreatStrides);
            Observe = new Observe(CraftActionId.ltw_Observe);
            CollectableSynthesis = new CollectableSynthesis(CraftActionId.ltw_CollectableSynthesis);
            ByregotsBrow = new ByregotsBrow(CraftActionId.ltw_ByregotsBrow);
        }
    }
    /// <summary>
    /// Extends the Actionlist and adds Weaver specific Craft Actions.
    /// </summary>
    public class WeaverActions : ActionList
    {
        // Add Weaver specific CraftActions to the list.
        public CraftAction CarefulSynthesis = new CarefulSynthesis();
        public CraftAction BrandOfLightning = new BrandOfLightning();
        public CraftAction CarefulSynthesisII = new CarefulSynthesisII();
        public CraftAction NameofLightning = new NameofLightning();
        // Construct the WeaverActions list.
        public WeaverActions()
        {
            ResourceLibrary = new ResourceLibrary(Settings.ResourceLocation, ClassJobType.Weaver);
            BasicSynthesis = new BasicSynthesis(CraftActionId.wvr_BasicSynthesis, ResourceLibrary.ClassSkillImages["wvr_BasicSynthesis"]);
            StandardSynthesis = new StandardSynthesis(CraftActionId.wvr_StandardSynthesis, ResourceLibrary.ClassSkillImages["wvr_StandardSynthesis"]);
            BasicTouch = new BasicTouch(CraftActionId.wvr_BasicTouch, ResourceLibrary.ClassSkillImages["wvr_BasicTouch"]);
            StandardTouch = new StandardTouch(CraftActionId.wvr_StandardTouch, ResourceLibrary.ClassSkillImages["wvr_StandardTouch"]);
            AdvancedTouch = new AdvancedTouch(CraftActionId.wvr_AdvancedTouch, ResourceLibrary.ClassSkillImages["wvr_AdvancedTouch"]);
            PreciseTouch = new PreciseTouch(CraftActionId.wvr_PreciseTouch, ResourceLibrary.ClassSkillImages["wvr_PreciseTouch"]);
            MastersMend = new MastersMend(CraftActionId.wvr_MastersMend);
            MastersMendII = new MastersMendII(CraftActionId.wvr_MastersMendII);
            SteadyHand = new SteadyHand(CraftActionId.wvr_SteadyHand);
            InnerQuiet = new InnerQuiet(CraftActionId.wvr_InnerQuiet);
            GreatStrides = new GreatStrides(CraftActionId.wvr_GreatStrides);
            Observe = new Observe(CraftActionId.wvr_Observe);
            CollectableSynthesis = new CollectableSynthesis(CraftActionId.wvr_CollectableSynthesis);
            ByregotsBrow = new ByregotsBrow(CraftActionId.wvr_ByregotsBrow);
        }
    }
    /// <summary>
    /// Extends the Actionlist and adds Alchemist specific Craft Actions.
    /// </summary>
    public class AlchemistActions : ActionList
    {
        // Add Alchemist specific CraftActions to the list.
        public CraftAction TricksOfTheTrade = new TricksOfTheTrade();
        public CraftAction BrandOfWater = new BrandOfWater();
        public CraftAction ComfortZone = new ComfortZone();
        public CraftAction NameofWater = new NameofWater();
        // Construct the AlchemistActions list.
        public AlchemistActions()
        {
            ResourceLibrary = new ResourceLibrary(Settings.ResourceLocation, ClassJobType.Alchemist);
            BasicSynthesis = new BasicSynthesis(CraftActionId.alc_BasicSynthesis, ResourceLibrary.ClassSkillImages["alc_BasicSynthesis"]);
            StandardSynthesis = new StandardSynthesis(CraftActionId.alc_StandardSynthesis, ResourceLibrary.ClassSkillImages["alc_StandardSynthesis"]);
            BasicTouch = new BasicTouch(CraftActionId.alc_BasicTouch, ResourceLibrary.ClassSkillImages["alc_BasicTouch"]);
            StandardTouch = new StandardTouch(CraftActionId.alc_StandardTouch, ResourceLibrary.ClassSkillImages["alc_StandardTouch"]);
            AdvancedTouch = new AdvancedTouch(CraftActionId.alc_AdvancedTouch, ResourceLibrary.ClassSkillImages["alc_AdvancedTouch"]);
            PreciseTouch = new PreciseTouch(CraftActionId.alc_PreciseTouch, ResourceLibrary.ClassSkillImages["alc_PreciseTouch"]);
            MastersMend = new MastersMend(CraftActionId.alc_MastersMend);
            MastersMendII = new MastersMendII(CraftActionId.alc_MastersMendII);
            SteadyHand = new SteadyHand(CraftActionId.alc_SteadyHand);
            InnerQuiet = new InnerQuiet(CraftActionId.alc_InnerQuiet);
            GreatStrides = new GreatStrides(CraftActionId.alc_GreatStrides);
            Observe = new Observe(CraftActionId.alc_Observe);
            CollectableSynthesis = new CollectableSynthesis(CraftActionId.alc_CollectableSynthesis);
            ByregotsBrow = new ByregotsBrow(CraftActionId.alc_ByregotsBrow);
        }
    }
    /// <summary>
    /// Extends the Actionlist and adds Culinarian specific Craft Actions.
    /// </summary>
    public class CulinarianActions : ActionList
    {
        // Add Culinarian specific CraftActions to the list.
        public CraftAction HastyTouch = new HastyTouch();
        public CraftAction SteadyHandII = new SteadyHandII();
        public CraftAction Reclaim = new Reclaim();
        public CraftAction Musclememory = new MuscleMemory();
        // Construct the CulinarianActions list.
        public CulinarianActions()
        {
            ResourceLibrary = new ResourceLibrary(Settings.ResourceLocation, ClassJobType.Culinarian);
            BasicSynthesis = new BasicSynthesis(CraftActionId.cul_BasicSynthesis, ResourceLibrary.ClassSkillImages["cul_BasicSynthesis"]);
            StandardSynthesis = new StandardSynthesis(CraftActionId.cul_StandardSynthesis, ResourceLibrary.ClassSkillImages["cul_StandardSynthesis"]);
            BasicTouch = new BasicTouch(CraftActionId.cul_BasicTouch, ResourceLibrary.ClassSkillImages["cul_BasicTouch"]);
            StandardTouch = new StandardTouch(CraftActionId.cul_StandardTouch, ResourceLibrary.ClassSkillImages["cul_StandardTouch"]);
            AdvancedTouch = new AdvancedTouch(CraftActionId.cul_AdvancedTouch, ResourceLibrary.ClassSkillImages["cul_AdvancedTouch"]);
            PreciseTouch = new PreciseTouch(CraftActionId.cul_PreciseTouch, ResourceLibrary.ClassSkillImages["cul_PreciseTouch"]);
            MastersMend = new MastersMend(CraftActionId.cul_MastersMend);
            MastersMendII = new MastersMendII(CraftActionId.cul_MastersMendII);
            SteadyHand = new SteadyHand(CraftActionId.cul_SteadyHand);
            InnerQuiet = new InnerQuiet(CraftActionId.cul_InnerQuiet);
            GreatStrides = new GreatStrides(CraftActionId.cul_GreatStrides);
            Observe = new Observe(CraftActionId.cul_Observe);
            CollectableSynthesis = new CollectableSynthesis(CraftActionId.cul_CollectableSynthesis);
            ByregotsBrow = new ByregotsBrow(CraftActionId.cul_ByregotsBrow);
        }
    }

}
