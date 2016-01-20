using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Resources;
using CraftExportEx;

namespace CraftExport.Helpers
{
    public class ResourceLibrary
    {
        #region Public Bitmap Fields
       
        // App Icons
        public Bitmap app_Reset;

        // Class Icons
        public Bitmap cls_Alchemist;
        public Bitmap cls_Armorer;
        public Bitmap cls_Blacksmith;
        public Bitmap cls_Carpenter;
        public Bitmap cls_Culinarian;
        public Bitmap cls_Goldsmith;
        public Bitmap cls_Leatherworker;
        public Bitmap cls_Weaver;

        // Shared Crafting Skill Icons
        public Bitmap all_GreatStrides;
        public Bitmap all_InnerQuiet;
        public Bitmap all_MastersMend;
        public Bitmap all_MastersMendII;
        public Bitmap all_Observe;
        public Bitmap all_SteadyHand;
        public Bitmap all_CollectableSynthesis;
        public Bitmap all_ByregotsBrow;

        // Carpenter Crafting Skill Icons
        public Bitmap crp_BrandOfWind;
        public Bitmap crp_ByregotsBlessing;
        public Bitmap crp_Rumination;
        public Bitmap crp_NameofWind;
        public Bitmap crp_PreciseTouch;

        // Blacksmith Crafting Skill Icons
        public Bitmap bsm_BrandOfFire;
        public Bitmap bsm_Ingenuity;
        public Bitmap bsm_IngenuityII;
        public Bitmap bsm_NameofFire;
        public Bitmap bsm_PreciseTouch;

        // Armorer Crafting Skill Icons
        public Bitmap arm_BrandOfIce;
        public Bitmap arm_PieceByPiece;
        public Bitmap arm_RapidSynthesis;
        public Bitmap arm_NameofIce;
        public Bitmap arm_PreciseTouch;

        // Goldsmith Crafting Skill Icons
        public Bitmap gsm_FlawlessSynthesis;
        public Bitmap gsm_Innovation;
        public Bitmap gsm_Manipulation;
        public Bitmap gsm_MakersMark;
        public Bitmap gsm_PreciseTouch;

        // Leatherworker Crafting Skill Icons
        public Bitmap ltw_BrandOfEarth;
        public Bitmap ltw_WasteNot;
        public Bitmap ltw_WasteNotII;
        public Bitmap ltw_NameofEarth;
        public Bitmap ltw_PreciseTouch;

        // Weaver Crafting Skill Icons
        public Bitmap wvr_BrandOfLightning;
        public Bitmap wvr_CarefulSynthesis;
        public Bitmap wvr_CarefulSynthesisII;
        public Bitmap wvr_NameofLightning;
        public Bitmap wvr_PreciseTouch;

        // Alchemist Crafting Skill Icons
        public Bitmap alc_BrandOfWater;
        public Bitmap alc_ComfortZone;
        public Bitmap alc_TricksOfTheTrade;
        public Bitmap alc_NameofWater;
        public Bitmap alc_PreciseTouch;

        // Culinarian Crafting Skill Icons
        public Bitmap cul_HastyTouch;
        public Bitmap cul_Reclaim;
        public Bitmap cul_SteadyHandII;
        public Bitmap cul_MuscleMemory;
        public Bitmap cul_PreciseTouch;

        #endregion

        /// <summary>
        /// Publically accessible list to hold class specific images.
        /// </summary>
        public Dictionary<string, Bitmap> ClassSkillImages = new Dictionary<string, Bitmap>();

