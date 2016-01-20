using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using CraftExport.Helpers;

namespace CraftExportEx
{
    public partial class CraftExportFormNew : Form
    {
        #region Private Properties

        /// <summary>
        /// List to keep track of the actions inside the Action Sequence.
        /// </summary>
        private readonly List<CraftAction> _craftActionList = new List<CraftAction>();

        /// <summary>
        /// Holds the current selected Crafting Class we're creating Crafting Profiles for.
        /// </summary>
        private readonly CraftClass _currentCraftClass = new CraftClass();

        /// <summary>
        /// The complete library of all recipes.
        /// </summary>
        private readonly RecipeLibrary _recipeLibrary = new RecipeLibrary();

        /// <summary>
        /// The library of public resources.
        /// </summary>
        private readonly ResourceLibrary _resourcelibrary = new ResourceLibrary(Settings.ResourceLocation);

        /// <summary>
        /// The library of meals.
        /// </summary>
        private readonly MealLibrary _mealLibrary = new MealLibrary();

        /// <summary>
        /// Food Quality selection for crafting profiles.
        /// </summary>
        private FoodQuality _foodQualitySelection;

        /// <summary>
        /// Food quality selection in string form.
        /// </summary>
        private string _foodQuality = "HqOnly";

        /// <summary>
        /// Material selection for crafting profiles.
        /// </summary>
        private MaterialUsage _materialSelection;

        /// <summary>
        /// Material selection in string form. (0 for all NQ, -1 for HQ first, -2 for NQ first).
        /// </summary>
        private string _materialUsage = "-2, -2, -2, -2, -2, -2";

        /// <summary>
        /// Keeps track of the total CP cost for the profile.
        /// </summary>
        private int _cpCost;

        /// <summary>
        /// Keeps track of how many stack of ComfortZone are still active.
        /// </summary>
        private int _comfortZoneCounter;

        #endregion

        #region Form Init

        public CraftExportFormNew()
        {
            InitializeComponent();
        }

        #endregion        

        #region Form Methods

        private void CraftExportForm_Load(object sender, EventArgs e)
        {
            // Set the standard images used in the form.
            SetStandardImages();

            // Construct the Cross Class skills.
            SetCrossClassAbilities();

            // Set the class to the current character's class type.
            ChangeClass(ClassJobType.Carpenter);

            // Populate the Food dropdown.
            SetMeals();

            Recipe selectedItem = (Recipe)recipeBox.SelectedItem;
            CurrentRecipeId.Text = selectedItem.Id.ToString();
            CurrentItemId.Text = selectedItem.ItemId.ToString();

            // image change bool
            imageA = true;
            imageB = false;
        }

        private void Class_btnClick(object sender, EventArgs e)
        {
            // Define the current button.
            var btn = (Button)sender;

            // Change class based on the button that was pressed.
            ChangeClass((ClassJobType)Convert.ToInt32(btn.Tag));
        }

        private void Skill_btnClick(object sender, EventArgs e)
        {
            // Set the counted objects.
            int counter = Utilities.GetAll(this.ActionSequence, typeof(PictureBox)).Count();

            // Stop if we've reached 99 actions.
            if (counter >= 99)
            {
                MessageBox.Show("达到了制作的最大步骤数");
                return;
            }

            // Define the current button as object.
            var btn = (Button)sender;

            // Get the CraftAction object from the button tag.
            CraftAction craftAction = (CraftAction)btn.Tag;

            if (_comfortZoneCounter > 0 && craftAction.Id == CraftActionId.alc_ComfortZone)
                _cpCost = (_cpCost + 8);

            // Check if comfortzone is active, and replenish 8 CP;
            if (_comfortZoneCounter > 0)
                _cpCost = (_cpCost - 8);

            // Check if comfortzone was used and initiate a counter if so.
            if (craftAction.Id == CraftActionId.alc_ComfortZone)
                _comfortZoneCounter = 11;
            
            // Set the CP cost.
            _cpCost = _cpCost + craftAction.Cost;

            // Define variables for the X and Y position for the added picturebox.
            int pointX = 8;
            int pointY = 20;
            const int distance = 45;

            // If there are any objects inside the action sequence already
            if (counter > 0)
            {
                // Math out the counts.
                int horizontalCount = counter - (counter - counter % 10);
                int verticalCount = ((counter - counter % 10) / 10);

                // Calculate the X and Y positions and set them.
                pointX = (pointX + (distance * horizontalCount));
                pointY = (pointY + (distance * verticalCount));
            }

            // Create a new picturebox to hold our button image.
            var queuedAction = new PictureBox
            {
                Cursor = Cursors.No,
                Location = new Point(pointX, pointY),
                Size = new Size(40, 40),
                BackgroundImage = craftAction.Image,
                BackgroundImageLayout = ImageLayout.Center,
                Tag = craftAction.Cost
            };

            // Construct the tooltip data, and set it to the tooltip.
            toolTip1.ToolTipTitle = craftAction.Name;
            toolTip1.SetToolTip(queuedAction, craftAction.Description);

            // Add the selected action to the list of actions.
            _craftActionList.Add(craftAction);

            // Add the CP cost to the counter.
            CpCounter.Text = _cpCost.ToString();

            // Bind the clickfunction to the Temp Image.
            queuedAction.Click += QueuedAction_btnClick;

            // Add the image to the actionsequence.
            ActionSequence.Controls.Add(queuedAction);

            // If comfortzone is still active, subtrack 1 roud.
            if (_comfortZoneCounter > 0)
                _comfortZoneCounter = _comfortZoneCounter - 1;
        }

        private void QueuedAction_btnClick(object sender, EventArgs e)
        {
            // Define the variable that gets all the pictureboxes inside the ActionSequence.
            int pictureCount = Utilities.GetAll(ActionSequence, typeof(PictureBox)).Count();

            // Cast the sender as a Control object.
            Control ctrl = (Control)sender;

            // Check if the sender is the last object in the sequence, then call method to remove it.
            if (ActionSequence.Controls.IndexOf(ctrl) == pictureCount)
                RemoveLastCraftingStep(ctrl, pictureCount);
        }

        private void Skill_btnHover(object sender, EventArgs e)
        {
            // Define the current button as object.
            var btn = (Button)sender;

            // Get the CraftAction object from the button tag.
            CraftAction craftAction = (CraftAction)btn.Tag;

            // Construct the tooltip data, and set it to the tooltip.
            toolTip1.ToolTipTitle = craftAction.Name;
            toolTip1.SetToolTip(btn, craftAction.Description);
        }

        private void Reset_btnClick(object sender, EventArgs e)
        {
            // Empties the Action Sequence and resets the counters.
            EmptyActions();
            Recipe selectedItem = (Recipe)recipeBox.SelectedItem;
            CurrentRecipeId.Text = selectedItem.Id.ToString();
            CurrentItemId.Text = selectedItem.ItemId.ToString();
        }

        private void EnableFood_checkClick(object sender, EventArgs e)
        {
            // Cast the sender as checkbox.
            CheckBox box = (CheckBox)sender;

            // Enabled/Disable the mealWrapper based on the checkbox state.
            mealWrapper.Enabled = box.Checked;
        }

        private void Food_radioClick(object sender, EventArgs e)
        {
            // Get the radiobittons inside the MealWrapper.
            var r = Utilities.GetAll(mealWrapper, typeof(RadioButton));

            // Check for the checked radiobutton and set the Food Quality Selection property.
            foreach (RadioButton radio in r.Cast<RadioButton>().Where(radio => radio.Checked))
                _foodQualitySelection = (FoodQuality)Convert.ToInt32(radio.Tag);

            // Call the method to populate the food quality string.
            SetFoodQuality();
        }

        private void Material_radioClick(object sender, EventArgs e)
        {
            // Check for the checked radiobutton, and set the Material Selection property.
            foreach (RadioButton radio in matsToUse.Controls.Cast<Control>().Select(control => control as RadioButton).Where(radio => radio.Checked))
                _materialSelection = (MaterialUsage)Convert.ToInt32(radio.Tag);

            // Call the method to populate the material string.
            SetMaterialUsage();
        }

        private void SynthQual_btnCheck(object sender, EventArgs e)
        {
            quantityNum.Enabled = quantityCheck.Checked;
        }

        private void Save_btnClick(object sender, EventArgs e)
        {
            if (!_craftActionList.Any())
            {
                MessageBox.Show("无法保存脚本，尚未选择任何制作技能");
                return;
            }

            // Get the currently selected item from the Recipe Box.
            Recipe selectedItem = (Recipe)recipeBox.SelectedItem;

            // Set the filename based on the selected item.
            saveFileDialog1.FileName = (quantityCheck.Checked)
                                        ? _currentCraftClass.Name + " - " + selectedItem.Name + " (数量_" + quantityNum.Value + ")"
                                        : _currentCraftClass.Name + " - " + selectedItem.Name;

            // Show the save file dialog.
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            // Set the filename from the Save Dialog.
            string fileName = saveFileDialog1.FileName;

            // Export the profile as an XML file to the designated location.
            ExportRecipe(fileName);
        }

