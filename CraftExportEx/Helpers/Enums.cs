namespace CraftExport.Helpers
{
    /// <summary>
    /// Enumeration of all current CraftAction Ids.
    /// </summary>
    public enum CraftActionId
    {
        none = 0,
        // Carpenter Crafting Actions
        crp_BasicSynthesis = 100001,
        crp_BasicTouch = 100002,
        crp_MastersMend = 100003,
        crp_SteadyHand = 244,
        crp_InnerQuiet = 252,
        crp_Observe = 100010,
        crp_Rumination = 276,
        crp_StandardTouch = 100004,
        crp_GreatStrides = 260,
        crp_MastersMendII = 100005,
        crp_StandardSynthesis = 100007,
        crp_BrandOfWind = 100006,
        crp_AdvancedTouch = 100008,
        crp_ByregotsBlessing = 100009,
        crp_CollectableSynthesis = 4560,
        crp_ByregotsBrow = 100120,
        crp_PreciseTouch = 100128,
        crp_NameofWind = 4568,

        // Blacksmith Crafting Actions
        bsm_BasicSynthesis = 100015,
        bsm_BasicTouch = 100016,
        bsm_MastersMend = 100017,
        bsm_SteadyHand = 245,
        bsm_InnerQuiet = 253,
        bsm_Observe = 100023,
        bsm_Ingenuity = 277,
        bsm_StandardTouch = 100018,
        bsm_GreatStrides = 261,
        bsm_MastersMendII = 100019,
        bsm_StandardSynthesis = 100021,
        bsm_BrandOfFire = 100020,
        bsm_AdvancedTouch = 100022,
        bsm_IngenuityII = 283,
        bsm_CollectableSynthesis = 4561,
        bsm_ByregotsBrow = 100121,
        bsm_PreciseTouch = 100129,
        bsm_NameofFire = 4569,

        // Armorer Crafting Actions
        arm_BasicSynthesis = 100030,
        arm_BasicTouch = 100031,
        arm_MastersMend = 100032,
        arm_SteadyHand = 246,
        arm_InnerQuiet = 254,
        arm_Observe = 100040,
        arm_RapidSynthesis = 100033,
        arm_StandardTouch = 100034,
        arm_GreatStrides = 262,
        arm_MastersMendII = 100035,
        arm_StandardSynthesis = 100037,
        arm_BrandOfIce = 100036,
        arm_AdvancedTouch = 100038,
        arm_PieceByPiece = 100039,
        arm_CollectableSynthesis = 4562,
        arm_ByregotsBrow = 100122,
        arm_PreciseTouch = 100130,
        arm_NameofIce = 4570,

        // Goldsmith Crafting Actions
        gsm_BasicSynthesis = 100075,
        gsm_BasicTouch = 100076,
        gsm_MastersMend = 100077,
        gsm_SteadyHand = 247,
        gsm_InnerQuiet = 255,
        gsm_Observe = 100082,
        gsm_Manipulation = 278,
        gsm_StandardTouch = 100078,
        gsm_GreatStrides = 263,
        gsm_MastersMendII = 100079,
        gsm_StandardSynthesis = 100080,
        gsm_FlawlessSynthesis = 100083,
        gsm_AdvancedTouch = 100081,
        gsm_Innovation = 284,
        gsm_CollectableSynthesis = 4563,
        gsm_ByregotsBrow = 100123,
        gsm_PreciseTouch = 100131,
        gsm_MakersMark = 100178,

        // Leatherworker Crafting Actions
        ltw_BasicSynthesis = 100045,
        ltw_BasicTouch = 100046,
        ltw_MastersMend = 100047,
        ltw_SteadyHand = 249,
        ltw_InnerQuiet = 257,
        ltw_Observe = 100053,
        ltw_WasteNot = 279,
        ltw_StandardTouch = 100048,
        ltw_GreatStrides = 265,
        ltw_MastersMendII = 100049,
        ltw_StandardSynthesis = 100051,
        ltw_BrandOfEarth = 100050,
        ltw_AdvancedTouch = 100052,
        ltw_WasteNotII = 285,
        ltw_CollectableSynthesis = 4564,
        ltw_ByregotsBrow = 100124,
        ltw_PreciseTouch = 100132,
        ltw_NameofEarth = 4571,

        // Weaver Crafting Actions
        wvr_BasicSynthesis = 100060,
        wvr_BasicTouch = 100061,
        wvr_MastersMend = 100062,
        wvr_SteadyHand = 248,
        wvr_InnerQuiet = 256,
        wvr_Observe = 100070,
        wvr_CarefulSynthesis = 100063,
        wvr_StandardTouch = 100064,
        wvr_GreatStrides = 264,
        wvr_MastersMendII = 100065,
        wvr_StandardSynthesis = 100067,
        wvr_BrandOfLightning = 100066,
        wvr_AdvancedTouch = 100068,
        wvr_CarefulSynthesisII = 100069,
        wvr_CollectableSynthesis = 4565,
        wvr_ByregotsBrow = 100125,
        wvr_PreciseTouch = 100133,
        wvr_NameofLightning = 4572,

        // Alchemist Crafting Actions
        alc_BasicSynthesis = 100090,
        alc_BasicTouch = 100091,
        alc_MastersMend = 100092,
        alc_SteadyHand = 250,
        alc_InnerQuiet = 258,
        alc_Observe = 100099,
        alc_TricksOfTheTrade = 100098,
        alc_StandardTouch = 100093,
        alc_GreatStrides = 266,
        alc_MastersMendII = 100094,
        alc_StandardSynthesis = 100096,
        alc_BrandOfWater = 100095,
        alc_AdvancedTouch = 100097,
        alc_ComfortZone = 286,
        alc_CollectableSynthesis = 4566,
        alc_ByregotsBrow = 100126,
        alc_PreciseTouch = 100134,
        alc_NameofWater = 4573,

        // Culinarian Crafting Actions
        cul_BasicSynthesis = 100105,
        cul_BasicTouch = 100106,
        cul_MastersMend = 100107,
        cul_SteadyHand = 251,
        cul_InnerQuiet = 259,
        cul_Observe = 100113,
        cul_HastyTouch = 100108,
        cul_StandardTouch = 100109,
        cul_GreatStrides = 267,
        cul_MastersMendII = 100110,
        cul_StandardSynthesis = 100111,
        cul_SteadyHandII = 281,
        cul_AdvancedTouch = 100112,
        cul_Reclaim = 287,
        cul_CollectableSynthesis = 4567,
        cul_ByregotsBrow = 100127,
        cul_PreciseTouch = 100135,
        cul_MuscleMemory = 100136,
    }

    /// <summary>
    /// Enumeration for usage with Material Radiobutton.
    /// </summary>
    public enum MaterialUsage
    {
        nqOnly = 0,
        nqFirst = 1,
        hqFirst = 2
    }

    /// <summary>
    /// Enumeration for usage with Food Quality selection.
    /// </summary>
    public enum FoodQuality
    {
        nqFood = 0,
        hqFood = 1
    }
}
