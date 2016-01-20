using System.Diagnostics.Eventing.Reader;
using System.Drawing;

namespace CraftExport.Helpers
{
    public class CraftAction
    {
        /// <summary>
        /// The Id of the action.
        /// </summary>
        public CraftActionId Id { get; set; }

        /// <summary>
        /// The level the action is available.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The name of the action.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The CP cost of the action.
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// The description of the action.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The efficiency of the action (if applicable).
        /// </summary>
        public string Efficiency { get; set; }

        /// <summary>
        /// The success rate of the action (if applicable).
        /// </summary>
        public string SuccesRate { get; set; }

        /// <summary>
        /// Whether or not the action can be used across other classes.
        /// </summary>
        public bool Crossclass { get; set; }

        /// <summary>
        /// The local resource image of the action.
        /// </summary>
        public Bitmap Image { get; set; }

        /// <summary>
        /// The resource library to hold the images.
        /// </summary>
        public ResourceLibrary ResourceLibrary = new ResourceLibrary(Settings.ResourceLocation);
    }

    #region Shared Synthesis Classes
    public class BasicSynthesis : CraftAction
    {
        public BasicSynthesis(CraftActionId actionId, Bitmap actionImage)
        {
            Id = actionId;
            Level = 1;
            Name = "制作";
            Cost = 0;
            Description = "消耗耐久以推动作业进展.";
            Efficiency = "100%";
            SuccesRate = "90%";
            Crossclass = false;
            Image = actionImage;
        }
    }

    public class StandardSynthesis : CraftAction
    {
        public StandardSynthesis(CraftActionId actionId, Bitmap actionImage)
        {
            Id = actionId;
            Level = 31;
            Name = "中级制作";
            Cost = 15;
            Description = "消耗耐久以推动作业进展.";
            Efficiency = "150%";
            SuccesRate = "90%";
            Crossclass = false;
            Image = actionImage;
        }
    }
    #endregion

    #region Shared Touch Classes
    public class BasicTouch : CraftAction
    {
        public BasicTouch(CraftActionId actionId, Bitmap actionImage)
        {
            Id = actionId;
            Level = 5;
            Name = "加工";
            Cost = 18;
            Description = "消耗耐久以提高制品品质.";
            Efficiency = "100%";
            SuccesRate = "70%";
            Crossclass = false;
            Image = actionImage;
        }
    }

    public class StandardTouch : CraftAction
    {
        public StandardTouch(CraftActionId actionId, Bitmap actionImage)
        {
            Id = actionId;
            Level = 18;
            Name = "中级加工";
            Cost = 32;
            Description = "消耗耐久以提高制品品质.";
            Efficiency = "125%";
            SuccesRate = "80%";
            Crossclass = false;
            Image = actionImage;
        }
    }

    public class AdvancedTouch : CraftAction
    {
        public AdvancedTouch(CraftActionId actionId, Bitmap actionImage)
        {
            Id = actionId;
            Level = 43;
            Name = "上级加工";
            Cost = 48;
            Description = "消耗耐久以提高制品品质.";
            Efficiency = "150%";
            SuccesRate = "90%";
            Crossclass = false;
            Image = actionImage;
        }
    }

    #endregion

    #region Shared Helper Classes

    public class MastersMend : CraftAction
    {
        public MastersMend(CraftActionId actionId)
        {
            Id = actionId;
            Level = 7;
            Name = "精修";
            Cost = 92;
            Description = "恢复30点耐久.";
            Crossclass = false;
            Image = ResourceLibrary.all_MastersMend;
        }
    }

    public class MastersMendII : CraftAction
    {
        public MastersMendII(CraftActionId actionId)
        {
            Id = actionId;
            Level = 25;
            Name = "精修 II";
            Cost = 160;
            Description = "恢复60点耐久.";
            Crossclass = false;
            Image = ResourceLibrary.all_MastersMendII;
        }
    }

    public class SteadyHand : CraftAction
    {
        public SteadyHand(CraftActionId actionId)
        {
            Id = actionId;
            Level = 9;
            Name = "稳手";
            Cost = 22;
            Description = "技能成功率提高20%, 五次作业内有效.";
            Crossclass = false;
            Image = ResourceLibrary.all_SteadyHand;
        }
    }