        /// <summary>
        /// Display Recipes base on Level and special 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecipeLv_SelectedIndexChanged(object sender, EventArgs e)
        {
            String rawString = this.RecipeLv.Text;
            String[] levelString = rawString.Split('-');
            if (levelString.Length > 1)
            {
                int levelLow = 1;
                int levelHigh = 60;
                try
                {
                    levelLow = Convert.ToInt32(levelString[0]);
                    levelHigh = Convert.ToInt32(levelString[1]);
                }
                catch (Exception ex) { }

                List<Recipe> currentRecipes = _recipeLibrary.LoadRecipes(_currentCraftClass.Type);

                // Create a new instance of the AutoCompleteStringcollection.
                var autoCompleteList = new AutoCompleteStringCollection();
                var inLevelList = new List<Recipe>();
                // Loop through every recipe in the list and add it to the collection.
                foreach (Recipe recipe in currentRecipes)
                {
                    if (recipe.Level <= levelHigh && recipe.Level >= levelLow)
                    {
                        autoCompleteList.Add(recipe.Name);
                        inLevelList.Add(recipe);
                    }
                }

                // Set the recipelist as data source.
                var datasource = currentRecipes;
                recipeBox.DataSource = inLevelList;

                // Set the autocomplete settings for the combobox.
                recipeBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                recipeBox.AutoCompleteCustomSource = autoCompleteList;
                recipeBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            }
            else
            {

                List<Recipe> currentRecipes = _recipeLibrary.LoadRecipes(_currentCraftClass.Type);

                // Create a new instance of the AutoCompleteStringcollection.
                var autoCompleteList = new AutoCompleteStringCollection();
                var inLevelList = new List<Recipe>();
                // Loop through Special Recipe in the dic and add it to the collection.
                List<String> specialRecipeNameList = SpecialRecipeDic._specialRecipeList[rawString];
                foreach (Recipe recipe in currentRecipes)
                {
                    if (specialRecipeNameList.Contains(recipe.Name))
                    {
                        autoCompleteList.Add(recipe.Name);
                        inLevelList.Add(recipe);
                    }
                }

                // Set the recipelist as data source.
                var datasource = currentRecipes;
                recipeBox.DataSource = inLevelList;

                // Set the autocomplete settings for the combobox.
                recipeBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                recipeBox.AutoCompleteCustomSource = autoCompleteList;
                recipeBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            }

        }


        #endregion

        #region Methods

        /// <summary>
        /// Populates buttons and other objects from the Resource Library.
        /// </summary>
        private void SetStandardImages()
        {
            #region AppIcons

            ResetButton.Image = _resourcelibrary.app_Reset;

            #endregion

            #region ClassImages

            Carpenter.Image = _resourcelibrary.cls_Carpenter;
            Blacksmith.Image = _resourcelibrary.cls_Blacksmith;
            Armorer.Image = _resourcelibrary.cls_Armorer;
            Goldsmith.Image = _resourcelibrary.cls_Goldsmith;
            Leatherworker.Image = _resourcelibrary.cls_Leatherworker;
            Weaver.Image = _resourcelibrary.cls_Weaver;
            Alchemist.Image = _resourcelibrary.cls_Alchemist;
            Culinarian.Image = _resourcelibrary.cls_Culinarian;

            #endregion
        }

        /// <summary>
        /// Change the current crafting class based on the passed class parameter.
        /// </summary>
        /// <param name="classType"></param>
        private void ChangeClass(ClassJobType classType)
        {
            // Get the current Active Class button.
            var activeClass = CraftClassGroup.Controls.Find(_currentCraftClass.Type.ToString(), true).FirstOrDefault();

            // If it exists, reset the color.
            if (activeClass != null)
                activeClass.BackColor = DefaultBackColor;

            // Check if the passed class is a craft class and load it, otherwise defaulto to Carpenter
            _currentCraftClass.Load(Utilities.IsCraftingClass(classType) ? classType : ClassJobType.Carpenter);
    
            // Set the active class based on the newly loaded class.
            activeClass = CraftClassGroup.Controls.Find(_currentCraftClass.Type.ToString(), true).FirstOrDefault();

            // If it exists, adjust the color.
            if (activeClass != null)
                activeClass.BackColor = SystemColors.ActiveCaption;

            // Set the base skills for the current craft class.
            SetBaseSkills();

            // Empty the existing action sequence.
            EmptyActions();

            // Load the recipes.
            LoadRecipes();
        }

        /// <summary>
        /// Sets all the buttons for the Cross Class abilities.
        /// Buttons will be disabled if appropriate level is not reached.
        /// </summary>
        private void SetCrossClassAbilities()
        {
            #region Carpenter CrossClass Skills

            // Rumination
            Rumination rumination = new Rumination();
            RuminationBtn.BackgroundImage = _resourcelibrary.crp_Rumination;
            RuminationBtn.Tag = rumination;

            // Brand of Wind 
            BrandOfWind brandOfWind = new BrandOfWind();
            BrandOfWindBtn.BackgroundImage = _resourcelibrary.crp_BrandOfWind;
            BrandOfWindBtn.Tag = brandOfWind;

            // Byregot's Blessing
            ByregotsBlessing byregotsBlessing = new ByregotsBlessing();
            ByregotsBlessingBtn.BackgroundImage = _resourcelibrary.crp_ByregotsBlessing;
            ByregotsBlessingBtn.Tag = byregotsBlessing;

            // Name Of Wind
            NameofWind nameofWind = new NameofWind();
            NameofWindBtn.BackgroundImage = _resourcelibrary.crp_NameofWind;
            NameofWindBtn.Tag = nameofWind;
            #endregion

            #region Blacksmith CrossClass Skills

            // Ingenuity
            Ingenuity ingenuity = new Ingenuity();
            IngenuityBtn.BackgroundImage = _resourcelibrary.bsm_Ingenuity;
            IngenuityBtn.Tag = ingenuity;

            // Brand of Fire
            BrandOfFire brandOfFire = new BrandOfFire();
            BrandOfFireBtn.BackgroundImage = _resourcelibrary.bsm_BrandOfFire;
            BrandOfFireBtn.Tag = brandOfFire;

            // Ingenuity II
            IngenuityII ingenuityII = new IngenuityII();
            IngenuityIIBtn.BackgroundImage = _resourcelibrary.bsm_IngenuityII;
            IngenuityIIBtn.Tag = ingenuityII;

            // Name Of Fire
            NameofFire nameofFire = new NameofFire();
            NameofFireBtn.BackgroundImage = _resourcelibrary.bsm_NameofFire;
            NameofFireBtn.Tag = nameofFire;

            #endregion

            #region Armorer CrossClass Skills

            // Rapid Synthesis
            RapidSynthesis rapidSynthesis = new RapidSynthesis();
            RapidSynthesisBtn.BackgroundImage = _resourcelibrary.arm_RapidSynthesis;
            RapidSynthesisBtn.Tag = rapidSynthesis;

            // Brand of Ice
            BrandOfIce brandOfIce = new BrandOfIce();
            BrandOfIceBtn.BackgroundImage = _resourcelibrary.arm_BrandOfIce;
            BrandOfIceBtn.Tag = brandOfIce;

            // Piece by Piece
            PieceByPiece pieceByPiece = new PieceByPiece();
            PieceByPieceBtn.BackgroundImage = _resourcelibrary.arm_PieceByPiece;
            PieceByPieceBtn.Tag = pieceByPiece;

            // Name Of Ice
            NameofIce nameofIce = new NameofIce();
            NameofIceBtn.BackgroundImage = _resourcelibrary.arm_NameofIce;
            NameofIceBtn.Tag = nameofIce;

            #endregion

            #region Goldsmith CrossClass Skills

            // Manipulation
            Manipulation manipulation = new Manipulation();
            ManipulationBtn.BackgroundImage = _resourcelibrary.gsm_Manipulation;
            ManipulationBtn.Tag = manipulation;

            //FlawlessSynthesis
            FlawlessSynthesis flawlessSynthesis = new FlawlessSynthesis();
            FlawlessSynthesisBtn.BackgroundImage = _resourcelibrary.gsm_FlawlessSynthesis;
            FlawlessSynthesisBtn.Tag = flawlessSynthesis;

            // Innovation
            Innovation innovation = new Innovation();
            InnovationBtn.BackgroundImage = _resourcelibrary.gsm_Innovation;
            InnovationBtn.Tag = innovation;

            // Maker's Mark
            MakersMark makersMark = new MakersMark();
            MakersMarkBtn.BackgroundImage = _resourcelibrary.gsm_MakersMark;
            MakersMarkBtn.Tag = makersMark;
            #endregion

            #region Leatherworker CrossClass Skills

            // Waste Not
            WasteNot wasteNot = new WasteNot();
            WasteNotBtn.BackgroundImage = _resourcelibrary.ltw_WasteNot;
            WasteNotBtn.Tag = wasteNot;

            // Brand of Earth
            BrandOfEarth brandOfEarth = new BrandOfEarth();
            BrandOfEarthBtn.BackgroundImage = _resourcelibrary.ltw_BrandOfEarth;
            BrandOfEarthBtn.Tag = brandOfEarth;

            // Waste Not II
            WasteNotII wasteNotII = new WasteNotII();
            WasteNotIIBtn.BackgroundImage = _resourcelibrary.ltw_WasteNotII;
            WasteNotIIBtn.Tag = wasteNotII;

            // Name of Earth
            NameofEarth nameofEarth = new NameofEarth();
            NameofEarthBtn.BackgroundImage = _resourcelibrary.ltw_NameofEarth;
            NameofEarthBtn.Tag = nameofEarth;
            #endregion

            #region Weaver CrossClass Skills

            // Careful Synthesis
            CarefulSynthesis carefulSynthesis = new CarefulSynthesis();
            CarefulSynthesisBtn.BackgroundImage = _resourcelibrary.wvr_CarefulSynthesis;
            CarefulSynthesisBtn.Tag = carefulSynthesis;

            // Brand of Lightning
            BrandOfLightning brandOfLightning = new BrandOfLightning();
            BrandOfLightningBtn.BackgroundImage = _resourcelibrary.wvr_BrandOfLightning;
            BrandOfLightningBtn.Tag = brandOfLightning;

            // Careful Synthesis II
            CarefulSynthesisII carefulSynthesisII = new CarefulSynthesisII();
            CarefulSynthesisIIBtn.BackgroundImage = _resourcelibrary.wvr_CarefulSynthesisII;
            CarefulSynthesisIIBtn.Tag = carefulSynthesisII;

            //Name of Lightning
            NameofLightning nameofLightning = new NameofLightning();
            NameofLightningBtn.BackgroundImage = _resourcelibrary.wvr_NameofLightning;
            NameofLightningBtn.Tag = nameofLightning;
            #endregion

            #region Alchemist CrossClass Skills

            // Tricks of the Trade
            TricksOfTheTrade tricksOfTheTrade = new TricksOfTheTrade();
            TricksOfTheTradeBtn.BackgroundImage = _resourcelibrary.alc_TricksOfTheTrade;
            TricksOfTheTradeBtn.Tag = tricksOfTheTrade;

            // Brand of Water
            BrandOfWater brandOfWater = new BrandOfWater();
            BrandOfWaterBtn.BackgroundImage = _resourcelibrary.alc_BrandOfWater;
            BrandOfWaterBtn.Tag = brandOfWater;

            // Comfort Zone
            ComfortZone comfortZone = new ComfortZone();
            ComfortZoneBtn.BackgroundImage = _resourcelibrary.alc_ComfortZone;
            ComfortZoneBtn.Tag = comfortZone;

            // Name of Water
            NameofWater nameofWater = new NameofWater();
            NameofWaterBtn.BackgroundImage = _resourcelibrary.alc_NameofWater;
            NameofWaterBtn.Tag = nameofWater;
            #endregion

            #region Culinarian CrossClass Skills

            // Hasty Touch
            HastyTouch hastyTouch = new HastyTouch();
            HastyTouchBtn.BackgroundImage = _resourcelibrary.cul_HastyTouch;
            HastyTouchBtn.Tag = hastyTouch;

            // Steady Hand II
            SteadyHandII steadyHandII = new SteadyHandII();
            SteadyHandIIBtn.BackgroundImage = _resourcelibrary.cul_SteadyHandII;
            SteadyHandIIBtn.Tag = steadyHandII;

            // Reclaim
            Reclaim reclaim = new Reclaim();
            ReclaimBtn.BackgroundImage = _resourcelibrary.cul_Reclaim;
            ReclaimBtn.Tag = reclaim;

            // Muscle Memory
            MuscleMemory muscleMemory = new MuscleMemory();
            MuscleMemoryBtn.BackgroundImage = _resourcelibrary.cul_MuscleMemory;
            MuscleMemoryBtn.Tag = muscleMemory;
            #endregion
        }