        /// <summary>
        /// Populates all the publically accessible images for use on load.
        /// </summary>
        /// <param name="resourceSet"></param>
        private void SetLocalImages(ResourceSet resourceSet)
        {
            // Populate the app images.
            app_Reset = (Bitmap)resourceSet.GetObject("app_reset");

            // Populate the class images Original One
            //cls_Alchemist = (Bitmap)resourceSet.GetObject("cls_Alchemist");
            //cls_Armorer = (Bitmap)resourceSet.GetObject("cls_Armorer");
            //cls_Blacksmith = (Bitmap)resourceSet.GetObject("cls_Blacksmith");
            //cls_Carpenter = (Bitmap)resourceSet.GetObject("cls_Carpenter");
            //cls_Culinarian = (Bitmap)resourceSet.GetObject("cls_Culinarian");
            //cls_Goldsmith = (Bitmap)resourceSet.GetObject("cls_Goldsmith");
            //cls_Leatherworker = (Bitmap)resourceSet.GetObject("cls_Leatherworker");
            //cls_Weaver = (Bitmap)resourceSet.GetObject("cls_Weaver");
            cls_Alchemist = (Bitmap)resourceSet.GetObject("Alc");
            cls_Armorer = (Bitmap)resourceSet.GetObject("Arm");
            cls_Blacksmith = (Bitmap)resourceSet.GetObject("Bsm");
            cls_Carpenter = (Bitmap)resourceSet.GetObject("Cpt");
            cls_Culinarian = (Bitmap)resourceSet.GetObject("Cul");
            cls_Goldsmith = (Bitmap)resourceSet.GetObject("Gsm");
            cls_Leatherworker = (Bitmap)resourceSet.GetObject("Ltw");
            cls_Weaver = (Bitmap)resourceSet.GetObject("Wvr");

            // Populate the baseskill images.
            all_GreatStrides = (Bitmap)resourceSet.GetObject("all_GreatStrides");
            all_InnerQuiet = (Bitmap)resourceSet.GetObject("all_InnerQuiet");
            all_MastersMend = (Bitmap)resourceSet.GetObject("all_MastersMend");
            all_MastersMendII = (Bitmap)resourceSet.GetObject("all_MastersMendII");
            all_Observe = (Bitmap)resourceSet.GetObject("all_Observe");
            all_SteadyHand = (Bitmap)resourceSet.GetObject("all_SteadyHand");
            all_CollectableSynthesis = (Bitmap)resourceSet.GetObject("all_CollectableSynthesis");
            all_ByregotsBrow = (Bitmap)resourceSet.GetObject("all_ByregotsBrow");


            // Carpenter Crafting Skill Icons
            crp_BrandOfWind = (Bitmap)resourceSet.GetObject("crp_BrandOfWind");
            crp_ByregotsBlessing = (Bitmap)resourceSet.GetObject("crp_ByregotsBlessing");
            crp_Rumination = (Bitmap)resourceSet.GetObject("crp_Rumination");
            crp_NameofWind = (Bitmap)resourceSet.GetObject("crp_NameofWind");

            // Blacksmith Crafting Skill Icons
            bsm_BrandOfFire = (Bitmap)resourceSet.GetObject("bsm_BrandOfFire");
            bsm_Ingenuity = (Bitmap)resourceSet.GetObject("bsm_Ingenuity");
            bsm_IngenuityII = (Bitmap)resourceSet.GetObject("bsm_IngenuityII");
            bsm_NameofFire = (Bitmap)resourceSet.GetObject("bsm_NameofFire");

            // Armorer Crafting Skill Icons
            arm_BrandOfIce = (Bitmap)resourceSet.GetObject("arm_BrandOfIce");
            arm_PieceByPiece = (Bitmap)resourceSet.GetObject("arm_PieceByPiece");
            arm_RapidSynthesis = (Bitmap)resourceSet.GetObject("arm_RapidSynthesis");
            arm_NameofIce = (Bitmap)resourceSet.GetObject("arm_NameofIce");

            // Goldsmith Crafting Skill Icons
            gsm_FlawlessSynthesis = (Bitmap)resourceSet.GetObject("gsm_FlawlessSynthesis");
            gsm_Innovation = (Bitmap)resourceSet.GetObject("gsm_Innovation");
            gsm_Manipulation = (Bitmap)resourceSet.GetObject("gsm_Manipulation");
            gsm_MakersMark = (Bitmap)resourceSet.GetObject("gsm_MakersMark");

            // Leatherworker Crafting Skill Icons
            ltw_BrandOfEarth = (Bitmap)resourceSet.GetObject("ltw_BrandOfEarth");
            ltw_WasteNot = (Bitmap)resourceSet.GetObject("ltw_WasteNot");
            ltw_WasteNotII = (Bitmap)resourceSet.GetObject("ltw_WasteNotII");
            ltw_NameofEarth = (Bitmap)resourceSet.GetObject("ltw_NameofEarth");

            // Weaver Crafting Skill Icons
            wvr_BrandOfLightning = (Bitmap)resourceSet.GetObject("wvr_BrandOfLightning");
            wvr_CarefulSynthesis = (Bitmap)resourceSet.GetObject("wvr_CarefulSynthesis");
            wvr_CarefulSynthesisII = (Bitmap)resourceSet.GetObject("wvr_CarefulSynthesisII");
            wvr_NameofLightning = (Bitmap)resourceSet.GetObject("wvr_NameofLightning");

            // Alchemist Crafting Skill Icons
            alc_BrandOfWater = (Bitmap)resourceSet.GetObject("alc_BrandOfWater");
            alc_ComfortZone = (Bitmap)resourceSet.GetObject("alc_ComfortZone");
            alc_TricksOfTheTrade = (Bitmap)resourceSet.GetObject("alc_TricksOfTheTrade");
            alc_NameofWater = (Bitmap)resourceSet.GetObject("alc_NameofWater");

            // Culinarian Crafting Skill Icons
            cul_HastyTouch = (Bitmap)resourceSet.GetObject("cul_HastyTouch");
            cul_Reclaim = (Bitmap)resourceSet.GetObject("cul_Reclaim");
            cul_SteadyHandII = (Bitmap)resourceSet.GetObject("cul_SteadyHandII");
            cul_MuscleMemory = (Bitmap)resourceSet.GetObject("cul_MuscleMemory");

        }