    public class InnerQuiet : CraftAction
    {
        public InnerQuiet(CraftActionId actionId)
        {
            Id = actionId;
            Level = 11;
            Name = "内静";
            Cost = 18;
            Description = "加工精度会随着加工系作业成功次数提高.";
            Crossclass = false;
            Image = ResourceLibrary.all_InnerQuiet;
        }
    }

    public class Observe : CraftAction
    {
        public Observe(CraftActionId actionId)
        {
            Id = actionId;
            Level = 13;
            Name = "观察";
            Cost = 14;
            Description = "放空一次作业，不做任何事.";
            Crossclass = false;
            Image = ResourceLibrary.all_Observe;
        }
    }

    public class GreatStrides : CraftAction
    {
        public GreatStrides(CraftActionId actionId)
        {
            Id = actionId;
            Level = 21;
            Name = "阔步";
            Cost = 32;
            Description = "令下一次使用的加工系技能效果变为2倍, 3次作业内有效.";
            Crossclass = false;
            Image = ResourceLibrary.all_GreatStrides;
        }
    }
    #endregion

    #region shared 3.0 HeavenSward skills

    public class CollectableSynthesis : CraftAction
    {
        public CollectableSynthesis(CraftActionId actionId)
        {
            Id = actionId;
            Level = 50;
            Name = "收藏品制作";
            Cost = 0;
            Description = "附加将特定配方制作为收藏品的状态";
            Efficiency = "0";
            SuccesRate = "100%";
            Crossclass = false;
            Image = ResourceLibrary.all_CollectableSynthesis;
        }
    }

    public class ByregotsBrow : CraftAction
    {
        public ByregotsBrow(CraftActionId actionId)
        {
            Id = actionId;
            Level = 51;
            Name = "比尔格的技巧";
            Cost = 18;
            Description = "消耗耐久以提高制品品质";
            Efficiency = "0";
            SuccesRate = "70%";
            Crossclass = false;
            Image = ResourceLibrary.all_ByregotsBrow;
        }
    }

    public class PreciseTouch : CraftAction
    {
        public PreciseTouch(CraftActionId actionId, Bitmap actionImage)
        {
            Id = actionId;
            Level = 53;
            Name = "集中加工";
            Cost = 18;
            Description = "消耗耐久以提高制品品质";
            Efficiency = "0";
            SuccesRate = "100%";
            Crossclass = false;
            Image = actionImage;
        }
    }
    #endregion

    #region Carpenter CrossClass
    public class Rumination : CraftAction
    {
        public Rumination()
        {
            Id = CraftActionId.crp_Rumination;
            Level = 15;
            Name = "松弛";
            Cost = 0;
            Description = "结束内静效果，恢复部分制作力.";
            Crossclass = true;
            Image = ResourceLibrary.crp_Rumination;
        }
    }

    public class BrandOfWind : CraftAction
    {
        public BrandOfWind()
        {
            Id = CraftActionId.crp_BrandOfWind;
            Level = 37;
            Name = "风之印记";
            Cost = 6;
            Description = "消耗耐久以推动作业进展, 处理风属性配方时效率为2倍.";
            Efficiency = "100% (200%)";
            SuccesRate = "90%";
            Crossclass = true;
            Image = ResourceLibrary.crp_BrandOfWind;
        }
    }

    public class ByregotsBlessing : CraftAction
    {
        public ByregotsBlessing()
        {
            Id = CraftActionId.crp_ByregotsBlessing;
            Level = 50;
            Name = "比尔格的祝福";
            Cost = 24;
            Description = "消耗耐久以提高制品品质.";
            Efficiency = "100% plus 20% for each bonus to control granted by Inner Quiet";
            SuccesRate = "90%";
            Crossclass = true;
            Image = ResourceLibrary.crp_ByregotsBlessing;
        }
    }

    public class NameofWind : CraftAction
    {
        public NameofWind()
        {
            Id = CraftActionId.crp_NameofWind;
            Level = 54;
            Name = "风之美名";
            Cost = 15;
            Description = "根据剩余的工作量多少，提高风之印记的效果.";
            Efficiency = "100%";
            SuccesRate = "100%";
            Crossclass = true;
            Image = ResourceLibrary.crp_NameofWind;
        }
    }
    #endregion