        /// <summary>
        /// Set the base skills and images for the current class.
        /// </summary>
        private void SetBaseSkills()
        {
            // Change the data for the BasicSynthesis button.
            BasicSynthesis.BackgroundImage = _currentCraftClass.Actions.BasicSynthesis.Image;
            BasicSynthesis.Tag = _currentCraftClass.Actions.BasicSynthesis;

            // Change the data for the StandardSynthesis button.
            StandardSynthesis.BackgroundImage = _currentCraftClass.Actions.StandardSynthesis.Image;
            StandardSynthesis.Tag = _currentCraftClass.Actions.StandardSynthesis;

            // Change the data for the BasicTouch button.
            BasicTouch.BackgroundImage = _currentCraftClass.Actions.BasicTouch.Image;
            BasicTouch.Tag = _currentCraftClass.Actions.BasicTouch;

            // Change the data for the StandardTouch button.
            StandardTouch.BackgroundImage = _currentCraftClass.Actions.StandardTouch.Image;
            StandardTouch.Tag = _currentCraftClass.Actions.StandardTouch;

            // Change the data for the AdvancedTouch button.
            AdvancedTouch.BackgroundImage = _currentCraftClass.Actions.AdvancedTouch.Image;
            AdvancedTouch.Tag = _currentCraftClass.Actions.AdvancedTouch;

            // Change the data for the MastersMend button.
            MastersMend.BackgroundImage = _resourcelibrary.all_MastersMend;
            MastersMend.Tag = _currentCraftClass.Actions.MastersMend;

            // Change the data for the MastersMendII button.
            MastersMendII.BackgroundImage = _resourcelibrary.all_MastersMendII;
            MastersMendII.Tag = _currentCraftClass.Actions.MastersMendII;

            // Change the data for the SteadyHand button.
            SteadyHand.BackgroundImage = _resourcelibrary.all_SteadyHand;
            SteadyHand.Tag = _currentCraftClass.Actions.SteadyHand;

            // Change the data for the InnerQuiet button.
            InnerQuiet.BackgroundImage = _resourcelibrary.all_InnerQuiet;
            InnerQuiet.Tag = _currentCraftClass.Actions.InnerQuiet;

            // Change the data for the GreatStrides button.
            GreatStrides.BackgroundImage = _resourcelibrary.all_GreatStrides;
            GreatStrides.Tag = _currentCraftClass.Actions.GreatStrides;

            // Change the data for the Observe button.
            Observe.BackgroundImage = _resourcelibrary.all_Observe;
            Observe.Tag = _currentCraftClass.Actions.Observe;

            // Change the data for the Collectable Synthesis.
            CollectableSynthesis.BackgroundImage = _resourcelibrary.all_CollectableSynthesis;
            CollectableSynthesis.Tag = _currentCraftClass.Actions.CollectableSynthesis;

            // Change the data for the Byregot's Brow.
            ByregotsBrow.BackgroundImage = _resourcelibrary.all_ByregotsBrow;
            ByregotsBrow.Tag = _currentCraftClass.Actions.ByregotsBrow;

            // Chagne the data for Presice Touch.
            PreciseTouch.BackgroundImage = _currentCraftClass.Actions.PreciseTouch.Image;
            PreciseTouch.Tag = _currentCraftClass.Actions.PreciseTouch;
        }

        /// <summary>
        /// Removes the last step in the Action Sequence.
        /// </summary>
        /// <param name="target">Element that needs to be removed.</param>
        /// <param name="index">Index of the element</param>
        private void RemoveLastCraftingStep(Control target, int index)
        {
            // Get the CP cost.
            int restoreCp = (int)target.Tag;

            // If ComfortZone is active, re-add the count, and remove the granted CP.
            if (_comfortZoneCounter > 0)
            {
                _cpCost = (_cpCost + 8);
                _comfortZoneCounter = _comfortZoneCounter + 1;
            }

            // Remove the CP cost from our removed action and update the counter.
            _cpCost = _cpCost - restoreCp;
            CpCounter.Text = _cpCost.ToString();

            // Remove the action from the sequence and the ActionList.
            ActionSequence.Controls.Remove(target);
            _craftActionList.RemoveAt(index - 1);
        }

        /// <summary>
        /// Empties the Action Sequence and resets the counters.
        /// </summary>
        private void EmptyActions()
        {
            // Empty the list of actions.
            _craftActionList.Clear();

            // Set the cp cost container.
            _cpCost = 0;

            // Reset the counter text.
            CpCounter.Text = 0.ToString();

            // Reset the ComfortZone Counter.
            _comfortZoneCounter = 0;

            // Reset the quantity selector.
            quantityCheck.Checked = false;
            quantityNum.Value = 1;

            // Dispose of all the pictureboxes inside the ActionSequence.
            for (int ix = ActionSequence.Controls.Count - 1; ix >= 0; ix--)
                if (ActionSequence.Controls[ix] is PictureBox) ActionSequence.Controls[ix].Dispose();
        }

        /// <summary>
        /// Loads the available crafting food into the Food combo box.
        /// </summary>
        private void SetMeals()
        {
            // Create a new instance of the AutoCompleteStringcollection.
            var autoCompleteList = new AutoCompleteStringCollection();

            // Loop through every meal in the list and add it to the collection.
            foreach (Meal meal in _mealLibrary.Meals)
                autoCompleteList.Add(meal.Name);

            // Set the recipelist as data source.
            var datasource = _mealLibrary.Meals;
            foodBox.DataSource = datasource;

            // Set the autocomplete settings for the combobox.
            foodBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            foodBox.AutoCompleteCustomSource = autoCompleteList;
            foodBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            // Set the Members for the combobox.
            foodBox.DisplayMember = "Name";
            foodBox.ValueMember = "Id";
        }

        /// <summary>
        /// Switches between the selected food quality for profiles (if applicable)
        /// </summary>
        private void SetFoodQuality()
        {
            switch (_foodQualitySelection)
            {
                case FoodQuality.nqFood:
                    _foodQuality = "NqFood";
                    break;
                case FoodQuality.hqFood:
                    _foodQuality = "HqFood";
                    break;
                default:
                    _foodQuality = "HqFood";
                    break;
            }
        }

        /// <summary>
        /// Loads the recipes for the current class, filtered by class level.
        /// </summary>
        private void LoadRecipes()
        {
            // Clear out the Recipebox.
            recipeBox.DataSource = null;

            // Get the list of recipes for the current class from the library and filter
            // it based on the class level available to the character.
            List<Recipe> currentRecipes = _recipeLibrary.LoadRecipes(_currentCraftClass.Type);

            // Create a new instance of the AutoCompleteStringcollection.
            var autoCompleteList = new AutoCompleteStringCollection();

            // Loop through every recipe in the list and add it to the collection.
            foreach (Recipe recipe in currentRecipes)
                autoCompleteList.Add(recipe.Name);

            // Set the recipelist as data source.
            var datasource = currentRecipes;
            recipeBox.DataSource = datasource;

            // Set the autocomplete settings for the combobox.
            recipeBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            recipeBox.AutoCompleteCustomSource = autoCompleteList;
            recipeBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            // Set the Members for the combobox.
            recipeBox.DisplayMember = "Name";
            recipeBox.ValueMember = "Id";
        }

        /// <summary>
        /// Sets the string for profile export, based on the material usage selection.
        /// </summary>
        private void SetMaterialUsage()
        {
            switch (_materialSelection)
            {
                case MaterialUsage.nqOnly:
                    _materialUsage = "0, 0, 0, 0, 0, 0";
                    break;
                case MaterialUsage.nqFirst:
                    _materialUsage = "-2, -2, -2, -2, -2, -2";
                    break;
                case MaterialUsage.hqFirst:
                    _materialUsage = "-1, -1, -1, -1, -1, -1";
                    break;
                default:
                    _materialUsage = "-2, -2, -2, -2, -2, -2";
                    break;
            }
        }