        /// <summary>
        /// Populates the ClassImageList based on the passed Class Type.
        /// </summary>
        /// <param name="resourceSet"></param>
        /// <param name="classType"></param>
        private void LoadImageList(ResourceSet resourceSet, ClassJobType classType)
        {
            // Clear the current entries.
            ClassSkillImages.Clear();
            // Switch based on the class type.
            switch (classType)
            {
                case ClassJobType.Carpenter:
                    ClassSkillImages.Add("crp_BasicSynthesis", (Bitmap)resourceSet.GetObject("crp_BasicSynthesis"));
                    ClassSkillImages.Add("crp_StandardSynthesis", (Bitmap)resourceSet.GetObject("crp_StandardSynthesis"));
                    ClassSkillImages.Add("crp_BasicTouch", (Bitmap)resourceSet.GetObject("crp_BasicTouch"));
                    ClassSkillImages.Add("crp_StandardTouch", (Bitmap)resourceSet.GetObject("crp_StandardTouch"));
                    ClassSkillImages.Add("crp_AdvancedTouch", (Bitmap)resourceSet.GetObject("crp_AdvancedTouch"));
                    ClassSkillImages.Add("crp_PreciseTouch", (Bitmap)resourceSet.GetObject("crp_PreciseTouch"));
                    break;
                case ClassJobType.Blacksmith:
                    ClassSkillImages.Add("bsm_BasicSynthesis", (Bitmap)resourceSet.GetObject("bsm_BasicSynthesis"));
                    ClassSkillImages.Add("bsm_StandardSynthesis", (Bitmap)resourceSet.GetObject("bsm_StandardSynthesis"));
                    ClassSkillImages.Add("bsm_BasicTouch", (Bitmap)resourceSet.GetObject("bsm_BasicTouch"));
                    ClassSkillImages.Add("bsm_StandardTouch", (Bitmap)resourceSet.GetObject("bsm_StandardTouch"));
                    ClassSkillImages.Add("bsm_AdvancedTouch", (Bitmap)resourceSet.GetObject("bsm_AdvancedTouch"));
                    ClassSkillImages.Add("bsm_PreciseTouch", (Bitmap)resourceSet.GetObject("bsm_PreciseTouch"));
                    break;
                case ClassJobType.Armorer:
                    ClassSkillImages.Add("arm_BasicSynthesis", (Bitmap)resourceSet.GetObject("arm_BasicSynthesis"));
                    ClassSkillImages.Add("arm_StandardSynthesis", (Bitmap)resourceSet.GetObject("arm_StandardSynthesis"));
                    ClassSkillImages.Add("arm_BasicTouch", (Bitmap)resourceSet.GetObject("arm_BasicTouch"));
                    ClassSkillImages.Add("arm_StandardTouch", (Bitmap)resourceSet.GetObject("arm_StandardTouch"));
                    ClassSkillImages.Add("arm_AdvancedTouch", (Bitmap)resourceSet.GetObject("arm_AdvancedTouch"));
                    ClassSkillImages.Add("arm_PreciseTouch", (Bitmap)resourceSet.GetObject("arm_PreciseTouch"));
                    break;

                case ClassJobType.Goldsmith:
                    ClassSkillImages.Add("gsm_BasicSynthesis", (Bitmap)resourceSet.GetObject("gsm_BasicSynthesis"));
                    ClassSkillImages.Add("gsm_StandardSynthesis", (Bitmap)resourceSet.GetObject("gsm_StandardSynthesis"));
                    ClassSkillImages.Add("gsm_BasicTouch", (Bitmap)resourceSet.GetObject("gsm_BasicTouch"));
                    ClassSkillImages.Add("gsm_StandardTouch", (Bitmap)resourceSet.GetObject("gsm_StandardTouch"));
                    ClassSkillImages.Add("gsm_AdvancedTouch", (Bitmap)resourceSet.GetObject("gsm_AdvancedTouch"));
                    ClassSkillImages.Add("gsm_PreciseTouch", (Bitmap)resourceSet.GetObject("gsm_PreciseTouch"));
                    break;
                case ClassJobType.Leatherworker:
                    ClassSkillImages.Add("ltw_BasicSynthesis", (Bitmap)resourceSet.GetObject("ltw_BasicSynthesis"));
                    ClassSkillImages.Add("ltw_StandardSynthesis", (Bitmap)resourceSet.GetObject("ltw_StandardSynthesis"));
                    ClassSkillImages.Add("ltw_BasicTouch", (Bitmap)resourceSet.GetObject("ltw_BasicTouch"));
                    ClassSkillImages.Add("ltw_StandardTouch", (Bitmap)resourceSet.GetObject("ltw_StandardTouch"));
                    ClassSkillImages.Add("ltw_AdvancedTouch", (Bitmap)resourceSet.GetObject("ltw_AdvancedTouch"));
                    ClassSkillImages.Add("ltw_PreciseTouch", (Bitmap)resourceSet.GetObject("ltw_PreciseTouch"));
                    break;
                case ClassJobType.Weaver:
                    ClassSkillImages.Add("wvr_BasicSynthesis", (Bitmap)resourceSet.GetObject("wvr_BasicSynthesis"));
                    ClassSkillImages.Add("wvr_StandardSynthesis", (Bitmap)resourceSet.GetObject("wvr_StandardSynthesis"));
                    ClassSkillImages.Add("wvr_BasicTouch", (Bitmap)resourceSet.GetObject("wvr_BasicTouch"));
                    ClassSkillImages.Add("wvr_StandardTouch", (Bitmap)resourceSet.GetObject("wvr_StandardTouch"));
                    ClassSkillImages.Add("wvr_AdvancedTouch", (Bitmap)resourceSet.GetObject("wvr_AdvancedTouch"));
                    ClassSkillImages.Add("wvr_PreciseTouch", (Bitmap)resourceSet.GetObject("wvr_PreciseTouch"));
                    break;
                case ClassJobType.Alchemist:
                    ClassSkillImages.Add("alc_BasicSynthesis", (Bitmap)resourceSet.GetObject("alc_BasicSynthesis"));
                    ClassSkillImages.Add("alc_StandardSynthesis", (Bitmap)resourceSet.GetObject("alc_StandardSynthesis"));
                    ClassSkillImages.Add("alc_BasicTouch", (Bitmap)resourceSet.GetObject("alc_BasicTouch"));
                    ClassSkillImages.Add("alc_StandardTouch", (Bitmap)resourceSet.GetObject("alc_StandardTouch"));
                    ClassSkillImages.Add("alc_AdvancedTouch", (Bitmap)resourceSet.GetObject("alc_AdvancedTouch"));
                    ClassSkillImages.Add("alc_PreciseTouch", (Bitmap)resourceSet.GetObject("alc_PreciseTouch"));
                    break;
                case ClassJobType.Culinarian:
                    ClassSkillImages.Add("cul_BasicSynthesis", (Bitmap)resourceSet.GetObject("cul_BasicSynthesis"));
                    ClassSkillImages.Add("cul_StandardSynthesis", (Bitmap)resourceSet.GetObject("cul_StandardSynthesis"));
                    ClassSkillImages.Add("cul_BasicTouch", (Bitmap)resourceSet.GetObject("cul_BasicTouch"));
                    ClassSkillImages.Add("cul_StandardTouch", (Bitmap)resourceSet.GetObject("cul_StandardTouch"));
                    ClassSkillImages.Add("cul_AdvancedTouch", (Bitmap)resourceSet.GetObject("cul_AdvancedTouch"));
                    ClassSkillImages.Add("cul_PreciseTouch", (Bitmap)resourceSet.GetObject("cul_PreciseTouch"));
                    break;
            }
        } 