    #region Blacksmith CrossClass

    public class Ingenuity : CraftAction
    {
        public Ingenuity()
        {
            Id = CraftActionId.bsm_Ingenuity;
            Level = 15;
            Name = "新颖";
            Cost = 24;
            Description = "令配方等级稍微下降，5次作业内有效.";
            Crossclass = true;
            Image = ResourceLibrary.bsm_Ingenuity;
        }
    }

    public class BrandOfFire : CraftAction
    {
        public BrandOfFire()
        {
            Id = CraftActionId.bsm_BrandOfFire;
            Level = 37;
            Name = "火之印记";
            Cost = 6;
            Description = "消耗耐久以推动作业进展, 处理火属性配方时效率为2倍.";
            Efficiency = "100% (200%)";
            SuccesRate = "90%";
            Crossclass = true;
            Image = ResourceLibrary.bsm_BrandOfFire;
        }
    }

    public class IngenuityII : CraftAction
    {
        public IngenuityII()
        {
            Id = CraftActionId.bsm_IngenuityII;
            Level = 50;
            Name = "新颖 II";
            Cost = 32;
            Description = "令配方等级下降，5次作业内有效.";
            Crossclass = true;
            Image = ResourceLibrary.bsm_IngenuityII;
        }
    }

    public class NameofFire : CraftAction
    {
        public NameofFire()
        {
            Id = CraftActionId.bsm_NameofFire;
            Level = 54;
            Name = "火之美名";
            Cost = 15;
            Description = "根据剩余的工作量多少，提高火之印记的效果.";
            Efficiency = "100%";
            SuccesRate = "100%";
            Crossclass = true;
            Image = ResourceLibrary.bsm_NameofFire;
        }
    }
    #endregion

    #region Armorer CrossClass
    public class RapidSynthesis : CraftAction
    {
        public RapidSynthesis()
        {
            Id = CraftActionId.arm_RapidSynthesis;
            Level = 15;
            Name = "高速制作";
            Cost = 0;
            Description = "消耗耐久以推动作业进展.";
            Efficiency = "250%";
            SuccesRate = "50%";
            Crossclass = true;
            Image = ResourceLibrary.arm_RapidSynthesis;
        }
    }

    public class BrandOfIce : CraftAction
    {
        public BrandOfIce()
        {
            Id = CraftActionId.arm_BrandOfIce;
            Level = 37;
            Name = "冰之印记";
            Cost = 6;
            Description = "消耗耐久以推动作业进展, 处理冰属性配方时效率为2倍.";
            Efficiency = "100% (200%)";
            SuccesRate = "90%";
            Crossclass = true;
            Image = ResourceLibrary.arm_BrandOfIce;
        }
    }

    public class PieceByPiece : CraftAction
    {
        public PieceByPiece()
        {
            Id = CraftActionId.arm_PieceByPiece;
            Level = 50;
            Name = "渐进";
            Cost = 15;
            Description = "消耗耐久完成剩余工作量的 1/3.";
            Crossclass = true;
            Image = ResourceLibrary.arm_PieceByPiece;
        }
    }

    public class NameofIce : CraftAction
    {
        public NameofIce()
        {
            Id = CraftActionId.arm_NameofIce;
            Level = 54;
            Name = "冰之美名";
            Cost = 15;
            Description = "根据剩余的工作量多少，提高冰之印记的效果.";
            Efficiency = "100%";
            SuccesRate = "100%";
            Crossclass = true;
            Image = ResourceLibrary.arm_NameofIce;
        }
    }

    #endregion

    #region Goldsmith CrossClass
    public class Manipulation : CraftAction
    {
        public Manipulation()
        {
            Id = CraftActionId.gsm_Manipulation;
            Level = 15;
            Name = "掌握";
            Cost = 88;
            Description = "每次作业结束时恢复10点耐久，3次作业内有效.";
            Crossclass = true;
            Image = ResourceLibrary.gsm_Manipulation;
        }
    }