        /// <summary>
        /// Exports the generated crafting recipe profile to an XML file.
        /// </summary>
        /// <param name="fileName"></param>
        private void ExportRecipe(string fileName)
        {
            // Initialize the XmlWriterSettings and set the properties.
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
            {
                Indent = true
            };

            // Get our currently selected item.
            Recipe selectedItem = (Recipe)recipeBox.SelectedItem;

            // Create an XmlWriter and target it to the our destination with the set settings.
            XmlWriter xmlWriter = XmlWriter.Create(fileName, xmlWriterSettings);

            // Create a list of skills currently set as craftactions, and take the ones that are marked as cross class.
            List<int> requiredSkillList = (from craftAction in _craftActionList where craftAction.Crossclass select Convert.ToInt32(craftAction.Id)).ToList();
            // Convert the list to an array.
            string requiredSkills = string.Join(",", requiredSkillList.ToArray());

            // Set a conditionParameter if we're crafting a set quantity.
            string conditionParameter = (quantityCheck.Checked) ? "not HasAtLeast(" + selectedItem.ItemId + "," + quantityNum.Value + ")" : "True";

            // Start the document.
            xmlWriter.WriteStartDocument();

            // Open the <Profile> tag.
            xmlWriter.WriteStartElement("Profile");

            // Open the <Name> tag.
            xmlWriter.WriteStartElement("Name");

            // Popoulate the <Name> tag with the current class and recipe name.
            xmlWriter.WriteString(_currentCraftClass.Name + " - " + selectedItem.Name);

            // Close the <Name> tag.
            xmlWriter.WriteEndElement();

            // Open the <Order> tag.
            xmlWriter.WriteStartElement("Order");

            // Open the <LogMessage> tag.
            xmlWriter.WriteStartElement("LogMessage");
            xmlWriter.WriteAttributeString("Message", "本脚本由CraftExportEx自动生成");
            xmlWriter.WriteEndElement();

            // Collector Check 
            if (collectableCheck.Checked)
            {
                xmlWriter.WriteStartElement("RunCode");
                xmlWriter.WriteAttributeString("Name", "Collector");
                xmlWriter.WriteEndElement();
            }

            // Check if a certain quantity needs to be crafted.
            if (quantityCheck.Checked)
            {
                // Open the <If> tag.
                xmlWriter.WriteStartElement("If");
                // Add the "Condition" with value and quantity of the desired crafted item to the <If> tag.
                xmlWriter.WriteAttributeString("Condition", "not HasAtLeast(" + selectedItem.ItemId + "," + quantityNum.Value + ")");
            }

            // Open the <While> tag.
            xmlWriter.WriteStartElement("While");

            // Add the "Condition" with value of the conditionParameter string to the <While> tag.
            xmlWriter.WriteAttributeString("Condition", conditionParameter);

            // Open the <LogMessage> tag.
            xmlWriter.WriteStartElement("LogMessage");

            // Add the Message attribute to the <LogMessage> tag.
            xmlWriter.WriteAttributeString("Message", "制作——" + selectedItem.Name);

            // Close the <LogMessage> tag.
            xmlWriter.WriteEndElement();

            // Check if the profile requires food.
            if (enableFood.Checked)
            {
                // Get the meal from the combobox.
                Meal meal = (Meal)foodBox.SelectedItem;

                // Open the <EatFood> tag.
                xmlWriter.WriteStartElement("EatFood");

                // Add the "ItemId" with value from our selected meal to the <EatFood> tag.
                xmlWriter.WriteAttributeString("ItemId", meal.Id.ToString());

                // Add the "Name" with value from our selected meal to the <EatFood> tag.
                xmlWriter.WriteAttributeString("Name", meal.Name);

                // Add the "HqFood" or "NqFood" to the <EatFood> tag.
                xmlWriter.WriteAttributeString(_foodQuality, "True");

                // Add the "MinDuration" to the <EatFood> tag.
                xmlWriter.WriteAttributeString("MinDuration", ((int)minFoodDuration.Value).ToString());

                // Close the <EatFood> tag.
                xmlWriter.WriteEndElement();
            }

            // Open the <Synthesize> tag.
            xmlWriter.WriteStartElement("Synthesize");

            // Add the "RecipeId" with value from our selected item to the <Synthesize> tag.
            xmlWriter.WriteAttributeString("RecipeId", selectedItem.Id.ToString());

            // Add the "RequiredSkills" with array of skills to the <Synthesize> tag.
            xmlWriter.WriteAttributeString("RequiredSkills", requiredSkills);

            // Add the "MinimumCp" with value of the cpCost variable to the <Synthesize> tag.
            xmlWriter.WriteAttributeString("MinimumCp", _cpCost.ToString());

            // Add the "HQMats" with value from the checkbox to the <Synthesize> tag.
            xmlWriter.WriteAttributeString("HQMats", _materialUsage);

            // Close the <Synthesize> tag.
            xmlWriter.WriteEndElement();

            // Open the <While> tag.
            xmlWriter.WriteStartElement("While");

            // Add the "Condition" with value "CraftingManager.IsCrafting" to the <While> tag.
            xmlWriter.WriteAttributeString("Condition", "CraftingManager.IsCrafting");

            // Loop through each CraftAction inside the _craftActionList.
            foreach (CraftAction action in _craftActionList)
            {
                if (action.Name == "集中加工")
                {
                    xmlWriter.WriteStartElement("RunCode");
                    xmlWriter.WriteAttributeString("Name", "UsePreciseTouch");
                    xmlWriter.WriteEndElement();
                    continue;
                }

                if (action.Name == "秘诀")
                {
                    xmlWriter.WriteStartElement("RunCode");
                    xmlWriter.WriteAttributeString("Name", "ToT");
                    xmlWriter.WriteEndElement();
                    continue;
                }
                // Open the <CraftAction> tag.
                xmlWriter.WriteStartElement("CraftAction");

                // Add the "ActionId" with the ActionId value from our action to the <CraftAction> tag.
                xmlWriter.WriteAttributeString("ActionId", Convert.ToInt32(action.Id).ToString());

                // Add the "Name" with the ActionName value from our action to the <CraftAction> tag.
                xmlWriter.WriteAttributeString("Name", action.Name);

                // Close the <CraftAction> tag.
                xmlWriter.WriteEndElement();
            }

            // CollectableCheck DoubleCheck
            if (collectableCheck.Checked)
            {
                xmlWriter.WriteStartElement("RunCode");
                xmlWriter.WriteAttributeString("Name", "Collectable");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("RunCode");
                xmlWriter.WriteAttributeString("Name", "Collectable");
                xmlWriter.WriteEndElement();
            }

            // Close the <While> tag.
            xmlWriter.WriteEndElement();

            // Close the <While> tag.
            xmlWriter.WriteEndElement();

            // Check if a certain quantity needs to be crafted.
            if (quantityCheck.Checked)
            {
                // Close the Initial <If> tag.
                xmlWriter.WriteEndElement();

                // Open the <If> tag.
                xmlWriter.WriteStartElement("If");

                // Add the "Condition" with value and quantity of the desired crafted item to the <If> tag.
                xmlWriter.WriteAttributeString("Condition", "HasAtLeast(" + selectedItem.ItemId + "," + quantityNum.Value + ")");

                // Open the <LogMessage> tag.
                xmlWriter.WriteStartElement("LogMessage");

                // Add the Message attribute to the <LogMessage> tag.
                xmlWriter.WriteAttributeString("Message", "制作完成，你的包裹里已有 " + quantityNum.Value + " " + selectedItem.Name + " 个。");

                // Close the <LogMessage> tag.
                xmlWriter.WriteEndElement();

                // Open and Close the <StopCrafting> tag.
                xmlWriter.WriteStartElement("StopCrafting");
                xmlWriter.WriteEndElement();

                // Close the <If> tag for Quantity Check
                xmlWriter.WriteEndElement();
            }

            // Close the <Order> tag.
            xmlWriter.WriteEndElement();

            // Open CodeChunks tag.
            xmlWriter.WriteStartElement("CodeChunks");

            if (collectableCheck.Checked)
            {
                // Open Collectable CodeChunk tag.
                xmlWriter.WriteStartElement("CodeChunk");

                xmlWriter.WriteAttributeString("Name", "Collectable");
                //WriteCDATA
                xmlWriter.WriteCData("if (await Buddy.Coroutines.Coroutine.Wait(5000, () => ff14bot.Managers.Actionmanager.CanCast(100069, null)))  {   ff14bot.Managers.Actionmanager.DoAction(100069, null);  await Buddy.Coroutines.Coroutine.Wait(10000, () => CraftingManager.AnimationLocked);    await Buddy.Coroutines.Coroutine.Wait(Timeout.Infinite, () => !CraftingManager.AnimationLocked || ff14bot.RemoteWindows.SelectYesNoItem.IsOpen);    if (ff14bot.RemoteWindows.SelectYesNoItem.IsOpen)   {   ff14bot.RemoteWindows.SelectYesNoItem.Yes();    await Buddy.Coroutines.Coroutine.Wait(10000, () => !ff14bot.RemoteWindows.SelectYesNoItem.IsOpen);  await Buddy.Coroutines.Coroutine.Wait(Timeout.Infinite, () => !CraftingManager.AnimationLocked);    }   }	");
                // Close Collectable CodeChunk Tag.
                xmlWriter.WriteEndElement();

                // Open Collector CodeChunk tag.
                xmlWriter.WriteStartElement("CodeChunk");

                xmlWriter.WriteAttributeString("Name", "Collector");

                //WriteCDATA
                xmlWriter.WriteCData("if (!Core.Player.HasAura(903)) { if (ff14bot.Managers.Actionmanager.CanCast(4560, null)) { Actionmanager.DoAction(4560, null); } if (ff14bot.Managers.Actionmanager.CanCast(4561, null)) { Actionmanager.DoAction(4561, null); } if (ff14bot.Managers.Actionmanager.CanCast(4562, null)) { Actionmanager.DoAction(4562, null); } if (ff14bot.Managers.Actionmanager.CanCast(4563, null)) { Actionmanager.DoAction(4563, null); } if (ff14bot.Managers.Actionmanager.CanCast(4564, null)) { Actionmanager.DoAction(4564, null); } if (ff14bot.Managers.Actionmanager.CanCast(4565, null)) { Actionmanager.DoAction(4565, null); } if (ff14bot.Managers.Actionmanager.CanCast(4566, null)) { Actionmanager.DoAction(4566, null); } if (ff14bot.Managers.Actionmanager.CanCast(4567, null)) { Actionmanager.DoAction(4567, null); } await Coroutine.Wait(7000, () => Core.Player.HasAura(903)); }");

                // Close Collector CodeChunk Tag.
                xmlWriter.WriteEndElement();
            }

            // Open Precise Touch CodeChunk tag.
            xmlWriter.WriteStartElement("CodeChunk");

            xmlWriter.WriteAttributeString("Name", "UsePreciseTouch");

            //WriteCDATA
            xmlWriter.WriteCData("SpellData data;   if ((CraftingManager.Condition == CraftingCondition.Good || CraftingManager.Condition == CraftingCondition.Excellent) &&    Actionmanager.CurrentActions.TryGetValue(\"Precise Touch\", out data) && Actionmanager.CanCast(data, null)) {   Actionmanager.DoAction(\"Precise Touch\", null);    Logging.Write(Colors.Azure,\"[制作步骤] 高品质，使用 集中加工\"); }   else {  Logging.Write(Colors.Azure,\"[制作步骤] 普通品质，使用 加工\");  Actionmanager.DoAction(\"Basic Touch\", null);  }   await Coroutine.Wait(10000, () => CraftingManager.AnimationLocked); await Coroutine.Wait(Timeout.Infinite, () => !CraftingManager.AnimationLocked); await Coroutine.Sleep(250);");

            // Close Precise Touch CodeChunk Tag.
            xmlWriter.WriteEndElement();

            // Open TOT CodeChunk tag.
            xmlWriter.WriteStartElement("CodeChunk");

            xmlWriter.WriteAttributeString("Name", "ToT");

            //WriteCDATA
            xmlWriter.WriteCData("SpellData data; if ((CraftingManager.Condition == CraftingCondition.Good || CraftingManager.Condition == CraftingCondition.Excellent) && Actionmanager.CurrentActions.TryGetValue(\"Tricks of the Trade\", out data) && Actionmanager.CanCast(data, null))  { Actionmanager.DoAction(\"Tricks of the Trade\", null); Logging.Write(Colors.Azure,\"[制作步骤] 高品质，使用 秘诀\"); await Coroutine.Wait(10000, () => CraftingManager.AnimationLocked); await Coroutine.Wait(Timeout.Infinite, () => !CraftingManager.AnimationLocked); } await Coroutine.Sleep(250);");

            // Close TOT CodeChunk Tag.
            xmlWriter.WriteEndElement();

            // Close CodeChunks Tag.
            xmlWriter.WriteEndElement();
            
            // Close the <Profile> tag.
            xmlWriter.WriteEndElement();

            // End the document.
            xmlWriter.WriteEndDocument();

            // Close the XmlWriter.
            xmlWriter.Close();
        }