        /// <summary>
        /// ResourceLibrary constructor.
        /// </summary>
        /// <param name="resourceLocation"></param>
        public ResourceLibrary(string resourceLocation)
        {
            // Create a resource set from our assembly based on the resource location.
            string projectName = Assembly.GetExecutingAssembly().GetName().Name.ToString();
            ResourceSet resourceSet = new ResourceSet( Assembly.GetExecutingAssembly().GetManifestResourceStream(projectName + ".Properties.Resources.resources"));
            // Set all the Images.
            SetLocalImages(resourceSet);
        }

        /// <summary>
        /// Resource ibrary overloaded constructor that accepts a classtype.
        /// Calls the LoadImageList with the resource set and passed classtype.
        /// </summary>
        /// <param name="resourceLocation"></param>
        /// <param name="classType"></param>
        public ResourceLibrary(string resourceLocation, ClassJobType classType)
        {
            // Create a resource set from our assembly based on the resource location.
            string projectName = Assembly.GetExecutingAssembly().GetName().Name.ToString();
            ResourceSet resourceSet = new ResourceSet(Assembly.GetExecutingAssembly().GetManifestResourceStream(projectName + ".Properties.Resources.resources"));
            // Populate the ClassImageList by ClassType.
            LoadImageList(resourceSet, classType);
        }

    }
}