    public class FlawlessSynthesis : CraftAction
    {
        public FlawlessSynthesis()
        {
            Id = CraftActionId.gsm_FlawlessSynthesis;
            Level = 37;
            Name = "坚实制作";
            Cost = 15;
            Description = "消耗耐久以推动作业进展40.";
            SuccesRate = "90%";
            Crossclass = true;
            Image = ResourceLibrary.gsm_FlawlessSynthesis;
        }
    }

    public class Innovation : CraftAction
    {
        public Innovation()
        {
            Id = CraftActionId.gsm_Innovation;
            Level = 50;
            Name = "改革";
            Cost = 18;
            Description = "加工精度提高50%，3次作业内有效.";
            Crossclass = true;
            Image = ResourceLibrary.gsm_Innovation;
        }
    }

    public class MakersMark : CraftAction
    {
        public MakersMark()
        {
            Id = CraftActionId.gsm_MakersMark;
            Level = 54;
            Name = "坚实的心得";
            Cost = 20;
            Description = "根据剩余的工作量多少，令数次作业内发动的坚实制作不消耗制作力和耐久.";
            Efficiency = "100%";
            SuccesRate = "90%";
            Crossclass = true;
            Image = ResourceLibrary.gsm_MakersMark;
        }
    }
    #endregion

    #region Leatherworker CrossClass
    public class WasteNot : CraftAction
    {
        public WasteNot()
        {
            Id = CraftActionId.ltw_WasteNot;
            Level = 15;
            Name = "俭约";
            Cost = 56;
            Description = "耐久的消耗量减少50%，4次作业内有效.";
            Crossclass = true;
            Image = ResourceLibrary.ltw_WasteNot;
        }
    }

    public class BrandOfEarth : CraftAction
    {
        public BrandOfEarth()
        {
            Id = CraftActionId.ltw_BrandOfEarth;
            Level = 37;
            Name = "土之印记";
            Cost = 6;
            Description = "消耗耐久以推动作业进展, 处理土属性配方时效率为2倍.";
            Efficiency = "100% (200%)";
            SuccesRate = "90%";
            Crossclass = true;
            Image = ResourceLibrary.ltw_BrandOfEarth;
        }
    }

    public class WasteNotII : CraftAction
    {
        public WasteNotII()
        {
            Id = CraftActionId.ltw_WasteNotII;
            Level = 50;
            Name = "俭约 II";
            Cost = 98;
            Description = "耐久的消耗量减少50%，8次作业内有效.";
            Crossclass = true;
            Image = ResourceLibrary.ltw_WasteNotII;
        }
    }

    public class NameofEarth : CraftAction
    {
        public NameofEarth()
        {
            Id = CraftActionId.ltw_NameofEarth;
            Level = 54;
            Name = "土之美名";
            Cost = 15;
            Description = "根据剩余的工作量多少，提高土之印记的效果.";
            Efficiency = "100%";
            SuccesRate = "100%";
            Crossclass = true;
            Image = ResourceLibrary.ltw_NameofEarth;
        }
    }
    #endregion

    #region Weaver CrossClass
    public class CarefulSynthesis : CraftAction
    {
        public CarefulSynthesis()
        {
            Id = CraftActionId.wvr_CarefulSynthesis;
            Level = 15;
            Name = "模范制作";
            Cost = 0;
            Description = "消耗耐久以推动作业进展.";
            Efficiency = "90%";
            SuccesRate = "100%";
            Crossclass = true;
            Image = ResourceLibrary.wvr_CarefulSynthesis;
        }
    }

    public class BrandOfLightning : CraftAction
    {
        public BrandOfLightning()
        {
            Id = CraftActionId.wvr_BrandOfLightning;
            Level = 37;
            Name = "雷之印记";
            Cost = 6;
            Description = "消耗耐久以推动作业进展, 处理雷属性配方时效率为2倍.";
            Efficiency = "100% (200%)";
            SuccesRate = "90%";
            Crossclass = true;
            Image = ResourceLibrary.wvr_BrandOfLightning;
        }
    }