        #endregion

        #region LinkLabel

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://ffsusu.com/scxh/");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://ffxiv.rs.exdreams.net/crafter/");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://cha.17173.com/ff14/recipe");
        }

        private void RecipeContainer_Leave(object sender, EventArgs e)
        {
            Recipe selectedItem = (Recipe)recipeBox.SelectedItem;
            CurrentRecipeId.Text = selectedItem.Id.ToString();
            CurrentItemId.Text = selectedItem.ItemId.ToString();
        }

        #endregion

        /// <summary>
        /// Change the style of Skills Selection
        /// </summary>
        #region Style Change
        private int styleTypeId;
        private bool imageA;
        private bool imageB;
        private void ChangeViewStyle(object sender, EventArgs e)
        {
            if (styleTypeId == 0)
            {
                
                //CrossClassContainer.Visible = false;
                this.SynSkills.Visible = false;
                this.BuffSkills.Visible = false;
                this.Name_Brand.Visible = false;
                this.TouchSkills.Visible = false;
                this.BaseSkillsOld.Visible = true;

                this.CarpenterSkills.Visible = true;
                this.LeatherworkerSkills.Visible = true;
                this.BlacksmithSkills.Visible = true;
                this.WeaverSkills.Visible = true;
                this.ArmorerSkills.Visible = true;
                this.AlchemistSkills.Visible = true;
                this.GoldsmithSkills.Visible = true;
                this.CulinarianSkills.Visible = true;
                this.BaseSkillsOld.Visible = true;
                // 
                // BaseSkillsOld
                // 
                this.BaseSkillsOld.Controls.Add(this.PreciseTouch);
                this.BaseSkillsOld.Controls.Add(this.ByregotsBrow);
                this.BaseSkillsOld.Controls.Add(this.Observe);
                this.BaseSkillsOld.Controls.Add(this.GreatStrides);
                this.BaseSkillsOld.Controls.Add(this.InnerQuiet);
                this.BaseSkillsOld.Controls.Add(this.SteadyHand);
                this.BaseSkillsOld.Controls.Add(this.MastersMendII);
                this.BaseSkillsOld.Controls.Add(this.MastersMend);
                this.BaseSkillsOld.Controls.Add(this.AdvancedTouch);
                this.BaseSkillsOld.Controls.Add(this.StandardTouch);
                this.BaseSkillsOld.Controls.Add(this.BasicTouch);
                this.BaseSkillsOld.Controls.Add(this.StandardSynthesis);
                this.BaseSkillsOld.Controls.Add(this.BasicSynthesis);
                this.BaseSkillsOld.Location = new System.Drawing.Point(475, 105);
                this.BaseSkillsOld.Name = "BaseSkillsOld";
                this.BaseSkillsOld.Size = new System.Drawing.Size(544, 126);
                this.BaseSkillsOld.TabIndex = 2;
                this.BaseSkillsOld.TabStop = false;
                this.BaseSkillsOld.Text = "基础技能";
                // 

                this.CrossClassContainer.Controls.Add(this.CulinarianSkills);
                this.CrossClassContainer.Controls.Add(this.GoldsmithSkills);
                this.CrossClassContainer.Controls.Add(this.AlchemistSkills);
                this.CrossClassContainer.Controls.Add(this.ArmorerSkills);
                this.CrossClassContainer.Controls.Add(this.WeaverSkills);
                this.CrossClassContainer.Controls.Add(this.BlacksmithSkills);
                this.CrossClassContainer.Controls.Add(this.LeatherworkerSkills);
                this.CrossClassContainer.Controls.Add(this.CarpenterSkills);
                this.CrossClassContainer.Location = new System.Drawing.Point(475, 230);
                this.CrossClassContainer.Name = "CrossClassContainer";
                this.CrossClassContainer.Size = new System.Drawing.Size(544, 447);
                this.CrossClassContainer.TabIndex = 17;
                this.CrossClassContainer.TabStop = false;
                this.CrossClassContainer.Text = "共享技能";
                // 
                // CulinarianSkills
                // 
                this.CulinarianSkills.Controls.Add(this.MuscleMemoryBtn);
                this.CulinarianSkills.Controls.Add(this.SteadyHandIIBtn);
                this.CulinarianSkills.Controls.Add(this.ReclaimBtn);
                this.CulinarianSkills.Controls.Add(this.HastyTouchBtn);
                this.CulinarianSkills.Location = new System.Drawing.Point(279, 264);
                this.CulinarianSkills.Name = "CulinarianSkills";
                this.CulinarianSkills.Size = new System.Drawing.Size(250, 77);
                this.CulinarianSkills.TabIndex = 0;
                this.CulinarianSkills.TabStop = false;
                this.CulinarianSkills.Text = "烹调师";
                // 
                // MuscleMemoryBtn
                // 

                this.MuscleMemoryBtn.Location = new System.Drawing.Point(178, 19);
                this.MuscleMemoryBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // SteadyHandIIBtn
                // 

                this.SteadyHandIIBtn.Location = new System.Drawing.Point(66, 19);
                this.SteadyHandIIBtn.Size = new System.Drawing.Size(50, 46);

                // 
                // ReclaimBtn
                // 
                this.ReclaimBtn.Location = new System.Drawing.Point(122, 19);
                this.ReclaimBtn.Size = new System.Drawing.Size(50, 46);

                // 
                // HastyTouchBtn
                // 
                this.HastyTouchBtn.Location = new System.Drawing.Point(10, 19);
                this.HastyTouchBtn.Size = new System.Drawing.Size(50, 46);

                // 
                // GoldsmithSkills
                // 
                this.GoldsmithSkills.Controls.Add(this.MakersMarkBtn);
                this.GoldsmithSkills.Controls.Add(this.FlawlessSynthesisBtn);
                this.GoldsmithSkills.Controls.Add(this.InnovationBtn);
                this.GoldsmithSkills.Controls.Add(this.ManipulationBtn);
                this.GoldsmithSkills.Name = "GoldsmithSkills";
                this.GoldsmithSkills.TabIndex = 0;
                this.GoldsmithSkills.TabStop = false;
                this.GoldsmithSkills.Text = "雕金匠";
                this.GoldsmithSkills.Location = new System.Drawing.Point(279, 100);
                this.GoldsmithSkills.Size = new System.Drawing.Size(250, 77);
                // 
                // MakersMarkBtn
                // 
                this.MakersMarkBtn.Location = new System.Drawing.Point(178, 19);
                this.MakersMarkBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // FlawlessSynthesisBtn
                // 
                this.FlawlessSynthesisBtn.Location = new System.Drawing.Point(66, 19);
                this.FlawlessSynthesisBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // InnovationBtn
                // 
                this.InnovationBtn.Location = new System.Drawing.Point(122, 19);
                this.InnovationBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // ManipulationBtn
                // 
                this.ManipulationBtn.Location = new System.Drawing.Point(10, 19);
                this.ManipulationBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // AlchemistSkills
                // 
                this.AlchemistSkills.Controls.Add(this.NameofWaterBtn);
                this.AlchemistSkills.Controls.Add(this.BrandOfWaterBtn);
                this.AlchemistSkills.Controls.Add(this.ComfortZoneBtn);
                this.AlchemistSkills.Controls.Add(this.TricksOfTheTradeBtn);
                this.AlchemistSkills.Location = new System.Drawing.Point(14, 264);
                this.AlchemistSkills.Name = "AlchemistSkills";
                this.AlchemistSkills.Size = new System.Drawing.Size(250, 77);
                this.AlchemistSkills.TabIndex = 0;
                this.AlchemistSkills.TabStop = false;
                this.AlchemistSkills.Text = "炼金术士";
                // 
                // NameofWaterBtn
                // 
                this.NameofWaterBtn.Location = new System.Drawing.Point(178, 20);
                this.NameofWaterBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // BrandOfWaterBtn
                // 
                this.BrandOfWaterBtn.Location = new System.Drawing.Point(66, 19);
                this.BrandOfWaterBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // ComfortZoneBtn
                // 
                this.ComfortZoneBtn.Location = new System.Drawing.Point(122, 19);
                this.ComfortZoneBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // TricksOfTheTradeBtn
                // 
                this.TricksOfTheTradeBtn.Location = new System.Drawing.Point(10, 19);
                this.TricksOfTheTradeBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // ArmorerSkills
                // 
                this.ArmorerSkills.Controls.Add(this.NameofIceBtn);
                this.ArmorerSkills.Controls.Add(this.BrandOfIceBtn);
                this.ArmorerSkills.Controls.Add(this.PieceByPieceBtn);
                this.ArmorerSkills.Controls.Add(this.RapidSynthesisBtn);
                this.ArmorerSkills.Location = new System.Drawing.Point(14, 100);
                this.ArmorerSkills.Name = "ArmorerSkills";
                this.ArmorerSkills.Size = new System.Drawing.Size(250, 77);
                this.ArmorerSkills.TabIndex = 0;
                this.ArmorerSkills.TabStop = false;
                this.ArmorerSkills.Text = "铸甲匠";
                // 
                // NameofIceBtn
                // 
                this.NameofIceBtn.Location = new System.Drawing.Point(178, 19);
                this.NameofIceBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // BrandOfIceBtn
                // 
                this.BrandOfIceBtn.Location = new System.Drawing.Point(66, 19);
                this.BrandOfIceBtn.Size = new System.Drawing.Size(50, 46);
  
                // 
                // PieceByPieceBtn
                // 
                this.PieceByPieceBtn.Location = new System.Drawing.Point(122, 19);
                this.PieceByPieceBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // RapidSynthesisBtn
                // 
                this.RapidSynthesisBtn.Location = new System.Drawing.Point(10, 19);
                this.RapidSynthesisBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // WeaverSkills
                // 
                this.WeaverSkills.Controls.Add(this.NameofLightningBtn);
                this.WeaverSkills.Controls.Add(this.BrandOfLightningBtn);
                this.WeaverSkills.Controls.Add(this.CarefulSynthesisIIBtn);
                this.WeaverSkills.Controls.Add(this.CarefulSynthesisBtn);
                this.WeaverSkills.Location = new System.Drawing.Point(279, 182);
                this.WeaverSkills.Name = "WeaverSkills";
                this.WeaverSkills.Size = new System.Drawing.Size(250, 77);
                this.WeaverSkills.TabIndex = 0;
                this.WeaverSkills.TabStop = false;
                this.WeaverSkills.Text = "裁衣匠";
                // 
                // NameofLightningBtn
                // 
                this.NameofLightningBtn.Location = new System.Drawing.Point(178, 18);
                this.NameofLightningBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // BrandOfLightningBtn
                // 
                this.BrandOfLightningBtn.Location = new System.Drawing.Point(66, 19);
                this.BrandOfLightningBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // CarefulSynthesisIIBtn
                // 
                this.CarefulSynthesisIIBtn.Location = new System.Drawing.Point(122, 19);
                this.CarefulSynthesisIIBtn.Size = new System.Drawing.Size(50, 46);
 
                // 
                // CarefulSynthesisBtn
                // 
                this.CarefulSynthesisBtn.Location = new System.Drawing.Point(10, 19);
                this.CarefulSynthesisBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // BlacksmithSkills
                // 
                this.BlacksmithSkills.Controls.Add(this.NameofFireBtn);
                this.BlacksmithSkills.Controls.Add(this.BrandOfFireBtn);
                this.BlacksmithSkills.Controls.Add(this.IngenuityIIBtn);
                this.BlacksmithSkills.Controls.Add(this.IngenuityBtn);
                this.BlacksmithSkills.Location = new System.Drawing.Point(279, 18);
                this.BlacksmithSkills.Name = "BlacksmithSkills";
                this.BlacksmithSkills.Size = new System.Drawing.Size(250, 77);
                this.BlacksmithSkills.TabIndex = 0;
                this.BlacksmithSkills.TabStop = false;
                this.BlacksmithSkills.Text = "锻铁匠";
                // 
                // NameofFireBtn
                // 
                this.NameofFireBtn.Location = new System.Drawing.Point(178, 19);
                this.NameofFireBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // BrandOfFireBtn
                // 
                this.BrandOfFireBtn.Location = new System.Drawing.Point(66, 19);
                this.BrandOfFireBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // IngenuityIIBtn
                // 
                this.IngenuityIIBtn.Location = new System.Drawing.Point(122, 19);
                this.IngenuityIIBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // IngenuityBtn
                // 
                this.IngenuityBtn.Location = new System.Drawing.Point(10, 19);
                this.IngenuityBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // LeatherworkerSkills
                // 
                this.LeatherworkerSkills.Controls.Add(this.NameofEarthBtn);
                this.LeatherworkerSkills.Controls.Add(this.BrandOfEarthBtn);
                this.LeatherworkerSkills.Controls.Add(this.WasteNotIIBtn);
                this.LeatherworkerSkills.Controls.Add(this.WasteNotBtn);
                this.LeatherworkerSkills.Location = new System.Drawing.Point(14, 182);
                this.LeatherworkerSkills.Name = "LeatherworkerSkills";
                this.LeatherworkerSkills.Size = new System.Drawing.Size(250, 77);
                this.LeatherworkerSkills.TabIndex = 0;
                this.LeatherworkerSkills.TabStop = false;
                this.LeatherworkerSkills.Text = "制革匠";
                // 
                // NameofEarthBtn
                // 
                this.NameofEarthBtn.Location = new System.Drawing.Point(178, 19);
                this.NameofEarthBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // BrandOfEarthBtn
                // 
                this.BrandOfEarthBtn.Location = new System.Drawing.Point(66, 19);
                this.BrandOfEarthBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // WasteNotIIBtn
                // 
                this.WasteNotIIBtn.Location = new System.Drawing.Point(122, 19);
                this.WasteNotIIBtn.Size = new System.Drawing.Size(50, 46);
  
                // 
                // WasteNotBtn
                // 
                this.WasteNotBtn.Location = new System.Drawing.Point(10, 19);
                this.WasteNotBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // CarpenterSkills
                // 
                this.CarpenterSkills.Controls.Add(this.NameofWindBtn);
                this.CarpenterSkills.Controls.Add(this.BrandOfWindBtn);
                this.CarpenterSkills.Controls.Add(this.ByregotsBlessingBtn);
                this.CarpenterSkills.Controls.Add(this.RuminationBtn);
                this.CarpenterSkills.Location = new System.Drawing.Point(14, 20);
                this.CarpenterSkills.Name = "CarpenterSkills";
                this.CarpenterSkills.Size = new System.Drawing.Size(250, 77);
                this.CarpenterSkills.TabIndex = 0;
                this.CarpenterSkills.TabStop = false;
                this.CarpenterSkills.Text = "刻木匠";
                // 
                // NameofWindBtn
                // 
                this.NameofWindBtn.Location = new System.Drawing.Point(178, 19);
                this.NameofWindBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // BrandOfWindBtn
                // 
  
                this.BrandOfWindBtn.Location = new System.Drawing.Point(66, 19);
                this.BrandOfWindBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // ByregotsBlessingBtn
                // 
                this.ByregotsBlessingBtn.Location = new System.Drawing.Point(122, 19);
                this.ByregotsBlessingBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // RuminationBtn
                // 
                this.RuminationBtn.Location = new System.Drawing.Point(10, 19);
                this.RuminationBtn.Size = new System.Drawing.Size(50, 46);

                // 
                // PreciseTouch
                // 
                this.PreciseTouch.Location = new System.Drawing.Point(403, 69);
                this.PreciseTouch.Size = new System.Drawing.Size(50, 46);
  
                // 
                // ByregotsBrow
                // 
                this.ByregotsBrow.Location = new System.Drawing.Point(374, 17);
                this.ByregotsBrow.Size = new System.Drawing.Size(50, 46);
                // 
                // Observe
                // 
                this.Observe.Location = new System.Drawing.Point(308, 69);
                this.Observe.Size = new System.Drawing.Size(50, 46);
                // 
                // GreatStrides
                // 
                this.GreatStrides.Location = new System.Drawing.Point(252, 69);
                this.GreatStrides.Size = new System.Drawing.Size(50, 46);
                // 
                // InnerQuiet
                // 
                this.InnerQuiet.Location = new System.Drawing.Point(196, 69);
                this.InnerQuiet.Size = new System.Drawing.Size(50, 46);
                // 
                // SteadyHand
                // 
                this.SteadyHand.Location = new System.Drawing.Point(140, 69);
                this.SteadyHand.Size = new System.Drawing.Size(50, 46);
                // 
                // MastersMendII
                // 
                this.MastersMendII.Location = new System.Drawing.Point(84, 69);
                this.MastersMendII.Size = new System.Drawing.Size(50, 46);
                // 
                // MastersMend
                // 
                this.MastersMend.Location = new System.Drawing.Point(28, 69);
                this.MastersMend.Size = new System.Drawing.Size(50, 46);
                // 
                // AdvancedTouch
                // 
                this.AdvancedTouch.Location = new System.Drawing.Point(279, 18);
                this.AdvancedTouch.Size = new System.Drawing.Size(50, 46);
                // 
                // StandardTouch
                // 
                this.StandardTouch.Location = new System.Drawing.Point(223, 18);
                this.StandardTouch.Size = new System.Drawing.Size(50, 46);
                // 
                // BasicTouch
                // 
                this.BasicTouch.Location = new System.Drawing.Point(167, 18);
                this.BasicTouch.Size = new System.Drawing.Size(50, 46);
                // 
                // StandardSynthesis
                // 
                this.StandardSynthesis.Location = new System.Drawing.Point(111, 18);
                this.StandardSynthesis.Size = new System.Drawing.Size(50, 46);
 
                // 
                // BasicSynthesis
                // 
                this.BasicSynthesis.Location = new System.Drawing.Point(55, 18);
                this.BasicSynthesis.Size = new System.Drawing.Size(50, 46);

                // 
                // MasterSkills
                // 
                this.MasterSkills.Location = new System.Drawing.Point(14, 343);
                this.MasterSkills.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.MasterSkills.Name = "MasterSkills";
                this.MasterSkills.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.MasterSkills.Size = new System.Drawing.Size(514, 79);
                this.MasterSkills.TabIndex = 14;
                this.MasterSkills.TabStop = false;
                this.MasterSkills.Text = "专家技能 （就给你看看）";
                // 
                // pictureBox2
                // 
                this.pictureBox2.BackgroundImage = global::CraftExportEx.Properties.Resources.all_WhistleWhileYouWork;
                this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                this.pictureBox2.Location = new System.Drawing.Point(426, 24);
                this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.pictureBox2.Name = "pictureBox2";
                this.pictureBox2.Size = new System.Drawing.Size(50, 46);
                this.pictureBox2.TabIndex = 1;
                this.pictureBox2.TabStop = false;
                // 
                // pictureBox1
                // 
                this.pictureBox1.BackgroundImage = global::CraftExportEx.Properties.Resources.all_Satisfaction;
                this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                this.pictureBox1.Location = new System.Drawing.Point(355, 24);
                this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.pictureBox1.Name = "pictureBox1";
                this.pictureBox1.Size = new System.Drawing.Size(50, 46);
                this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
                this.pictureBox1.TabIndex = 0;
                this.pictureBox1.TabStop = false;
                // 
                // pictureBox7
                // 
                this.pictureBox7.BackgroundImage = global::CraftExportEx.Properties.Resources.all_HeartoftheCulinarian;
                this.pictureBox7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                this.pictureBox7.Location = new System.Drawing.Point(153, 24);
                this.pictureBox7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.pictureBox7.Name = "pictureBox7";
                this.pictureBox7.Size = new System.Drawing.Size(50, 46);
                this.pictureBox7.TabIndex = 6;
                this.pictureBox7.TabStop = false;
                // 
                // pictureBox6
                // 
                this.pictureBox6.BackgroundImage = global::CraftExportEx.Properties.Resources.all_TrainedHand;
                this.pictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                this.pictureBox6.Location = new System.Drawing.Point(82, 24);
                this.pictureBox6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.pictureBox6.Name = "pictureBox6";
                this.pictureBox6.Size = new System.Drawing.Size(50, 46);
                this.pictureBox6.TabIndex = 5;
                this.pictureBox6.TabStop = false;
                // 
                // pictureBox5
                // 
                this.pictureBox5.BackgroundImage = global::CraftExportEx.Properties.Resources.all_ByregotsMiracle;
                this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                this.pictureBox5.Location = new System.Drawing.Point(12, 24);
                this.pictureBox5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.pictureBox5.Name = "pictureBox5";
                this.pictureBox5.Size = new System.Drawing.Size(50, 46);
                this.pictureBox5.TabIndex = 4;
                this.pictureBox5.TabStop = false;
                // 
                // pictureBox4
                // 
                this.pictureBox4.BackgroundImage = global::CraftExportEx.Properties.Resources.all_NymeiasWheel;
                this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                this.pictureBox4.Location = new System.Drawing.Point(287, 24);
                this.pictureBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.pictureBox4.Name = "pictureBox4";
                this.pictureBox4.Size = new System.Drawing.Size(50, 46);
                this.pictureBox4.TabIndex = 3;
                this.pictureBox4.TabStop = false;
                // 
                // pictureBox3
                // 
                this.pictureBox3.BackgroundImage = global::CraftExportEx.Properties.Resources.all_InnovativeTouch;
                this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                this.pictureBox3.Location = new System.Drawing.Point(220, 24);
                this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.pictureBox3.Name = "pictureBox3";
                this.pictureBox3.Size = new System.Drawing.Size(50, 46);
                this.pictureBox3.TabIndex = 2;
                this.pictureBox3.TabStop = false;

                styleTypeId = 1;
            }
            else
            {
                this.SynSkills.Visible = true;
                this.BuffSkills.Visible = true;
                this.Name_Brand.Visible = true;
                this.TouchSkills.Visible = true;
                this.BaseSkillsOld.Visible = false;

                this.CarpenterSkills.Visible = false;
                this.LeatherworkerSkills.Visible = false;
                this.BlacksmithSkills.Visible = false;
                this.WeaverSkills.Visible = false;
                this.ArmorerSkills.Visible = false;
                this.AlchemistSkills.Visible = false;
                this.GoldsmithSkills.Visible = false;
                this.CulinarianSkills.Visible = false;
                this.BaseSkillsOld.Visible = false;

                // 
                // MasterSkills
                // 
                this.MasterSkills.Controls.Add(this.pictureBox7);
                this.MasterSkills.Controls.Add(this.pictureBox6);
                this.MasterSkills.Controls.Add(this.pictureBox5);
                this.MasterSkills.Controls.Add(this.pictureBox4);
                this.MasterSkills.Controls.Add(this.pictureBox3);
                this.MasterSkills.Controls.Add(this.pictureBox2);
                this.MasterSkills.Controls.Add(this.pictureBox1);
                this.MasterSkills.Location = new System.Drawing.Point(10, 408);
                this.MasterSkills.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.MasterSkills.Name = "MasterSkills";
                this.MasterSkills.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.MasterSkills.Size = new System.Drawing.Size(258, 160);
                this.MasterSkills.TabIndex = 14;
                this.MasterSkills.TabStop = false;
                this.MasterSkills.Text = "专家技能 （就给你看看）";
                // 
                // pictureBox7
                // 
                this.pictureBox7.Location = new System.Drawing.Point(132, 82);
                this.pictureBox7.Size = new System.Drawing.Size(50, 46);
                // 
                // pictureBox6
                // 
                this.pictureBox6.Location = new System.Drawing.Point(72, 82);
                this.pictureBox6.Size = new System.Drawing.Size(50, 46);
                // 
                // pictureBox5
                // 
                this.pictureBox5.Location = new System.Drawing.Point(12, 82);
                this.pictureBox5.Size = new System.Drawing.Size(50, 46);
                // 
                // pictureBox4
                // 
                this.pictureBox4.Location = new System.Drawing.Point(192, 28);
                this.pictureBox4.Size = new System.Drawing.Size(50, 46);
                // 
                // pictureBox3
                // 
                this.pictureBox3.Location = new System.Drawing.Point(132, 28);
                this.pictureBox3.Size = new System.Drawing.Size(50, 46);
                // 
                // pictureBox2
                // 
                this.pictureBox2.Location = new System.Drawing.Point(72, 28);
                this.pictureBox2.Size = new System.Drawing.Size(50, 46);
                // 
                // pictureBox1
                // 
                this.pictureBox1.Location = new System.Drawing.Point(12, 28);
                this.pictureBox1.Size = new System.Drawing.Size(50, 46);
                // 
                // BuffSkills
                // 
                this.BuffSkills.Controls.Add(this.InnerQuiet);
                this.BuffSkills.Controls.Add(this.Observe);
                this.BuffSkills.Controls.Add(this.MakersMarkBtn);
                this.BuffSkills.Controls.Add(this.GreatStrides);
                this.BuffSkills.Controls.Add(this.WasteNotBtn);
                this.BuffSkills.Controls.Add(this.SteadyHandIIBtn);
                this.BuffSkills.Controls.Add(this.SteadyHand);
                this.BuffSkills.Controls.Add(this.ReclaimBtn);
                this.BuffSkills.Controls.Add(this.RuminationBtn);
                this.BuffSkills.Controls.Add(this.WasteNotIIBtn);
                this.BuffSkills.Controls.Add(this.IngenuityBtn);
                this.BuffSkills.Controls.Add(this.IngenuityIIBtn);
                this.BuffSkills.Controls.Add(this.InnovationBtn);
                this.BuffSkills.Controls.Add(this.TricksOfTheTradeBtn);
                this.BuffSkills.Controls.Add(this.ComfortZoneBtn);
                this.BuffSkills.Location = new System.Drawing.Point(274, 22);
                this.BuffSkills.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.BuffSkills.Name = "BuffSkills";
                this.BuffSkills.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.BuffSkills.Size = new System.Drawing.Size(258, 281);
                this.BuffSkills.TabIndex = 18;
                this.BuffSkills.TabStop = false;
                this.BuffSkills.Text = "Buff系技能";
                // 
                // InnerQuiet
                // 
                this.InnerQuiet.Location = new System.Drawing.Point(72, 28);
                this.InnerQuiet.Size = new System.Drawing.Size(50, 46);
                // 
                // Observe
                // 
                this.Observe.Location = new System.Drawing.Point(132, 194);
                this.Observe.Size = new System.Drawing.Size(50, 46);
                // 
                // MakersMarkBtn
                // 
                this.MakersMarkBtn.Location = new System.Drawing.Point(12, 138);
                this.MakersMarkBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // GreatStrides
                // 
                this.GreatStrides.Location = new System.Drawing.Point(192, 28);
                this.GreatStrides.Size = new System.Drawing.Size(50, 46);
                // 
                // WasteNotBtn
                // 
                this.WasteNotBtn.Location = new System.Drawing.Point(192, 138);
                this.WasteNotBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // SteadyHandIIBtn
                // 
                this.SteadyHandIIBtn.Location = new System.Drawing.Point(13, 82);
                this.SteadyHandIIBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // SteadyHand
                // 

                this.SteadyHand.Location = new System.Drawing.Point(72, 82);
                this.SteadyHand.Size = new System.Drawing.Size(50, 46);
                // 
                // ReclaimBtn
                // 
                this.ReclaimBtn.Location = new System.Drawing.Point(72, 194);
                this.ReclaimBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // RuminationBtn
                // 
                this.RuminationBtn.Location = new System.Drawing.Point(72, 138);
                this.RuminationBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // WasteNotIIBtn
                // 
                this.WasteNotIIBtn.Location = new System.Drawing.Point(132, 138);
                this.WasteNotIIBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // IngenuityBtn
                // 
                this.IngenuityBtn.Location = new System.Drawing.Point(192, 82);
                this.IngenuityBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // IngenuityIIBtn
                // 
                this.IngenuityIIBtn.Location = new System.Drawing.Point(132, 82);
                this.IngenuityIIBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // InnovationBtn
                // 
                this.InnovationBtn.Location = new System.Drawing.Point(132, 28);
                this.InnovationBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // TricksOfTheTradeBtn
                // 
                this.TricksOfTheTradeBtn.Location = new System.Drawing.Point(12, 194);
                this.TricksOfTheTradeBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // ComfortZoneBtn
                // 
                this.ComfortZoneBtn.Location = new System.Drawing.Point(12, 28);
                this.ComfortZoneBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // Name_Brand
                // 
                this.Name_Brand.Controls.Add(this.BrandOfWaterBtn);
                this.Name_Brand.Controls.Add(this.NameofWaterBtn);
                this.Name_Brand.Controls.Add(this.BrandOfLightningBtn);
                this.Name_Brand.Controls.Add(this.NameofLightningBtn);
                this.Name_Brand.Controls.Add(this.BrandOfEarthBtn);
                this.Name_Brand.Controls.Add(this.NameofEarthBtn);
                this.Name_Brand.Controls.Add(this.BrandOfIceBtn);
                this.Name_Brand.Controls.Add(this.NameofIceBtn);
                this.Name_Brand.Controls.Add(this.BrandOfFireBtn);
                this.Name_Brand.Controls.Add(this.NameofFireBtn);
                this.Name_Brand.Controls.Add(this.BrandOfWindBtn);
                this.Name_Brand.Controls.Add(this.NameofWindBtn);
                this.Name_Brand.Location = new System.Drawing.Point(274, 311);
                this.Name_Brand.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.Name_Brand.Name = "Name_Brand";
                this.Name_Brand.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.Name_Brand.Size = new System.Drawing.Size(258, 257);
                this.Name_Brand.TabIndex = 19;
                this.Name_Brand.TabStop = false;
                this.Name_Brand.Text = "美名/印记";
                // 
                // BrandOfWaterBtn
                // 
                this.BrandOfWaterBtn.Location = new System.Drawing.Point(132, 138);
                this.BrandOfWaterBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // NameofWaterBtn
                // 
                this.NameofWaterBtn.Location = new System.Drawing.Point(192, 138);
                this.NameofWaterBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // BrandOfLightningBtn
                // 
                this.BrandOfLightningBtn.Location = new System.Drawing.Point(12, 138);
                this.BrandOfLightningBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // NameofLightningBtn
                // 
                this.NameofLightningBtn.Location = new System.Drawing.Point(72, 138);
                this.NameofLightningBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // BrandOfEarthBtn
                // 
                this.BrandOfEarthBtn.Location = new System.Drawing.Point(132, 82);
                this.BrandOfEarthBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // NameofEarthBtn
                // 
                this.NameofEarthBtn.Location = new System.Drawing.Point(192, 82);
                this.NameofEarthBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // BrandOfIceBtn
                // 
                this.BrandOfIceBtn.Location = new System.Drawing.Point(132, 28);
                this.BrandOfIceBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // NameofIceBtn
                // 
                this.NameofIceBtn.Location = new System.Drawing.Point(192, 28);
                this.NameofIceBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // BrandOfFireBtn
                // 
                this.BrandOfFireBtn.Location = new System.Drawing.Point(12, 82);
                this.BrandOfFireBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // NameofFireBtn
                // 
                this.NameofFireBtn.Location = new System.Drawing.Point(72, 82);
                this.NameofFireBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // BrandOfWindBtn
                // 
                this.BrandOfWindBtn.Location = new System.Drawing.Point(12, 28);
                this.BrandOfWindBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // NameofWindBtn
                // 
                this.NameofWindBtn.Location = new System.Drawing.Point(72, 28);
                this.NameofWindBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // TouchSkills
                // 
                this.TouchSkills.Controls.Add(this.ByregotsBrow);
                this.TouchSkills.Controls.Add(this.PreciseTouch);
                this.TouchSkills.Controls.Add(this.ByregotsBlessingBtn);
                this.TouchSkills.Controls.Add(this.HastyTouchBtn);
                this.TouchSkills.Controls.Add(this.BasicTouch);
                this.TouchSkills.Controls.Add(this.StandardTouch);
                this.TouchSkills.Controls.Add(this.AdvancedTouch);
                this.TouchSkills.Location = new System.Drawing.Point(10, 244);
                this.TouchSkills.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.TouchSkills.Name = "TouchSkills";
                this.TouchSkills.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.TouchSkills.Size = new System.Drawing.Size(258, 156);
                this.TouchSkills.TabIndex = 0;
                this.TouchSkills.TabStop = false;
                this.TouchSkills.Text = "加工系技能";
                // 
                // ByregotsBrow
                // 
                this.ByregotsBrow.Location = new System.Drawing.Point(132, 82);
                this.ByregotsBrow.Size = new System.Drawing.Size(50, 46);
                // 
                // PreciseTouch
                // 
                this.PreciseTouch.Location = new System.Drawing.Point(192, 28);
                this.PreciseTouch.Size = new System.Drawing.Size(50, 46);
                // 
                // ByregotsBlessingBtn
                // 
                this.ByregotsBlessingBtn.Location = new System.Drawing.Point(72, 82);
                this.ByregotsBlessingBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // HastyTouchBtn
                // 
                this.HastyTouchBtn.Location = new System.Drawing.Point(12, 82);
                this.HastyTouchBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // BasicTouch
                // 
                this.BasicTouch.Location = new System.Drawing.Point(12, 28);
                this.BasicTouch.Size = new System.Drawing.Size(50, 46);
                // 
                // StandardTouch
                // 
                this.StandardTouch.Location = new System.Drawing.Point(72, 28);
                this.StandardTouch.Size = new System.Drawing.Size(50, 46);
                // 
                // AdvancedTouch
                // 
                this.AdvancedTouch.Location = new System.Drawing.Point(132, 28);
                this.AdvancedTouch.Size = new System.Drawing.Size(50, 46);
                // 
                // SynSkills
                // 
                this.SynSkills.Controls.Add(this.CarefulSynthesisIIBtn);
                this.SynSkills.Controls.Add(this.FlawlessSynthesisBtn);
                this.SynSkills.Controls.Add(this.MuscleMemoryBtn);
                this.SynSkills.Controls.Add(this.CarefulSynthesisBtn);
                this.SynSkills.Controls.Add(this.MastersMendII);
                this.SynSkills.Controls.Add(this.PieceByPieceBtn);
                this.SynSkills.Controls.Add(this.MastersMend);
                this.SynSkills.Controls.Add(this.RapidSynthesisBtn);
                this.SynSkills.Controls.Add(this.BasicSynthesis);
                this.SynSkills.Controls.Add(this.StandardSynthesis);
                this.SynSkills.Controls.Add(this.ManipulationBtn);
                this.SynSkills.Location = new System.Drawing.Point(10, 22);
                this.SynSkills.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.SynSkills.Name = "SynSkills";
                this.SynSkills.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.SynSkills.Size = new System.Drawing.Size(258, 214);
                this.SynSkills.TabIndex = 19;
                this.SynSkills.TabStop = false;
                this.SynSkills.Text = "作业系技能";
                // 
                // CarefulSynthesisIIBtn
                // 
                this.CarefulSynthesisIIBtn.Location = new System.Drawing.Point(132, 28);
                this.CarefulSynthesisIIBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // FlawlessSynthesisBtn
                // 
                this.FlawlessSynthesisBtn.Location = new System.Drawing.Point(192, 82);
                this.FlawlessSynthesisBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // MuscleMemoryBtn
                // 
                this.MuscleMemoryBtn.Location = new System.Drawing.Point(12, 28);
                this.MuscleMemoryBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // CarefulSynthesisBtn
                // 
                this.CarefulSynthesisBtn.Location = new System.Drawing.Point(192, 28);
                this.CarefulSynthesisBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // MastersMendII
                // 
                this.MastersMendII.Location = new System.Drawing.Point(72, 138);
                this.MastersMendII.Size = new System.Drawing.Size(50, 46);
                // 
                // PieceByPieceBtn
                // 
                this.PieceByPieceBtn.Location = new System.Drawing.Point(72, 28);
                this.PieceByPieceBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // MastersMend
                // 
                this.MastersMend.Location = new System.Drawing.Point(12, 138);
                this.MastersMend.Size = new System.Drawing.Size(50, 46);
                // 
                // RapidSynthesisBtn
                // 
                this.RapidSynthesisBtn.Location = new System.Drawing.Point(132, 82);
                this.RapidSynthesisBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // BasicSynthesis
                // 
                this.BasicSynthesis.Location = new System.Drawing.Point(12, 82);
                this.BasicSynthesis.Size = new System.Drawing.Size(50, 46);
                // 
                // StandardSynthesis
                // 
                this.StandardSynthesis.Location = new System.Drawing.Point(72, 82);
                this.StandardSynthesis.Size = new System.Drawing.Size(50, 46);
                // 
                // ManipulationBtn
                // 
                this.ManipulationBtn.Location = new System.Drawing.Point(132, 138);
                this.ManipulationBtn.Size = new System.Drawing.Size(50, 46);
                // 
                // CrossClassContainer
                // 
                this.CrossClassContainer.Controls.Add(this.SynSkills);
                this.CrossClassContainer.Controls.Add(this.TouchSkills);
                this.CrossClassContainer.Controls.Add(this.Name_Brand);
                this.CrossClassContainer.Controls.Add(this.BuffSkills);
                this.CrossClassContainer.Controls.Add(this.MasterSkills);
                this.CrossClassContainer.Location = new System.Drawing.Point(475, 102);
                this.CrossClassContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.CrossClassContainer.Name = "CrossClassContainer";
                this.CrossClassContainer.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
                this.CrossClassContainer.Size = new System.Drawing.Size(544, 559);
                this.CrossClassContainer.TabIndex = 4;
                this.CrossClassContainer.TabStop = false;
                this.CrossClassContainer.Text = "制作技能";

                styleTypeId = 0;
            }
            if (imageA == true && imageB == false && pictureBox8.Image != Properties.Resources.PicBox8_B)
            {
                pictureBox8.Image = Properties.Resources.PicBox8_B;
                imageA = false;
                imageB = true;
            }
            else
            {
                pictureBox8.Image = Properties.Resources.PicBox8;
                imageA = true;
                imageB = false;
            }
        }

        #endregion
    }
}
 