    public class CarefulSynthesisII : CraftAction
    {
        public CarefulSynthesisII()
        {
            Id = CraftActionId.wvr_CarefulSynthesisII;
            Level = 50;
            Name = "模范制作 II";
            Cost = 0;
            Description = "消耗耐久以推动作业进展.";
            Efficiency = "120%";
            SuccesRate = "100%";
            Crossclass = true;
            Image = ResourceLibrary.wvr_CarefulSynthesisII;
        }
    }

    public class NameofLightning : CraftAction
    {
        public NameofLightning()
        {
            Id = CraftActionId.wvr_NameofLightning;
            Level = 54;
            Name = "雷之美名";
            Cost = 15;
            Description = "根据剩余的工作量多少，提高雷之印记的效果.";
            Efficiency = "100%";
            SuccesRate = "100%";
            Crossclass = true;
            Image = ResourceLibrary.wvr_NameofLightning;
        }
    }
    #endregion

    #region Alchemist CrossClass
    public class TricksOfTheTrade : CraftAction
    {
        public TricksOfTheTrade()
        {
            Id = CraftActionId.alc_TricksOfTheTrade;
            Level = 15;
            Name = "秘诀";
            Cost = 0;
            Description = "恢复20点制作力，只有在高品质状态下才能使用.";
            Crossclass = true;
            Image = ResourceLibrary.alc_TricksOfTheTrade;
        }
    }

    public class BrandOfWater : CraftAction
    {
        public BrandOfWater()
        {
            Id = CraftActionId.alc_BrandOfWater;
            Level = 37;
            Name = "水之印记";
            Cost = 6;
            Description = "消耗耐久以推动作业进展, 处理水属性配方时效率为2倍.";
            Efficiency = "100% (200%)";
            SuccesRate = "90%";
            Crossclass = true;
            Image = ResourceLibrary.alc_BrandOfWater;
        }
    }

    public class ComfortZone : CraftAction
    {
        public ComfortZone()
        {
            Id = CraftActionId.alc_ComfortZone;
            Level = 50;
            Name = "安逸";
            Cost = 66;
            Description = "每次作业结束时恢复8点制作力，10次作业内有效.";
            Crossclass = true;
            Image = ResourceLibrary.alc_ComfortZone;
        }
    }

    public class NameofWater : CraftAction
    {
        public NameofWater()
        {
            Id = CraftActionId.alc_NameofWater;
            Level = 54;
            Name = "水之美名";
            Cost = 15;
            Description = "根据剩余的工作量多少，提高水之印记的效果.";
            Efficiency = "100%";
            SuccesRate = "100%";
            Crossclass = true;
            Image = ResourceLibrary.alc_NameofWater;
        }
    }
    #endregion

    #region Culinarian CrossClass
    public class HastyTouch : CraftAction
    {
        public HastyTouch()
        {
            Id = CraftActionId.cul_HastyTouch;
            Level = 15;
            Name = "仓促";
            Cost = 0;
            Description = "消耗耐久以提高制品品质不会消耗制作力.";
            Efficiency = "100%";
            SuccesRate = "50%";
            Crossclass = true;
            Image = ResourceLibrary.cul_HastyTouch;
        }
    }

    public class SteadyHandII : CraftAction
    {
        public SteadyHandII()
        {
            Id = CraftActionId.cul_SteadyHandII;
            Level = 37;
            Name = "稳手 II";
            Cost = 25;
            Description = "技能成功率提高30%，5次作业内有效.";
            Crossclass = true;
            Image = ResourceLibrary.cul_SteadyHandII;
        }
    }

    public class Reclaim : CraftAction
    {
        public Reclaim()
        {
            Id = CraftActionId.cul_Reclaim;
            Level = 50;
            Name = "回收";
            Cost = 55;
            Description = "制作失败时拿回素材的几率变为90%.";
            Crossclass = true;
            Image = ResourceLibrary.cul_Reclaim;
        }
    }

    public class MuscleMemory : CraftAction
    {
        public MuscleMemory()
        {
            Id = CraftActionId.cul_MuscleMemory;
            Level = 54;
            Name = "坚信";
            Cost = 6;
            Description = "消耗耐久以完成工作量的1/3.";
            Efficiency = "100%";
            SuccesRate = "100%";
            Crossclass = true;
            Image = ResourceLibrary.cul_MuscleMemory;
        }
    }
    #endregion

}
